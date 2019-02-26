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

namespace TodoList.FunctionTest
{
    public class CustomWebApplicationFactory<TStartUp>
        : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
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

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DbContext>();
                    var loggerFactory = scopedServices.GetRequiredService<ILoggerFactory>();

                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartUp>>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();
                }
            });
        }
    }
}
