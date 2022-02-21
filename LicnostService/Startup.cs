using LicnostService.Data;
using LicnostService.Data.User;
using LicnostService.Entities;
using LicnostService.Helpers;
using LicnostService.ServiceCalls;
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LicnostService
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }
            ).AddXmlDataContractSerializerFormatters() //Dodajemo podr�ku za XML tako da ukoliko klijent to tra�i u Accept header-u zahteva mo�emo da serializujemo payload u XML u odgovoru.
            .ConfigureApiBehaviorOptions(setupAction => //Deo koji se odnosi na podr�avanje Problem Details for HTTP APIs
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    //Kreiramo problem details objekat
                    ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();

                    //Prosle�ujemo trenutni kontekst i ModelState, ovo prevodi validacione gre�ke iz ModelState-a u RFC format
                    ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                        context.HttpContext,
                        context.ModelState);

                    //Ubacujemo dodatne podatke
                    problemDetails.Detail = "Pogledajte polje errors za detalje.";
                    problemDetails.Instance = context.HttpContext.Request.Path;

                    //po defaultu se sve vra�a kao status 400 BadRequest, to je ok kada nisu u pitanju validacione gre�ke,
                    //ako jesu ho�emo da koristimo status 422 UnprocessibleEntity
                    //tra�imo info koji status kod da koristimo
                    var actionExecutiongContext = context as ActionExecutingContext;

                    //proveravamo da li postoji neka gre�ka u ModelState-u, a tako�e proveravamo da li su svi prosle�eni parametri dobro parsirani
                    //ako je sve ok parsirano ali postoje gre�ke u validaciji ho�emo da vratimo status 422
                    if ((context.ModelState.ErrorCount > 0) &&
                        (actionExecutiongContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                    {
                        problemDetails.Type = "https://google.com"; //ina�e treba da stoji link ka stranici sa detaljima gre�ke
                        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                        problemDetails.Title = "Do�lo je do gre�ke prilikom validacije.";

                        //sve vra�amo kao UnprocessibleEntity objekat
                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };

                    //ukoliko postoji ne�to �to nije moglo da se parsira ho�emo da vra�amo status 400 kao i do sada
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Do�lo je do gre�ke prilikom parsiranja poslatog sadr�aja.";
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<ILicnostRepository, LicnostRepository>(); //Nova instanca svaki put kad ide novi request, potrebno za bazu
            services.AddDbContextPool<LicnostContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LicnostDB"))); //Dodavanje konteksta za entity framework
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("LicnostOpenApiSpecification", new Microsoft.OpenApi.Models.OpenApiInfo()  //Definise naziv API-ja
                { 
                    Title = "API for operations with Licnost entity",
                    Version = "1",
                    Description = "This API lets you add, modify, delete or find licnost objects."
                });
                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
                });

                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

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
                setupAction.SwaggerEndpoint("/swagger/LicnostOpenApiSpecification/swagger.json", "Licnost entity operations API");
                setupAction.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
