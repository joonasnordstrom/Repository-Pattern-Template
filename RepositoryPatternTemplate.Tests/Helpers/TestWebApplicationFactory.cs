using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PikiouAPI.Persistence.Contexts;

namespace PikiouAPI.Tests.Helpers
{
    // https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.2#customize-webapplicationfactory
    public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Setup PostgreSQL database for testing
                var dbURI = Environment.GetEnvironmentVariable("DB_CONTEXT") ?? "host=localhost;port=5432;database=pikiou_db;username=pikiou_api;password=password123";
                services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql(dbURI);
                });

                // Build the service provider
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AppDbContext>();

                    // Ensure the database is created
                    db.Database.Migrate();
                }
            });
        }
    }
}