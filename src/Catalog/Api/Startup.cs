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


        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        private string GetHerokuConnectionString(string connectionString)
        {
            //string connectionUrl = Environment.GetEnvironmentVariable(connectionString);
            //string connectionUrl = Env.IsDevelopment()
            //    ? Configuration.GetConnectionString("ExempleApiHeroku")
            //    : Environment.GetEnvironmentVariable(connectionString);
            if (Env.IsDevelopment())
            {
                return Environment.GetEnvironmentVariable(connectionString);
            }
            else
            {
                var databaseUri = new Uri(Environment.GetEnvironmentVariable(connectionString).Replace("\"", ""));

                string db = databaseUri.LocalPath.TrimStart('/');
                string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

                return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var dbUrl = GetHerokuConnectionString("ENV_DATABASE_URL");
            // Définition de l'injection (ICatalog correspond a CatalogRepo)
            services.AddTransient<ICatalogRepository>(service => new CatalogRepository(
            //Configuration.GetConnectionString("ExempleApiHeroku")
            dbUrl            

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

            logger.LogWarning($"Variable : {Environment.GetEnvironmentVariable("ENV_DATABASE_URL")}");

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
