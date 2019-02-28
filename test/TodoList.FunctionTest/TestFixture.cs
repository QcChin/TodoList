using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoList.Repository.Data;
using TodoList.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace TodoList.FunctionTest
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _server;
        public HttpClient Client { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddEntityFrameworkInMemoryDatabase();

                    var provider = services
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                    services.AddDbContext<TodoContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(provider);
                    });

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("Identity");
                        options.UseInternalServiceProvider(provider);
                    });
                    services.AddDefaultIdentity<IdentityUser>()
                        .AddDefaultUI(UIFramework.Bootstrap4)
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddDefaultTokenProviders();

                    //var sp = services.BuildServiceProvider();

                    //using (var scope = sp.CreateScope())
                    //{
                    //    var scopedServices = scope.ServiceProvider;
                    //    var db = scopedServices.GetRequiredService<DbContext>();

                    //    // Ensure the database is created.
                    //    db.Database.EnsureCreated();
                    //}
                });

            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5000");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
