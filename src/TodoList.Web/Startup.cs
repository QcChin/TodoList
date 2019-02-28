using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoList.Repository.Data;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Services;
using Autofac.Extensions.DependencyInjection;
using TodoList.Domain.Models;
using Autofac;
using System;

namespace TodoList.Web
{
    public class Startup
    {
        //private IServiceCollection _services;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureCookieSettings(services);
            CreateIdentityIfNotCreated(services);
            ConfigureInject(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //var builder = new ContainerBuilder();
            //builder.RegisterAssemblyTypes(typeof(EfRepository<>).Assembly)
            //    .Where(t => t.IsSubclassOf(typeof(EfRepository<>)))
            //    .As<IAsyncRepository<Entity>>();

            //builder.RegisterType<TodoService>().As<ITodoService>();
            //builder.RegisterType<TodoRepository>().As<ITodoRepository>();
            //builder.RegisterType<TodoTypeRepository>().As<ITodoTypeRepository>();
            //builder.RegisterType<TodoItemRepository>().As<ITodoItemRepository>();
            //builder.Populate(services);
            //var container = builder.Build();
            ////Create the IServiceProvider based on the container.
            //return new AutofacServiceProvider(container);

            //services.AddScoped<ITodoService, TodoService>();
            //services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            ConfigureInMemoryDatabases(services);

            // use real database
            // ConfigureProductionServices(services);
        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<TodoContext>(c =>
                c.UseInMemoryDatabase("Todo"));

            // Add Identity DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("Identity"));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use real database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<TodoContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            ConfigureServices(services);
        }

        private static void ConfigureCookieSettings(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Signout";
                options.Cookie = new CookieBuilder
                {
                    IsEssential = true // required for auth to work without explicit user consent; adjust to suit your privacy policy
                };
            });
        }

        private static void CreateIdentityIfNotCreated(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var existingUserManager = scope.ServiceProvider
                    .GetService<UserManager<IdentityUser>>();
                if (existingUserManager == null)
                {
                    services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddDefaultUI(UIFramework.Bootstrap4)
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                                        .AddDefaultTokenProviders();
                }
            }
        }

        public void ConfigureInject(IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ITodoTypeRepository, TodoTypeRepository>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        }

    }
}
