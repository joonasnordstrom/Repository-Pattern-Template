using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PikiouAPI.Persistence.Contexts;
using System;

namespace PikiouAPI
{
    /// <summary>
    /// When application starts this one gets called
    /// </summary>
    public class Program
    { 
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetService<AppDbContext>())
            {
                try
                {
                    context.Database.EnsureCreated();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            host.Run();
        }

        // Just create a builder here, so we can customize the host in tests
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
