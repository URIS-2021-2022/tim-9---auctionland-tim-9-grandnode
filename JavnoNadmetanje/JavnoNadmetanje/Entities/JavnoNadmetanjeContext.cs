using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JavnoNadmetanje.Entities
{
    public class JavnoNadmetanjeContext : DbContext
    {
        //private readonly IConfiguration configuration;

        public JavnoNadmetanjeContext(DbContextOptions<JavnoNadmetanjeContext> options) : base(options)
        {

        }

        public DbSet<TipJavnogNadmetanja> TipoviJavnihNadmetanja { get; set; }

        public DbSet<StatusNadmetanja> StatusiNadmetanja { get; set; }

        public DbSet<JavnoNadmetanje> JavnaNadmetanja { get; set; }

        public DbSet<Licitacija> Licitacije { get; set; }

        /// <summary>
        /// Popunjava bazu podataka inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TipJavnogNadmetanja>()
              .HasData(new
              {
                  TipJavnogNadmetanjaID = Guid.Parse("4246a611-7b2f-429d-a9ba-0e539c81b82f"),
                  NazivTipaJavnogNadmetanja = "Javna licitacija"
              });
            builder.Entity<TipJavnogNadmetanja>()
              .HasData(new
              {
                  TipJavnogNadmetanjaID = Guid.Parse("99b6d6ec-4358-4898-936b-31b31d236324"),
                  NazivTipaJavnogNadmetanja = "Otvaranje zatvorenih ponuda"
              });

            builder.Entity<StatusNadmetanja>()
               .HasData(new
               {
                   StatusNadmetanjaID = Guid.Parse("8aaa90c8-56f3-4a76-b07a-f895eded5a84"),
                   NazivStatusaNadmetanja = "Prvi krug"
               });
            builder.Entity<StatusNadmetanja>()
              .HasData(new
              {
                  StatusNadmetanjaID = Guid.Parse("b1ad846b-f76f-4485-8c89-08e2dfebd112"),
                  NazivStatusaNadmetanja = "Drugi krug sa starim uslovima"
              });
            builder.Entity<StatusNadmetanja>()
              .HasData(new
              {
                  StatusNadmetanjaID = Guid.Parse("d85b4a71-27e4-4626-9a3e-0412430e03d6"),
                  NazivStatusaNadmetanja = "Drugi krug sa novim uslovima"
              });
            
            builder.Entity<JavnoNadmetanje>()
                .HasData(new
                {
                    JavnoNadmetanjeID = Guid.Parse("208a48a5-371c-4f9d-ac23-18bb176ff8f3"),
                    Datum = DateTime.Parse("2022-2-17"),
                    VremePocetka = DateTime.Parse("2022-2-17T08:00:00"),//godina, mesec, dan, sat, minut, sekunda
                    VremeKraja = DateTime.Parse("2022-2-17T10:00:00"),
                    PocetnaCenaPoHektaru = 5000,
                    Izuzeto = false,
                    TipJavnogNadmetanjaID = Guid.Parse("4246a611-7b2f-429d-a9ba-0e539c81b82f"),
                    IzlicitiranaCena = 7500,
                    PeriodZakupa = 12,
                    BrojUcesnika = 10,
                    VisinaDopuneDepozita = 500,
                    Krug = 1,
                    StatusNadmetanjaID = Guid.Parse("8aaa90c8-56f3-4a76-b07a-f895eded5a84"),
                    AdresaID = Guid.Parse("a06f99d2-0ba7-40ff-a241-304a03dfe4be"),
                    OvlascenoLiceID = Guid.Parse("5cfa282f-8324-4a8b-8c23-8d43502ca01e"),
                    //ParceleID = new List<Guid>() { Guid.Parse("90d32f0d-a5ea-4a9a-98e8-0820104a1196") },
                    KupacID = Guid.Parse("8b3b7775-4293-4b41-9ccc-19f9cf694d68"),
                    //KupciID = new List<Guid>() { Guid.Parse("93cb5df3-e411-4917-bbe1-26018df7a7cb") }
                });

            builder.Entity<JavnoNadmetanje>()
               .HasData(new
               {
                   JavnoNadmetanjeID = Guid.Parse("13d6ced2-ab84-4132-bf67-e96037f4813d"),
                   Datum = DateTime.Parse("2022-2-18"),
                   VremePocetka = DateTime.Parse("2022-2-18T08:00:00"),
                   VremeKraja = DateTime.Parse("2022-2-18T10:00:00"),
                   PocetnaCenaPoHektaru = 4000,
                   Izuzeto = false,
                   TipJavnogNadmetanjaID = Guid.Parse("4246a611-7b2f-429d-a9ba-0e539c81b82f"),
                   IzlicitiranaCena = 6000,
                   PeriodZakupa = 12,
                   BrojUcesnika = 10,
                   VisinaDopuneDepozita = 400,
                   Krug = 1,
                   StatusNadmetanjaID = Guid.Parse("8aaa90c8-56f3-4a76-b07a-f895eded5a84"),
                   AdresaID = Guid.Parse("a06f99d2-0ba7-40ff-a241-304a03dfe4be"),
                   OvlascenoLiceID = Guid.Parse("5cfa282f-8324-4a8b-8c23-8d43502ca01e"),
                   //ParceleID = new List<Guid>() { Guid.Parse("90d32f0d-a5ea-4a9a-98e8-0820104a1196") },
                   KupacID = Guid.Parse("8b3b7775-4293-4b41-9ccc-19f9cf694d68"),
                   //KupciID = new List<Guid>() { Guid.Parse("93cb5df3-e411-4917-bbe1-26018df7a7cb") }
               });

            builder.Entity<Licitacija>()
                .HasData(new
                {
                    LicitacijaID = Guid.Parse("a215e4cb-a427-40cf-88b2-8488d140a939"),
                    Broj = 1,
                    Godina = 2022,
                    Datum = DateTime.Parse("2022-2-17"),
                    Ogranicenja = 1,
                    KorakCene = 100,
                    ListaDokumentacijeFizickaLica = new List<string>() { "dok1_fl", "dok2_fl" },
                    ListaDokumentacijePravnaLica = new List<string>() { "dok1_pl", "dok1_pl" },
                    JavnoNadmetanjeID = Guid.Parse("208a48a5-371c-4f9d-ac23-18bb176ff8f3"),
                    RokPrijava = DateTime.Parse("2022-2-15")

                });
            builder.Entity<Licitacija>()
               .HasData(new
               {
                   LicitacijaID = Guid.Parse("1de13266-85e8-4120-8b1f-daacc32c5811"),
                   Broj = 2,
                   Godina = 2022,
                   Datum = DateTime.Parse("2022-2-18"),
                   Ogranicenja = 1,
                   KorakCene = 200,
                   ListaDokumentacijeFizickaLica = new List<string>() { "dok1_fl", "dok2_fl" },
                   ListaDokumentacijePravnaLica = new List<string>() { "dok1_pl", "dok1_pl" },
                   JavnoNadmetanjeID = Guid.Parse("208a48a5-371c-4f9d-ac23-18bb176ff8f3"),
                   RokPrijava = DateTime.Parse("2022-2-16")
               });
        }
    }
}
