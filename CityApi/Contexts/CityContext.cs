using CityApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityApi.Contexts
{
    public class CityContext : DbContext, ICityContext
    {
        public DbSet<City> Cities { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite("Data Source=Cities.db");
    }
}