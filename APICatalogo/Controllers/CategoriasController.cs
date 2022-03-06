using APICatalogo.Context;
using APICatalogo.DTO;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
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
    [ApiController]
    [Route("api/[Controller]")]
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

        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get([FromQuery] CategoriasParameters parameters)
        {
            try
            {
                //return _context.Categorias.AsNoTracking().Include(c => c.Produtos).ToList();
                //var categorias = _uof.CategoriaRepository.GetAll().ToList();

                var categorias = _uof.CategoriaRepository.GetCategories(parameters);

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

        [HttpGet("{id:int:min(1)}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetById(int id)
        {
            try
            {
                Categoria categoria = _context.Categorias
                .AsNoTracking()
                .Include(c => c.Produtos)
                .FirstOrDefault(c => c.Id.Equals(id));

                if (categoria == null)
                    return NotFound("Categoria não encontrada.");

                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            try
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.Id }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao adicionar Categoria");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.Id)
                    return BadRequest("Id da Categoria não coincide.");

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok("Categoria atualizada com Sucesso.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar Categoria");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);

                if (categoria == null)
                    return NotFound("Categoria não encontrada");

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao remover Categoria");
            }
        }
    }
}
