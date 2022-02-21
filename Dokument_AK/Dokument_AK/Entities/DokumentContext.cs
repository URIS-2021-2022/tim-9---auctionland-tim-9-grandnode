using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dokument_AK.Entities
{
    public class DokumentContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DokumentContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<StatusDokumentaEnt> StatusDokumentaEnt { get; set; }

        public DbSet<DokumentEnt> DokumentEnt { get; set; }

        public DbSet<InterniDokumentEnt> InterniDokumentEnt { get; set; }

        public DbSet<EksterniDokumentEnt> EksterniDokumentEnt { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DokumentDB"));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
             builder.Entity<StatusDokumentaEnt>()
                .HasData(new StatusDokumentaEnt
                {
                    StatusDokID = Guid.Parse("2f530032-429e-4be7-b202-d800876d393d"),
                    Usvojen = true,
                    Odbijen = false,
                    Otvoren = false
                });

            builder.Entity<StatusDokumentaEnt>()
                .HasData(new StatusDokumentaEnt
                {
                    StatusDokID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    Usvojen = false,
                    Odbijen = false,
                    Otvoren = true
                });

            builder.Entity<DokumentEnt>()
                .HasData(new DokumentEnt
                {
                    DokumentID = Guid.Parse("1794d8c7-6c5c-4725-9d92-d819bdc07773"),
                    StatusDokID = Guid.Parse("2f530032-429e-4be7-b202-d800876d393d"),
                    ZavodniBroj = "15548/RS7",
                    Datum = DateTime.Parse("2021-11-15T09:00:00"),
                    DatumDonosenjaDokumenta = DateTime.Parse("2021-12-15T09:00:00")
                });

            builder.Entity<DokumentEnt>()
                .HasData(new DokumentEnt
                {
                    DokumentID = Guid.Parse("cfe84b37-bb6d-498d-a546-5dee8758ed1a"),
                    StatusDokID = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    ZavodniBroj = "17748/RS7",
                    Datum = DateTime.Parse("2019-11-15T09:00:00"),
                    DatumDonosenjaDokumenta = DateTime.Parse("2019-12-15T09:00:00")
                });

            builder.Entity<InterniDokumentEnt>()
                .HasData(new InterniDokumentEnt
                {
                    DokumentID = Guid.Parse("1794d8c7-6c5c-4725-9d92-d819bdc07773"),
                    Izmenjen = true
                });

            builder.Entity<EksterniDokumentEnt>()
                .HasData(new EksterniDokumentEnt
                {
                    DokumentID = Guid.Parse("cfe84b37-bb6d-498d-a546-5dee8758ed1a"),
                    Izmenjen = false
                });
        }
    }
}
