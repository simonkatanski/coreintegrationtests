using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CityApi.Entities;
using CityApi.IntegrationTests.Utility;
using Newtonsoft.Json;
using Xunit;

namespace CityApi.IntegrationTests
{
    
    public class CityControllerTests
    {
        [Fact]
        public async Task GivenExistingCityInDb_ThenReturnCollectionWithCityName()
        {
            using (var server = new ApiTestServer())
            {
                //Arrange
                server.CityContext.Add(new City {CityName = "Bristol", Population = 100000});
                server.CityContext.SaveChanges();
                var request = server.CreateRequest("/api/cities");

                //Act
                var response = await request.GetAsync();
                var responseContent = await response.Content.ReadAsStringAsync();
                var cityNames = JsonConvert.DeserializeObject<List<string>>(responseContent);
                
                //Assert
                Assert.True(cityNames.Count == 1);
                Assert.True(response.StatusCode == HttpStatusCode.OK);
            }
        }

        [Fact]
        public async Task GivenEmptyDb_ThenExpectCityToBeAdded()
        {
            using (var server = new ApiTestServer())
            {
                //Arrange
                var cityToAdd = new City {CityName = "Bristol", Population = 100000};
                var stringData = JsonConvert.SerializeObject(cityToAdd);
                var request = server.CreateRequest("/api/cities")
                    .And(c => c.Content = new StringContent(stringData, Encoding.UTF8, "application/json"));

                //Act
                var response = await request.PostAsync();
                
                //Assert
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                Assert.True(server.CityContext.Cities.Count() == 1);
            }
        }
        
        [Fact]
        public async Task GivenNonEmptyDb_ThenExpectCityToBeAdded()
        {
            using (var server = new ApiTestServer())
            {
                //Arrange
                server.CityContext.Add(new City { CityName = "Bristol", Population = 100000 });
                server.CityContext.SaveChanges();

                var cityToAdd = new City { CityName = "Berlin", Population = 100000 };
                var stringData = JsonConvert.SerializeObject(cityToAdd);
                var request = server.CreateRequest("/api/cities")
                    .And(c => c.Content = new StringContent(
                        stringData, 
                        Encoding.UTF8, 
                        "application/json"));

                //Act
                var response = await request.PostAsync();

                //Assert
                Assert.True(response.StatusCode == HttpStatusCode.OK);
                Assert.True(server.CityContext.Cities.Count() == 2);
            }
        }
    }
}