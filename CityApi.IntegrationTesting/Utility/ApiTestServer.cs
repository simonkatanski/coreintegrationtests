using System;
using CityApi.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CityApi.IntegrationTests.Utility
{
    public class ApiTestServer : IDisposable
    {
        private readonly TestServer _server;
        public InMemoryCityContext CityContext { get; }

        /// <summary>
        ///     A wrapper around the TestServer, which also contains the 
        ///     EF contexts used in the API.
        /// </summary>
        public ApiTestServer()
        {

            CityContext = new InMemoryCityContext();
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(RegisterDependencies));
        }

        public RequestBuilder CreateRequest(string path)
        {
            return _server.CreateRequest(path);
        }
        
        /// <summary>
        ///     Register dependencies, which differ from the ordinary setup of the API. 
        ///     For the registration here to work, you need to use the TryAdd* versions
        ///     of container registration methods.
        /// </summary>
        private void RegisterDependencies(IServiceCollection service)
        {
            service.AddSingleton<ICityContext>(CityContext);
        }

        public void Dispose()
        {
            _server?.Dispose();
        }
    }
}