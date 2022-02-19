using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesto.Entities
{
    public class MestoContext : DbContext
    {
        private readonly IConfiguration configuration;
        public MestoContext(DbContextOptions<MestoContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        public DbSet<Adresa> Adresa { get; set; }
        public DbSet<Drzava> Drzava { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MestoDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresa>()
                .HasData(
                new Adresa
                {
                    AdresaId = Guid.Parse("814f8e70-0edf-4cf5-8729-5091f7590b68"),
                    Mesto = "SM",
                    PostanskiBroj=2200,
                    Ulica="SAve kovacevica 25"
                }
                );
            modelBuilder.Entity<Drzava>()
                .HasData(
                new Drzava
                {
                    DrzavaId= Guid.Parse("24742b99-32c6-4999-b0a7-757a178f9ee7"),
                    Naziv = "Srbija"
                }
                );
        }
    }
}
