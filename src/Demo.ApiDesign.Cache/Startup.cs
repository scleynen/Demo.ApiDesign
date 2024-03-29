﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.ApiDesign.Cache.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo.ApiDesign.Cache
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
            // Database connection string.
            var connection = @"Server=db;Database=Demo;User=sa;Password=MySecret_9000;";

            // This line uses 'UseSqlServer' in the 'options' parameter
            // with the connection string defined above.
            services.AddDbContext<DemoDbContext>(options => options.UseSqlServer(connection));

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //options.ApiVersionReader = new QueryStringApiVersionReader("v");
                //options.ApiVersionReader = new HeaderApiVersionReader("api-version");
                options.ApiVersionReader = new CustomMediaTypeApiVersionReader();
            });


            services.AddMvc(options =>
            {
                //Use COntent negotiation
                options.RespectBrowserAcceptHeader = true;
                //When content-type not accepted --> return 406
                options.ReturnHttpNotAcceptable = true;
                var jsonOutputFormatter = options.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                jsonOutputFormatter?.SupportedMediaTypes.Add("application/vnd.product.v1");
                jsonOutputFormatter?.SupportedMediaTypes.Add("application/vnd.product.v2");

                
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddResponseCaching();
           // services.AddHttpCacheHeaders(expirationModelOptions =>
           //{
           //    expirationModelOptions.MaxAge = 60;
           //    expirationModelOptions.SharedMaxAge = 30;
           //},
           //     validationModelOptions =>
           //     {
           //         validationModelOptions.MustRevalidate = true;
           //         validationModelOptions.ProxyRevalidate = true;
           //     }
           // );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            }



            app.UseHttpsRedirection();
            // add Microsoft's ResponseCaching middleware to the request pipeline (with InMemory cache store)
            app.UseResponseCaching();

            // add HttpCacheHeaders middleware to the request pipeline
            //app.UseHttpCacheHeaders();
            app.UseMvc();
        }
    }
}
