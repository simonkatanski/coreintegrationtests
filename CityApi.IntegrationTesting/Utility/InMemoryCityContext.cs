using System;
using CityApi.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CityApi.IntegrationTests.Utility
{
    public class InMemoryCityContext : CityContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }
}