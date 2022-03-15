using APICatalogo.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APICatalogo.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [EnableCors("AllowAPIRequest")]
    [Produces("application/json")]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthorizationController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Retorna um teste de acesso do AuthorizationController, com a data e hora atual
        /// </summary>
        /// <returns>String com a data atual</returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "AuthorizationController: " + DateTime.Now.ToLongDateString();
        }

        /// <summary>
        /// Registra um novo usuário com base no email, senha e senha de confirmação
        /// </summary>
        /// <param name="model"></param>
        /// <returns>OkObjectResult com token de acesso</returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UsuarioDTO model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(user, false);
            return Ok(GenerateToken(model));
        }

        /// <summary>
        /// login de usuário com base no email, senha e senha de confirmação
        /// </summary>
        /// <param name="model"></param>
        /// <returns>OkObjectResult com token de acesso</returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UsuarioDTO model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            // Verifica as credenciais do usuario e retorna um valor
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password, 
                isPersistent: false, 
                lockoutOnFailure: false
            );

            if (result.Succeeded)
            {
                return Ok(GenerateToken(model));
            } else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido ...");
                return BadRequest(ModelState);
            }
        }

        private object GenerateToken(UsuarioDTO userInfo)
        {
            // definir declarações do usuario
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Chave Simetrica
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(double.Parse(_configuration["TokenConfiguration:ExpireHours"]));

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: _configuration["TokenConfiguration:Issuer"],
               audience: _configuration["TokenConfiguration:Audience"],
               expires: expiration,
               claims: claims,
               signingCredentials: credentials
            );

            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token Jwt generated successfully"
            };
        }
    }
}
