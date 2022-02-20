using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZalbaService.Entities.DataContext
{
    public class ZalbaContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ZalbaContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<TipZalbe> TipZalbe { get; set; }
        public DbSet<StatusZalbe> StatusZalbe { get; set; }
        public DbSet<Radnja> Radnja { get; set; }
        public DbSet<Zalba> Zalba { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("zalbaDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipZalbe>()
                .HasData(new
                {
                    TipZalbeId = Guid.Parse("cd155ba7-f573-4f24-b412-e41994ef8073"),
                    NazivTipa = "Žalba na tok javnog nadmetanja"
                },
                new
                {
                    TipZalbeId = Guid.Parse("1fbd2475-b35e-4e47-af39-f784c8d49497"),
                    NazivTipa = "Žalba na Odluku o davanju u zakup"
                },
                new
                {
                    TipZalbeId = Guid.Parse("c925bbc9-bbc1-4917-9aea-d5b253abc8a5"),
                    NazivTipa = "Žalba na Odluku o davanju na korišćenje"
                }
                );
            modelBuilder.Entity<StatusZalbe>()
               .HasData(new
               {
                   StatusZalbeId = Guid.Parse("212b6e83-ab50-49ec-bd95-92cd5e8f8a25"),
                   NazivStatusa = "Usvojena"
               },
               new
               {
                   StatusZalbeId = Guid.Parse("0d377d88-9e91-43c0-adfd-ecc8bd406809"),
                   NazivStatusa = "Odbijena"
               },
               new
               {
                   StatusZalbeId = Guid.Parse("b1c8cea8-c996-4344-a211-6a7e7e257e46"),
                   NazivStatusa = "Otvorena"
               }
               );
            modelBuilder.Entity<Radnja>()
               .HasData(new
               {
                   RadnjaId = Guid.Parse("b0f02aee-2bf3-43e2-bdd4-eb9e80f4bcc9"),
                   NazivRadnje = "JN ide u drugi krug sa novim uslovima"
               },
               new
               {
                   RadnjaId = Guid.Parse("62462998-fe3f-49f2-861d-9f226264beba"),
                   NazivRadnje = "JN ide u drugi krug sa starim uslovima"
               },
               new
               {
                   RadnjaId = Guid.Parse("3eeede02-9e9e-46d2-8034-d21125e45b43"),
                   NazivRadnje = "JN ne ide u drugi krug"
               }
               );
            modelBuilder.Entity<Zalba>()
              .HasData(new
              {
                  ZalbaId = Guid.Parse("007ed3b2-abb5-4bb8-90d5-f193907079ad"),
                  TipZalbe = Guid.Parse("cd155ba7-f573-4f24-b412-e41994ef8073"),
                  DatumZalbe = DateTime.Parse("2021-04-20T11:00:00"),
                  PodnosilacZalbe = Guid.Parse(""),
                  Razlog = "Krsenje pravilnika za javno nadmetanje",
                  Obrazlozenje = "Neispravnost prilikom dodeljivanja parcele",
                  DatumResenja = DateTime.Parse("2021-06-03T10:00:00"),
                  BrojResenja = "1035",
                  StatusZalbe = Guid.Parse("212b6e83-ab50-49ec-bd95-92cd5e8f8a25"),
                  BrojOdluke = "1221",
                  Radnja = Guid.Parse("3eeede02-9e9e-46d2-8034-d21125e45b43")
              }
              );
        }
    }
}
