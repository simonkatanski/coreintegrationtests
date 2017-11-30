using System.Collections.Generic;
using System.Linq;
using CityApi.Contexts;
using CityApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CityApi.Controllers
{
    [Route("api")]
    public class CityController : Controller
    {
        private readonly ICityContext _cityContext;

        public CityController(ICityContext cityContext)
        {
            _cityContext = cityContext;
        }
        
        [HttpGet("cities")]
        public IEnumerable<string> Get()
        {
            var cities = _cityContext.Cities.ToList();
            if (cities.Any())
            {
                return cities.Select(c => c.CityName);
            }
            return Enumerable.Empty<string>();
        }

        [HttpPost("cities")]
        public void Post([FromBody]City city)
        {
            _cityContext.Cities.Add(city);
            _cityContext.SaveChanges();
        }
    }
}
