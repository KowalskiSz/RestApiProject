using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApiProject.Entitis;
using AutoMapper;
using RestApiProject.Services;
using RestApiProject.Middleware;

namespace RestApiProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            
            services.AddControllers();
            // Restaurant context, seed, mapper, errorhandlerMiddelware servicers to execute HTTP queries added to services
            services.AddDbContext<RestaurantDbContext>();
            services.AddScoped<RestaurantSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<TimeRequestMiddleware>();
            //Adding the swagger documentacion
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RestaurantSeeder seeder)
        {
            // Seeder added to configaration
            seeder.Seed(); 

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Register MIDDLEWARES 

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<TimeRequestMiddleware>();

            #endregion

            app.UseHttpsRedirection();

            //Getting access to the Swagger UI - info - Endpoints, dtos
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API Project");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
