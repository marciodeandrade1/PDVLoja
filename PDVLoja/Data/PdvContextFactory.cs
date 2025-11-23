using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PDVLoja.Data
{
    public class PdvContextFactory : IDesignTimeDbContextFactory<PdvContext>
    {
        public PdvContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PdvContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=PDV_LojaDB;Trusted_Connection=True;");

            return new PdvContext(optionsBuilder.Options);
        }
    }
}