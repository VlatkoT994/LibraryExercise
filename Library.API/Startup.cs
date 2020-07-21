using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.API.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Library.API.Services;
using Newtonsoft.Json.Serialization;
using AutoMapper;

namespace Library.API
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
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                setupAction.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            })
            .AddXmlDataContractSerializerFormatters();

            services.AddCors();
            

            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=LibraryDB;Trusted_Connection=True;");
                    
            });
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
