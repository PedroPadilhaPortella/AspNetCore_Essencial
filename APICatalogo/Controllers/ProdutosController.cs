using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            this._context = context;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            try
            {
                return _context.Produtos.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Produto> GetById(int id)
        {
            try
            {
                var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.Id.Equals(id));

                if (produto == null)
                    return NotFound();

                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            try
            {
                //if(!ModelState.IsValid)
                //    return BadRequest(ModelState);

                Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == produto.CategoriaId);

                if(categoria == null)
                    return NotFound();

                produto.Categoria = categoria;

                _context.Produtos.Add(produto);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterProduto", new { id = produto.Id }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Produto produto)
        {
            try
            {
                if (id != produto.Id)
                    return BadRequest();

                Categoria categoria = _context.Categorias.FirstOrDefault(c => c.Id == produto.CategoriaId);

                if (categoria == null)
                    return NotFound();

                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            try
            {
                var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);

                if (produto == null)
                    return NotFound();

                _context.Produtos.Remove(produto);
                _context.SaveChanges();
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao acessar dados do banco");
            }
        }
    }
}
