using APICatalogoMinimal.Context;
using APICatalogoMinimal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//ConfigureServices
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CatalogoMinimalDbContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//Configure
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Endpoints Categorias
app.MapPost("/categorias", async ([FromBody] Categoria categoria, [FromServices] CatalogoMinimalDbContext db) =>
{
    db.Categorias.Add(categoria);
    await db.SaveChangesAsync();
    return Results.Created($"/categorias/{categoria.Id}", categoria);
})
    .Accepts<Categoria>("application/json")
    .Produces<Categoria>(StatusCodes.Status201Created)
    .WithName("CriarCategoria")
    .WithTags("APICatalogoMinimal");

app.MapGet("/categorias", async (CatalogoMinimalDbContext db) => await db.Categorias.ToListAsync());

app.MapGet("/categorias/{id:int}", async (int id, CatalogoMinimalDbContext db) =>
{
    return await db.Categorias.FindAsync(id) 
    is Categoria categoria 
        ? Results.Ok(categoria) 
        : Results.NotFound();
});

app.MapPut("/categorias/{id:int}", async (int id, Categoria categoria, CatalogoMinimalDbContext db) =>
{
    if (categoria.Id != id) return Results.BadRequest();

    var categoriaDb = await db.Categorias.FindAsync(id);

    if (categoriaDb == null) return Results.NotFound();

    categoriaDb.Nome = categoria.Nome;
    categoriaDb.Descricao = categoria.Descricao;

    await db.SaveChangesAsync();
    return Results.Ok(categoriaDb);
});

app.MapDelete("/categorias/{id:int}", async (int id, CatalogoMinimalDbContext db) =>
{
    var categoria = await db.Categorias.FindAsync(id);

    if (categoria == null) return Results.NotFound();

    db.Categorias.Remove(categoria);
    await db.SaveChangesAsync();
    return Results.NoContent();
});


//Endpoints Produtos
app.MapPost("/produtos", async ([FromBody] Produto produto, [FromServices] CatalogoMinimalDbContext db) =>
{
    Categoria categoria = db.Categorias.FirstOrDefault(c => c.Id == produto.CategoriaId);

    if (categoria == null) return Results.NotFound();
    produto.Categoria = categoria;

    db.Produtos.Add(produto);
    await db.SaveChangesAsync();
    return Results.Created($"/categorias/{produto.Id}", produto);
})
    .Accepts<Produto>("application/json")
    .Produces<Produto>(StatusCodes.Status201Created)
    .WithName("CriarProduto")
    .WithTags("APICatalogoMinimal");

app.MapGet("/produtos", async (CatalogoMinimalDbContext db) => await db.Produtos.ToListAsync());

app.MapGet("/produtos/{id:int}", async (int id, CatalogoMinimalDbContext db) =>
{
    return await db.Produtos.FindAsync(id)
    is Produto produto
        ? Results.Ok(produto)
        : Results.NotFound();
});

app.MapPut("/produtos/{id:int}", async (int id, Produto produto, CatalogoMinimalDbContext db) =>
{
    if (produto.Id != id) return Results.BadRequest();

    Categoria categoria = db.Categorias.FirstOrDefault(c => c.Id == produto.CategoriaId);
    if (categoria == null) return Results.NotFound();

    var produtoDb = await db.Produtos.FindAsync(id);

    if (produtoDb == null) return Results.NotFound();

    produtoDb.Nome = produto.Nome;
    produtoDb.Descricao = produto.Descricao;

    await db.SaveChangesAsync();
    return Results.Ok(produtoDb);
});

app.MapDelete("/produtos/{id:int}", async (int id, CatalogoMinimalDbContext db) =>
{
    var produto = await db.Produtos.FindAsync(id);

    if (produto == null) return Results.NotFound();

    db.Produtos.Remove(produto);
    await db.SaveChangesAsync();
    return Results.NoContent();
});




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
