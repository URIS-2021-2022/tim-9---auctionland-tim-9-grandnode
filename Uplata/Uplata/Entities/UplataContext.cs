using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uplata.Entities
{
    public class UplataContext : DbContext
    {
        
        public UplataContext(DbContextOptions<UplataContext> options) : base(options)
        {

        }

        public DbSet<KursnaLista> KursneListe { get; set; }

        public DbSet<Uplata> Uplate { get; set; }

        /// <summary>
        /// Popunjava bazu podataka inicijalnim podacima
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KursnaLista>()
              .HasData(new
              {
                  KursnaListaID = Guid.Parse("c8a3972c-ed80-4030-a6a3-61c37cc5b36d"),
                  Datum = DateTime.Parse("2022-2-17"),
                  Valuta = "RSD",
                  Vrednost = float.Parse("1234")
              });
            modelBuilder.Entity<KursnaLista>()
              .HasData(new
              {
                  KursnaListaID = Guid.Parse("f9d0d94c-a332-4437-a8d1-e2b64349e0ad"),
                  Datum = DateTime.Parse("2022-2-15"),
                  Valuta = "RSD",
                  Vrednost = float.Parse("4321")
              });

            modelBuilder.Entity<Uplata>()
                .HasData(new
                {
                    //UplataID = Guid.Parse("608eba57-ec53-4286-b745-b4db269a611c"),
                    UplataID = Guid.Parse("556228f6-4afd-4a7a-9767-371b608abaab"),
                    BrojRacuna = "236541",
                    PozivNaBroj = "147852",
                    Iznos = float.Parse("4321"),
                    SvrhaUplate = "Uplata javnog nadmetanja",
                    Datum = DateTime.Parse("2022-2-18"),
                    KursnaListaID = Guid.Parse("c8a3972c-ed80-4030-a6a3-61c37cc5b36d"),
                    KupacID = Guid.Parse("8aaa90c8-56f3-4a76-b07a-f895eded5a84"),
                    JavnoNadmetanjeID = Guid.Parse("13d6ced2-ab84-4132-bf67-e96037f4813d")
                });

            modelBuilder.Entity<Uplata>()
               .HasData(new
               {
                   //UplataID = Guid.Parse("608eba57-ec53-4286-b745-b4db269a611c"),
                   UplataID = Guid.Parse("3c08638f-86c4-4744-a6fa-e0863adccdc6"),
                   BrojRacuna = "236541",
                   PozivNaBroj = "147852",
                   Iznos = float.Parse("4321"),
                   SvrhaUplate = "Uplata javnog nadmetanja",
                   Datum = DateTime.Parse("2022-2-18"),
                   KursnaListaID = Guid.Parse("c8a3972c-ed80-4030-a6a3-61c37cc5b36d"),
                   KupacID = Guid.Parse("d371e7b7-9b08-4831-a300-0df4c200a3d8"),
                   JavnoNadmetanjeID = Guid.Parse("13d6ced2-ab84-4132-bf67-e96037f4813d")
               });
        }
    }
}
