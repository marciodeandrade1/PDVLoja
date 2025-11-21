using Microsoft.EntityFrameworkCore; // Add this using directive


using PDVLoja.Models;

namespace PDVLoja.Data
{
    public class PdvContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }
        public DbSet<UsuarioCaixa> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Use Helpers.ConnectionHelper para connection string
                object value = optionsBuilder.UseSqlServer(Helpers.ConnectionHelper.GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemVenda>()
                .HasOne(i => i.Venda)
                .WithMany(v => v.Itens)
                .HasForeignKey(i => i.IdVenda);

            modelBuilder.Entity<ItemVenda>()
                .HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.IdProduto);

            modelBuilder.Entity<Produto>()
                .Property(p => p.EstoqueMinimo)
                .HasDefaultValue(10);
        }
    }
}
