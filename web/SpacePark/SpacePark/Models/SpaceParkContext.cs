using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SpacePark.Models
{
    public class SpaceParkContext : DbContext
    {
        public SpaceParkContext() { }
        public DbSet<Parkinglot> Parkinglot { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Spaceship> Spaceships { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            var defaultConnectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(defaultConnectionString);
        }
    }
}
