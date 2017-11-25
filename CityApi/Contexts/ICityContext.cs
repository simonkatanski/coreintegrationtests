using CityApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityApi.Contexts
{
    public interface ICityContext
    {
        DbSet<City> Cities { get; set; }

        int SaveChanges();
    }
}