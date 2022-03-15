using APICatalogo.Context;
using APICatalogo.DTO;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [ApiController]
    [EnableCors("AllowAPIRequest")]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoriasController(AppDbContext context, IUnitOfWork uof, IMapper mapper)
        {
            _context = context;
            _uof = uof;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as Categorias, podendo ser paginadas via queryString
        /// </summary>
        /// <param name="parameters">Informações de QueryString, PageNumber e PageSize</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters parameters)
        {
            try
            {
                //return _context.Categorias.AsNoTracking().Include(c => c.Produtos).ToList();
                //var categorias = _uof.CategoriaRepository.GetAll().ToList();

                var categorias = await _uof.CategoriaRepository.GetCategories(parameters);

                var metadata = new
                {
                    categorias.TotalCount,
                    categorias.PageSize,
                    categorias.TotalPages,
                    categorias.CurrentPage,
                    categorias.HasNext,
                    categorias.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                return _mapper.Map<List<CategoriaDTO>>(categorias);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        /// <summary>
        /// Retorna uma Categoria por Id
        /// </summary>
        /// <param name="id">Id da Categoria</param>
        /// <returns></returns>
        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> GetById(int id)
        {
            try
            {
                Categoria categoria = await _context.Categorias
                .AsNoTracking()
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id.Equals(id));

                if (categoria == null)
                    return NotFound("Categoria não encontrada.");

                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        /// <summary>
        /// Salvar uma nova Categoria
        /// </summary>
        /// <param name="categoria">Categoria</param>
        /// <returns>A Categoria Criada</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Categoria categoria)
        {
            try
            {
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.Id }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar Categoria");
            }
        }

        /// <summary>
        /// Atualizar de uma Categoria
        /// </summary>
        /// <param name="id">Id da Categoria Atualizada</param>
        /// <param name="categoria">Categoria</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.Id)
                    return BadRequest("Id da Categoria não coincide.");

                _context.Entry(categoria).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Categoria atualizada com Sucesso.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar Categoria");
            }
        }

        /// <summary>
        /// Excluir uma Categoria por Id
        /// </summary>
        /// <param name="id">Id da Categoria</param>
        /// <returns>Categoria</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

                if (categoria == null)
                    return NotFound("Categoria não encontrada");

                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao remover Categoria");
            }
        }
    }
}
