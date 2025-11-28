using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyApp.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=192.168.1.100;Database=POC-localDb;User Id=sa;Password=ScenarioERP@7507674;TrustServerCertificate=True;Command Timeout=300;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
