using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UgovorService.Entities
{
    public class UgovorContext : DbContext
    {
        private readonly IConfiguration configuration;

        public UgovorContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<TipGarancijeEnt> TipGarancijeEnt { get; set; }

        public DbSet<UgovorEnt> UgovorEnt { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UgovorDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TipGarancijeEnt>()
               .HasData(new TipGarancijeEnt
               {
                   TipID = Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                   Tip = "Mesecna"
               });

            builder.Entity<TipGarancijeEnt>()
                .HasData(new TipGarancijeEnt
                {
                    TipID = Guid.Parse("5a36d34e-1620-40c6-ab8c-b96b0be49332"),
                    Tip = "Kvartalna"
                });

            builder.Entity<UgovorEnt>()
                .HasData(new UgovorEnt
                {
                    UgovorID = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    TipID = Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                    DokumentID = Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                    ZavodniBr = "471/RS",
                    JavnoNadmetanjeID = Guid.Parse("bf33a825-ad63-4e04-a812-74ffbebdadbb"),
                    DatumZavo = DateTime.Parse("2020-11-15T09:00:00"),
                    KupacID = Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8edcc"),
                    Rok = DateTime.Parse("2023-11-15T09:00:00"),
                    Mesto = "Novi Sad",
                    DatumPot = DateTime.Parse("2020-12-15T09:00:00")
                });

            builder.Entity<UgovorEnt>()
                .HasData(new UgovorEnt
                {
                    UgovorID = Guid.Parse("6f4967b3-beb3-4acf-95aa-488b16f8fc9a"),
                    TipID = Guid.Parse("5a36d34e-1620-40c6-ab8c-b96b0be49332"),
                    DokumentID = Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                    ZavodniBr = "471/RS",
                    JavnoNadmetanjeID = Guid.Parse("5a36d34e-1620-40c6-ab8c-b96b0be49333"),
                    DatumZavo = DateTime.Parse("2019-11-15T09:00:00"),
                    KupacID = Guid.Parse("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                    Rok = DateTime.Parse("2022-11-15T09:00:00"),
                    Mesto = "Beograd",
                    DatumPot = DateTime.Parse("2019-12-15T09:00:00")
                });
        }
    }
}
