using APICatalogoMinimal.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoMinimal.Context
{
    public class CatalogoMinimalDbContext : DbContext
    {
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Produto>? Produtos { get; set; }

        public CatalogoMinimalDbContext(DbContextOptions<CatalogoMinimalDbContext> options) 
            : base(options) { }
    
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Categoria>().HasKey((c) => c.Id);
            mb.Entity<Categoria>().Property((c) => c.Nome).HasMaxLength(100).IsRequired();
            mb.Entity<Categoria>().Property((c) => c.Descricao).HasMaxLength(150).IsRequired();

            mb.Entity<Produto>().HasKey((c) => c.Id);
            mb.Entity<Produto>().Property((c) => c.Nome).HasMaxLength(100).IsRequired();
            mb.Entity<Produto>().Property((c) => c.Descricao).HasMaxLength(150);
            mb.Entity<Produto>().Property((c) => c.Imagem).HasMaxLength(100);
            mb.Entity<Produto>().Property((c) => c.Preco).HasPrecision(14, 2);

            mb.Entity<Produto>()
                .HasOne<Categoria>((p) => p.Categoria)
                .WithMany(p => p.Produtos)
                .HasForeignKey(c => c.CategoriaId);

        }
    }
}
