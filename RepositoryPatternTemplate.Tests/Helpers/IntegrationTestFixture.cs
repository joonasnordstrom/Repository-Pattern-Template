using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PikiouAPI.Mapping;
using PikiouAPI.Persistence.Contexts;

namespace PikiouAPI.Tests.Helpers
{
    public class IntegrationTestFixture : IDisposable
    {
        public readonly TestWebApplicationFactory<PikiouAPI.Startup> WebApplicationFactory;
        public readonly AppDbContext Context;
        public readonly IMapper Mapper;
        
        public IntegrationTestFixture()
        {
            WebApplicationFactory = new TestWebApplicationFactory<PikiouAPI.Startup>();
            
            var dbURI = Environment.GetEnvironmentVariable("DB_CONTEXT") ?? "host=localhost;port=5432;database=pikiou_db;username=pikiou_api;password=password123";
            var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(dbURI)
                .Options;
            Context = new AppDbContext(contextOptions);
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToRecourceProfile());
                cfg.AddProfile(new ResourceToModelProfile());
            }).CreateMapper();
        }
        
//        public static IConfiguration InitConfiguration()
//        {
//            var config = new ConfigurationBuilder()
//                .AddJsonFile("appsettings.test.json")
//                .Build();
//            return config;
//        }
        
        public void Dispose()
        {
            Context.Database.ExecuteSqlCommand("DELETE FROM \"Couriers\"");
            Context.Database.ExecuteSqlCommand("DELETE FROM  \"Orders\"");
        }
    }
}