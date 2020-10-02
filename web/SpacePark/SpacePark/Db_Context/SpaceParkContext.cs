using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpacePark.Services;
using System.IO;

namespace SpacePark.Models
{
    public class SpaceParkContext : DbContext
    {
        public SpaceParkContext() { }
        AzureKeyVaultService _aKVService = new AzureKeyVaultService();
        public DbSet<Parkinglot> Parkinglot { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Spaceship> Spaceships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var azureDbCon = _aKVService.GetKeyVaultSecret("https://spacepark-kv-dev-01.vault.azure.net/secrets/ConnectionStrings--spacepark-sqldb-dev-01/f54483ed80744f0bad0bdfb31203d786");
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var defaultConnectionString = config.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(defaultConnectionString))
            {
                
                optionsBuilder.UseSqlServer(defaultConnectionString);

            }
            else
            {
                optionsBuilder.UseSqlServer(azureDbCon);
            }
        }
    }
}
