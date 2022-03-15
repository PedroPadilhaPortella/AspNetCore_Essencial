using APICatalogo.DTO;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using APICatalogo.Services.Filters;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICatalogo.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[Controller]")]
    [EnableCors("AllowAPIRequest")]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProdutosController : Controller
    {
        //private readonly AppDbContext _context;
        private IUnitOfWork _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProdutosController(
            IUnitOfWork context, 
            IConfiguration configuration, 
            ILogger<ProdutosController> logger,
            IMapper mapper 
        )
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters parameters)
        {
            try
            {
                //return _context.Produtos.AsNoTracking().ToList();
                //var produtos = _context.ProdutoRepository.GetAll().ToList();
                var produtos = await _context.ProdutoRepository.GetProducts(parameters);

                var metadata = new
                {
                    produtos.TotalCount,
                    produtos.PageSize,
                    produtos.TotalPages,
                    produtos.CurrentPage,
                    produtos.HasNext,
                    produtos.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return _mapper.Map<List<ProdutoDTO>>(produtos);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        // [HttpGet("{valor:alpha:length(5)}")]
        public async Task<ActionResult<ProdutoDTO>> GetById(int id)
        {
            try
            {
                //var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));
                var produto = await _context.ProdutoRepository.GetById(p => p.Id.Equals(id));

                if (produto == null)
                    return NotFound();

                return _mapper.Map<ProdutoDTO>(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                //if(!ModelState.IsValid)
                //    return BadRequest(ModelState);

                Produto produto = _mapper.Map<Produto>(produtoDto);

                //Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == produto.CategoriaId);
                Categoria categoria = await _context.CategoriaRepository.GetById(c => c.Id == produto.CategoriaId);

                if (categoria == null)
                    return NotFound();

                produto.Categoria = categoria;

                //_context.Produtos.Add(produto);
                //_context.SaveChanges();
                _context.ProdutoRepository.Add(produto);
                await _context.Commit();

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                if (id != produtoDto.Id)
                    return BadRequest();

                Produto produto = _mapper.Map<Produto>(produtoDto);

                //Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == produto.CategoriaId);
                Categoria categoria = await _context.CategoriaRepository.GetById(c => c.Id == produto.CategoriaId);

                if (categoria == null)
                    return NotFound();

                //_context.Entry(produto).State = EntityState.Modified;
                //_context.SaveChanges();
                _context.ProdutoRepository.Update(produto);
                await _context.Commit();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            try
            {
                //var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
                var produto = await _context.ProdutoRepository.GetById(p => p.Id == id);

                if (produto == null)
                    return NotFound();

                //_context.Produtos.Remove(produto);
                //_context.SaveChanges();
                _context.ProdutoRepository.Delete(produto);
                await _context.Commit();

                return _mapper.Map<ProdutoDTO>(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        [HttpGet("/autor")]
        public ActionResult GetAutor()
        {
            string autor = _configuration["Metadata:Autor"];
            _logger.LogInformation("-- GET /autor --");
            _logger.LogInformation(autor);
            return Ok(autor);
        }

        [HttpGet("price")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetOrderByPrice()
        {
            try
            {
                return _mapper.Map<List<ProdutoDTO>>(_context.ProdutoRepository.GetProductsByPrice());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }
    }
}
