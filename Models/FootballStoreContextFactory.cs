using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FootballStoreApp.Models
{
    public class FootballStoreContextFactory : IDesignTimeDbContextFactory<FootballStoreContext>
    {
        public FootballStoreContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<FootballStoreContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("FootballStore"));

            return new FootballStoreContext(optionsBuilder.Options);
        }
    }
}
