using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Parcela.Data;
using Parcela.Entities;
using Parcela.Helper;
using Parcela.ServiceCalls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Parcela
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
            services.AddHttpClient();

            services.AddScoped<IDeoParceleRepository, DeoParceleRepository>();
            services.AddScoped<IParcelaRepository, ParcelaRepository>();
            services.AddScoped<IKatastarskaOpstinaRepository, KatastarskaOpstinaRepository>();
            services.AddScoped<IKlasaRepository, KlasaRepository>();
            services.AddScoped<IKulturaRepository, KulturaRepository>();
            services.AddScoped<IOblikSvojineRepository, OblikSvojineRepository>();
            services.AddScoped<IObradivostRepository, ObradivostRepository>();
            services.AddScoped<IOdvodnjavanjeRepository, OdvodnjavanjeRepository>();
            services.AddScoped<IZasticenaZonaRepository, ZasticenaZonaRepository>();
            services.AddScoped<IUserRepository, UserMockRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<ParcelaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ParcelaDB")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("ParcelaOpenApiSpecification", 
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                        Title = "Parcela API",
                        Version = "1"
                });
                //Pomocu refleksije dobijamo ime XML fajla sa komentarima (ovako smo ga nazvali u Project -> Properties)
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                //Pravimo putanju do XML fajla sa komentarima
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                //Govorimo swagger-u gde se nalazi dati xml fajl sa komentarima
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ParcelaOpenApiSpecification/swagger.json", "ParcelaOpenApiSpecification");
                setupAction.RoutePrefix = "";
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
