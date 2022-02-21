using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvlascenoLice.Entities
{
    public class OvlascenoLiceContext : DbContext
    {
        private readonly IConfiguration  configuration;
        public OvlascenoLiceContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        //ovo se direktno mapira u bazi
        public DbSet<OvlascenoLiceModel> OvlascenaLica { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OvlascenoLiceDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OvlascenoLiceModel>()
                .HasData(new OvlascenoLiceModel
                {
                    OvlascenoLiceID = Guid.Parse("5dc3dfcd-de07-4e5f-878e-a07636db322f"),
                    Ime = "Sara",
                    Prezime = "Kijanovic",
                    BrojDokumenta = "4585248",
                    BrojTable = "74474",
                    AdresaID = Guid.Parse("7280c84a-a070-4516-94e7-ef905c7dcf8b")
                });

            builder.Entity<OvlascenoLiceModel>()
                .HasData(new OvlascenoLiceModel
                {
                    OvlascenoLiceID = Guid.Parse("668e0c43-810b-4443-82a7-649b4f25a840"),
                    Ime = "Marko",
                    Prezime = "Ruzic",
                    BrojDokumenta = "465548",
                    BrojTable = "7434664",
                    AdresaID = Guid.Parse("4ead0649-3ad7-42cb-92b3-80e504006df9")
                });
        }
    }
}
