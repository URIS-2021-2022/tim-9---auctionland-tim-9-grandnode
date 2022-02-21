using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace galic_korisnik.Entities
{
    public class KorisnikContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<TipKorisnika> TipKorisnika { get; set; }
        public DbSet<TokenTime> Tokens { get; set; }

        public KorisnikContext(DbContextOptions<KorisnikContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("KorisnikDB"));
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Korisnik>()
                .HasData(
                new Korisnik
                {
                    korisnikId = Guid.Parse("f7a20259-5aeb-3135-64ea-32cf7a96b98a"),
                    tipKorisnikaId = Guid.Parse("ce4a6a8a-b25d-d5d0-9364-3dee56521821"),
                    ime = "Petar",
                    prezime = "Petrovic",
                    korisnickoIme = "PPetrovic",
                    lozinka = "123456"
                },
                new Korisnik
                {
                    korisnikId = Guid.Parse("e8920f41-e035-da6d-27d1-ee8909f6271d"),
                    tipKorisnikaId = Guid.Parse("22caf793-fbaa-a3f5-8266-7fc3dcc798dc"),
                    ime = "Marko",
                    prezime = "Markovic",
                    korisnickoIme = "MMarkovic",
                    lozinka = "123456"
                }
                );
            builder.Entity<TipKorisnika>()
                .HasData(
                new TipKorisnika
                {
                    tipKorisnikaId = Guid.Parse("9d8004cb-fad6-40a9-9d9e-978ff4f98481"),
                    uloga = "Admin"
                }
                );
           builder.Entity<TokenTime>()
               .HasData(new
               {
                   tokenId = 1,
                   token = "token",
                   korisnikId = "e8920f41 - e035 - da6d - 27d1 - ee8909f6271d",
                   time = DateTime.Parse("2021-21-11 21:21:21")
               });
        }
    }
}
