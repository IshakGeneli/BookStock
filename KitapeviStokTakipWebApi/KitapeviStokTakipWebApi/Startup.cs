using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Services;
using KitapeviStokTakipWebApi.Models;
using KitapeviStokTakipWebApi.MiddleWares;
using Microsoft.AspNetCore.Authentication;
using WebApi.Helpers;

namespace KitapeviStokTakipWebApi
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
            services.AddCors();

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            // services.AddScoped<IBookService, BookService>(); 
            services.AddTransient<IBookService, BookService>(); 
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ILogService, LogService>();
            services.AddTransient<IAuthService, AuthService>();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddControllers();
            //services.AddDbContext<AuthenticationContext>(opt =>
            //{
                
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseHttpsRedirection();

            //app.UseMiddleware<BasicAuthMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
