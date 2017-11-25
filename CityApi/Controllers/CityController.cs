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
        
        [HttpGet("cities/{id}")]
        public string Get(int id)
        {
            var foundCity = _cityContext.Cities.FirstOrDefault(p => p.Id == id);
            return foundCity?.CityName;
        }
        
        [HttpPost("cities")]
        public void Post([FromBody]City city)
        {
            _cityContext.Cities.Add(city);
            _cityContext.SaveChanges();
        }
    }
}
