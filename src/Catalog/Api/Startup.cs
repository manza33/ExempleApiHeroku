using System;
using System.IO;
using System.Reflection;
using Catalog.Domain.Interfaces;
using Catalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Catalog.Api
{
    public class Startup
    {
        //public Startup(IWebHostEnvironment webHostEnvironment)
        //{
        //    var builder = new ConfigurationBuilder().AddEnvironmentVariables();
        //    builder.AddUserSecrets<Startup>();
        //    Configuration = builder.Build();
        //    WebHostEnvironment = webHostEnvironment;
        //}


        public Startup(IWebHostEnvironment webHostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(webHostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{webHostEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            WebHostEnvironment = webHostEnvironment;
        }

        //public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        //{
        //    Configuration = configuration;
        //    WebHostEnvironment = webHostEnvironment;
        //}

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        private string GetHerokuConnectionString(string connectionString)
        {

            string connectionUrl = WebHostEnvironment.IsDevelopment()
                ? Configuration.GetConnectionString("ExempleApiHeroku")
                : Environment.GetEnvironmentVariable(connectionString);

            return connectionUrl;
            //var databaseUri = new Uri(connectionUrl);

            //string db = databaseUri.LocalPath.TrimStart('/');
            //string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            //return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var test = Configuration.GetConnectionString("ExempleApiHeroku");

            // Définition de l'injection (ICatalog correspond a CatalogRepo)
            services.AddTransient<ICatalogRepository>(service => new CatalogRepository(
                //Configuration.GetConnectionString("ExempleApiHeroku")
                GetHerokuConnectionString("CONNECTION_STRING")                

            ));


            services.AddSwaggerGen(
                c => {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Catalog.Api",
                        Version = "v1"
                    });
                    //c.IncludeXmlComments($"{ Assembly.GetExecutingAssembly().GetName().Name }.xml");
                    c.IncludeXmlComments(Path.Combine(
                        AppContext.BaseDirectory,
                        $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml"
                    ));
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            logger.LogInformation($"Variable : {GetHerokuConnectionString("CONNECTION_STRING")}");

            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "Catalog Api v1");
                }
            );

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
