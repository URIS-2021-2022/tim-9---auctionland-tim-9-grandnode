using galic_korisnik.Data;
using galic_korisnik.Entities;
using galic_korisnik.ServiceCalls;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace galic_korisnik
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
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();
            //singleton je zivotni ciklus onoga sto prosledjujemo (jednom koristimo instancu)
            services.AddScoped<IKorisnikRepository, KorisnikRepository>();
            services.AddScoped<ITipKorisnikaRepository, TipKorisnikaRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<KorisnikContext>(options => options.UseSqlServer(Configuration.GetConnectionString("KorisnikDB")));
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("KorisnikOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Korisnik API",
                        Version = "1"

                    });

                //Pomocu refleksije dobijamo ime XML fajla sa komentarima (ovako smo ga nazvali u Project -> Properties)
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";

                //Pravimo putanju do XML fajla sa komentarima
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                //Govorimo swagger-u gde se nalazi dati xml fajl sa komentarima
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                //Podesavamo endpoint gde Swagger UI moze da pronadje OpenAPI specifikaciju
                setupAction.SwaggerEndpoint("/swagger/KorisnikOpenApiSpecification/swagger.json", "Korisnik API");
                setupAction.RoutePrefix = ""; //Dokumentacija ce sada biti dostupna na root-u (ne mora da se pise /swagger)
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
