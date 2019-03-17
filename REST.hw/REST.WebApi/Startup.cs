using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using REST.BLL.Models;
using REST.BLL.Profiles;
using REST.BLL.Services;
using REST.DataAccess;
using REST.DataAccess.Contexts;
using REST.DataAccess.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace REST.WebApi
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => 
                    fv.RegisterValidatorsFromAssemblyContaining<UpdatePersonRequestValidator>());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "REST.Homework",
                    Description = "Testing"
                });
                c.IncludeXmlComments(
                    System.IO.Path.Combine(
                        System.AppContext.BaseDirectory, "REST.WebApi.xml"));

            });
            Mapper.Initialize(cfg => cfg.AddProfile<PersonProfile>());
            
            services.AddDbContext<PersonContext>();
            services.AddAutoMapper();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IPersonService, PersonService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
