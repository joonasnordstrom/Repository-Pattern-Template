using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPatternTemplate.Domain.Repositories;
using RepositoryPatternTemplate.Persistence.Repositories;
using RepositoryPatternTemplate.Domain.Services;
using RepositoryPatternTemplate.Persistence.Contexts;
using RepositoryPatternTemplate.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace RepositoryPatternTemplate
{
    /// <summary>
    /// This class gets called right after program has started 
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Setup PostgreSQL
            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
            {
                // Env variable will have higher priority than connection string
                var dbURI = Environment.GetEnvironmentVariable("DB_CONTEXT") ?? Configuration.GetConnectionString("DbContext");
                options.UseNpgsql(dbURI);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // TODO should these services be singleton instead of scoped ?
            services.AddScoped<ICourierRepository, CourierRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<ICourierService, CourierService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddAutoMapper();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // /swagger/v1/swagger.json is exposed by app.UseSwagger()
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pikiou API v1");
            });
            app.UseMvc();
        }
    }
}
