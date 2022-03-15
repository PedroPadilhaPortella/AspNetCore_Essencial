using APICatalogo.Context;
using APICatalogo.Controllers;
using APICatalogo.DTO;
using APICatalogo.DTO.Mapping;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APICatalogoXUnitTests
{
    public class CategoriasUnitTestController
    {
        private AppDbContext context;
        private IMapper mapper;
        private IUnitOfWork repository;

        public static DbContextOptions<AppDbContext> dbContextOptions { get; }

        public static string connectionString = "server=localhost;port=3306;database=catalogo_db;uid=root;password=root;";

        static CategoriasUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseMySql(connectionString).Options;
        }

        public CategoriasUnitTestController()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapper = config.CreateMapper();

            var context = new AppDbContext(dbContextOptions);
            this.context = context;
            repository = new UnitOfWork(context);

            //DBUnitTestsMockInitializer db = new DBUnitTestsMockInitializer();
            //db.Seed(context);
        }

        //  ===============================Testes Unitários==================================

        //================================= Get(int id) =====================================
        [Fact]
        public async Task GetCategoriaById_Return_OkResult()
        {
            //Arrange  
            var controller = new CategoriasController(context, repository, mapper);
            var id = 2;

            //Act  
            var data = await controller.GetById(id);
            Console.WriteLine(data);

            //Assert  
            Assert.IsType<Categoria>(data.Value);
        }

        [Fact]
        public async Task GetCategoriaById_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new CategoriasController(context, repository, mapper);
            var id = 9999;

            //Act  
            var data = await controller.GetById(id);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data.Result);
        }

        //====================================== Get ========================================
        [Fact]
        public async Task GetCategorias_Return_OkResult()
        {
            //Arrange  
            var controller = new CategoriasController(context, repository, mapper);
            CategoriasParameters parameters = new CategoriasParameters() { PageNumber = 1, PageSize = 10 };

            //Act
            var data = await controller.Get(parameters);

            //Assert  
            Assert.IsAssignableFrom<List<CategoriaDTO>>(data.Value);
        }

        //====================================Post=====================================

        [Fact]
        public async Task Post_Categoria_AddValidData_Return_CreatedResult()
        {
            //Arrange  
            var controller = new CategoriasController(context, repository, mapper);

            var categoria = new Categoria() { Nome = "Teste Unitario 1", ImagemUrl = "testecat.jpg" };

            //Act  
            var data = await controller.Post(categoria);

            //Assert 
            Assert.IsType<CreatedAtRouteResult>(data);
        }

        ////===========================================Put =====================================

        [Fact]
        public async Task Put_Categoria_Update_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new CategoriasController(context, repository, mapper);
            var id = 6;

            //Act  
            var existingPost = await controller.GetById(id);
            var result = existingPost.Value.Should().BeAssignableTo<Categoria>().Subject;

            var categoria = new Categoria();
            categoria.Id = id;
            categoria.Nome = "Categoria Atualizada - Testes 1";
            categoria.ImagemUrl = result.ImagemUrl;

            var updatedData = await controller.Put(id, categoria);

            //Assert  
            Assert.IsType<OkObjectResult>(updatedData);
        }

        ////=======================================Delete ===================================
        [Fact]
        public async Task Delete_Categoria_Return_OkResult()
        {
            //Arrange  
            var controller = new CategoriasController(context, repository, mapper);
            var catId = 11;

            //Act  
            var data = await controller.Delete(catId);

            //Assert  
            Assert.IsType<Categoria>(data.Value);
        }

        [Fact]
        public async Task Delete_Categoria_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new CategoriasController(context, repository, mapper);
            var catId = 999999;

            //Act  
            var data = await controller.Delete(catId);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data.Result);
        }
    }
}
