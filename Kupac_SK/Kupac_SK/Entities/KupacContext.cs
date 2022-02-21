using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kupac_SK.Entities
{
    public class KupacContext : DbContext
    {
        private readonly IConfiguration configuration;
        public KupacContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        //sve tabele koje imamo 
        public DbSet<FizickoLice> fizLica { get; set; }
        public DbSet<PravnoLice> pravnaLica { get; set; }
        public DbSet<PrioritetModel> prioriteti { get; set; }
        public DbSet<KontaktOsobaModel> kontaktOsoba { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("KupacDB"));
        }
        //punjenje

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //prioriteti
            builder.Entity<PrioritetModel>().HasData(new
            {
                PrioritetID = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                OpisPrioriteta = "drugi testni prioritet"
            });

            builder.Entity<PrioritetModel>().HasData(new
            {
                PrioritetID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                OpisPrioriteta = "prvi testni prioritet"
            });
            //prioriteti

            //kontaktOsobe
            builder.Entity<KontaktOsobaModel>().HasData(new
            {
                KontaktOsobaID = Guid.Parse("c658a3cf-df57-4818-8a38-00b42bccc8a1"),
                Ime = "Sara",
                Prezime = "Kijanovic",
                Funkcija = "Zastupnik1",
                Telefon = " 12345687"
            });

            builder.Entity<KontaktOsobaModel>().HasData(new
            {
                KontaktOsobaID = Guid.Parse("b60955b8-fb83-4947-a72a-ec7050cb3454"),
                Ime = "Teodora",
                Prezime = "Kijanovic",
                Funkcija = "Zastupnik2",
                Telefon = " 18915517"
            });

            //kontaktosobe 

            //pravna lica

            builder.Entity<FizickoLice>().HasData(new
            {
                KupacID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                FizPravno = true,
                OstvarenaPovrsina = "15000",
                Zabrana = false,
                PocetakZabrane = DateTime.Parse("1900-01-01T09:00:00"),
                DuzinaZabrane = "0",
                PrestanakZabrane = DateTime.Parse("1900-01-01T09:00:00"),
                OvlascenoLiceID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                PrioritetID = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                BrTel1 = "064111558",
                BrTel2 = "225447",
                Email = "imejl@gmail.com",
                AdresaID = "bulevar 13",
                UplataID = "yyyyyyyyyyyyyyy",
                BrojRacuna = "170000000082",
                JMBG = "160999979894",
                Ime = "Sara",
                Prezime = "Kijanovic"

            });
        
            //pravna lica

            //fizicka lica
            builder.Entity<PravnoLice>().HasData(new
            {
                KupacID = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                FizPravno = false,
                OstvarenaPovrsina = "15000",
                Zabrana = false,
                PocetakZabrane = DateTime.Parse("1900-01-01T09:00:00"),
                DuzinaZabrane = "0",
                PrestanakZabrane = DateTime.Parse("1900-01-01T09:00:00"),
                OvlascenoLiceID = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                PrioritetID = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                BrTel1 = "064111558",
                BrTel2 = "225447",
                Email = "imejl@gmail.com",
                AdresaID = "bulevar 13",
                UplataID = "xxxx",
                BrojRacuna = "170000000082",
                Naziv = "doo x",
                MatBr = "12345678",
                Faks = "741258",
                KontaktOsoba = Guid.Parse("b60955b8-fb83-4947-a72a-ec7050cb3454")
            });
          

            //fizicka lica 




        }
    }
}
