using Kupac_SK.Data;
using Kupac_SK.Entities;
using Kupac_SK.Helper;
using Kupac_SK.ServiceCalls_;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Kupac_SK.Helper.IAutenthicationHelper;

namespace Kupac_SK
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
                setup.ReturnHttpNotAcceptable = true
            ).AddXmlDataContractSerializerFormatters();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

           
            services.AddScoped<IKupacRepository, KupacRepository>();
            services.AddScoped<IPrioritetRepository, PrioritetRepository>();
            services.AddScoped<IKontaktOsobaRepository, KontakOsobaRepository>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddScoped<IUserRepository, UserMockRepository>();
            services.AddScoped<IFizickoLiceRepository, FizickoLiceRepository>();
            services.AddScoped<IPravnoLiceRepository, PravnoLiceRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IovlascenoliceService, ovlascenoliceService>();

            services.AddDbContext<KupacContext>(options => options.UseSqlServer(Configuration.GetConnectionString("KupacDB")));

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("DocumentOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {

                        Title = "Document API",
                        Version = "1",
                        Description = "Pomoću ovog API-ja može da se vrši manipulacija podataka u vezi sa kupcima, izmene njihovih stanja, kao i njihov pregled",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Sara Kijanovic",
                            Email = "kijanovic26@gmail.com",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        TermsOfService = new Uri("http://www.ftn.uns.ac.rs/examRegistrationTermsOfService")


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
       

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Došlo je do neočekivane greške. Molimo pokušajte kasnije.");
                    });
                });
            }

            app.UseHttpsRedirection();


            app.UseRouting();


         app.UseAuthorization();

            app.UseSwagger();
            
            app.UseSwaggerUI(setupAction =>
            {

                setupAction.SwaggerEndpoint("/swagger/DocumentOpenApiSpecification/swagger.json", "Dokument API");
                setupAction.RoutePrefix = "";
            });
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
