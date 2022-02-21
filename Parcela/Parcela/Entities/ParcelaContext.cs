using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcela.Entities
{
    public class ParcelaContext : DbContext
    {
        private readonly IConfiguration configuration;
        public ParcelaContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        public DbSet<Parcela> Parcela { get; set; }
        public DbSet<DeoParcele> DeoParcele { get; set; }
        public DbSet<KatastarskaOpstina> KatastarskaOpstina { get; set; }
        public DbSet<Kultura> Kultura { get; set; }
        public DbSet<Klasa> Klasa { get; set; }
        public DbSet<Obradivost> Obradivost { get; set; }
        public DbSet<ZasticenaZona> ZasticenaZona { get; set; }
        public DbSet<OblikSvojine> OblikSvojine { get; set; }
        public DbSet<Odvodnjavanje> Odvodnjavanje { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ParcelaDB"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KatastarskaOpstina>()
                .HasData(
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("7d03a946-40a3-41e8-8feb-dbe9381d2170"),
                        NazivKatastarskeOpstine = "Čantavir"
                    },
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("5a20c276-5b98-4e24-92b4-0996cb51b293"),
                        NazivKatastarskeOpstine = "Bački Vinogradi"
                    },
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("24742b99-32c6-4999-b0a7-757a178f9ee7"),
                        NazivKatastarskeOpstine = "Bikovo"
                    },
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("55c5e423-e70c-452f-a89f-42c412163b76"),
                        NazivKatastarskeOpstine = "Đuđin"
                    },
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("714f831b-b86b-443d-abf5-f248694a8b2e"),
                        NazivKatastarskeOpstine = "Žednik"
                    },
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("835bd51d-80e3-4c28-9e34-3951bac861c7"),
                        NazivKatastarskeOpstine = "Tavankut"
                    },
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("ddc12930-390b-4e16-8c91-b9259d53bf16"),
                        NazivKatastarskeOpstine = "Bajmok"
                    },
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("a077a57d-6e63-41c8-a944-c93eb938b8f7"),
                        NazivKatastarskeOpstine = "Donji Grad"
                    },
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("26831746-3efb-48a5-8cd9-9810db3a75f4"),
                        NazivKatastarskeOpstine = "Stari Grad"
                    },
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("f0152fa7-8abc-43e9-956d-1eff5a4ace62"),
                        NazivKatastarskeOpstine = "Novi Grad"
                    },
                    new KatastarskaOpstina
                    {
                        KatastarskaOpstinaID = Guid.Parse("870742be-2358-45f2-bb1b-1d4efa6bf9d2"),
                        NazivKatastarskeOpstine = "Palić"
                    }
            );

            modelBuilder.Entity<Kultura>()
                .HasData(
                    new Kultura
                    {
                        KulturaID = Guid.Parse("562d2838-345b-46e0-977e-b1350199e46b"),
                        NazivKulture = "Njive"
                    },
                    new Kultura
                    {
                        KulturaID = Guid.Parse("429b3bd9-c9e4-42e5-a94d-dcf10276756d"),
                        NazivKulture = "Vrtovi"
                    },
                    new Kultura
                    {
                        KulturaID = Guid.Parse("c52c698e-ab99-4b69-bd7f-80addbd72e10"),
                        NazivKulture = "Voćnjaci"
                    },
                    new Kultura
                    {
                        KulturaID = Guid.Parse("d54ae671-92eb-4892-8579-e91ef4a53f3e"),
                        NazivKulture = "Vinogradi"
                    },
                    new Kultura
                    {
                        KulturaID = Guid.Parse("a644ca6e-89c2-4b16-84d8-6f5b76909ad2"),
                        NazivKulture = "Livade"
                    },
                    new Kultura
                    {
                        KulturaID = Guid.Parse("101395f3-0ab9-4952-a691-530f14fa443d"),
                        NazivKulture = "Pašnjaci"
                    },
                    new Kultura
                    {
                        KulturaID = Guid.Parse("fc963149-30b2-4038-b5f0-bdaff5e332c2"),
                        NazivKulture = "Šume"
                    },
                    new Kultura
                    {
                        KulturaID = Guid.Parse("21b610b2-50fd-478e-b649-8d67694618c6"),
                        NazivKulture = "Trstici-močvare"
                    }
            );

            modelBuilder.Entity<Klasa>()
                .HasData(
                    new Klasa
                    {
                        KlasaID = Guid.Parse("5c3ad689-4409-4e4d-8e93-fd452776c770"),
                        NazivKlase = "I"
                    },
                    new Klasa
                    {
                        KlasaID = Guid.Parse("5af4b60e-1aaa-46c8-a4a0-550e602e633a"),
                        NazivKlase = "II"
                    },
                    new Klasa
                    {
                        KlasaID = Guid.Parse("7cfbb7c4-6a83-4a69-bc73-5cacca0c01fd"),
                        NazivKlase = "III"
                    },
                    new Klasa
                    {
                        KlasaID = Guid.Parse("4a0f4309-15ed-428b-b1a6-67245d316739"),
                        NazivKlase = "IV"
                    },
                    new Klasa
                    {
                        KlasaID = Guid.Parse("2c063dc7-fc37-4071-a1be-60f574184909"),
                        NazivKlase = "V"
                    },
                    new Klasa
                    {
                        KlasaID = Guid.Parse("480a44e0-dc28-4d2b-8013-96326d0f9982"),
                        NazivKlase = "VI"
                    },
                    new Klasa
                    {
                        KlasaID = Guid.Parse("ae9b7c8e-3f80-43b6-bc97-14f50bee4fb7"),
                        NazivKlase = "VII"
                    },
                    new Klasa
                    {
                        KlasaID = Guid.Parse("a95ea9b3-4eef-495e-9bcd-3ede006d7609"),
                        NazivKlase = "VIII"
                    }
            );

            modelBuilder.Entity<Obradivost>()
                .HasData(
                    new Obradivost
                    {
                        ObradivostID = Guid.Parse("9e1a4745-8838-4130-8662-4337d153a5fd"),
                        NazivObradivosti = "Obradivo"
                    },
                    new Obradivost
                    {
                        ObradivostID = Guid.Parse("8a8d331a-3553-4de1-a8aa-c4a70e5b7765"),
                        NazivObradivosti = "Ostalo"
                    }
            );

            modelBuilder.Entity<ZasticenaZona>()
                .HasData(
                    new ZasticenaZona
                    {
                        ZasticenaZonaID = Guid.Parse("70c4f5fe-694f-4879-84ca-de5744bbd967"),
                        NazivZasticeneZone = "1"
                    },
                    new ZasticenaZona
                    {
                        ZasticenaZonaID = Guid.Parse("7801b5f4-63ef-4990-b039-efa5afc22d1d"),
                        NazivZasticeneZone = "2"
                    },
                    new ZasticenaZona
                    {
                        ZasticenaZonaID = Guid.Parse("308a9704-2235-4310-9092-9cde4a40164b"),
                        NazivZasticeneZone = "3"
                    },
                    new ZasticenaZona
                    {
                        ZasticenaZonaID = Guid.Parse("25fb6ccc-6713-44e3-8234-44d680fc1b5f"),
                        NazivZasticeneZone = "4"
                    }
            );

            modelBuilder.Entity<OblikSvojine>()
                .HasData(
                    new OblikSvojine
                    {
                        OblikSvojineID = Guid.Parse("84226e05-ec99-4c6c-827f-f0ccf63a0990"),
                        NazivOblikaSvojine = "Privatna svojina"
                    },
                    new OblikSvojine
                    {
                        OblikSvojineID = Guid.Parse("36e7d2b4-188a-4560-8325-c8b591afcce4"),
                        NazivOblikaSvojine = "Državna svojina RS"
                    },
                    new OblikSvojine
                    {
                        OblikSvojineID = Guid.Parse("2994163c-e08a-4647-996b-86eda5e0be7a"),
                        NazivOblikaSvojine = "Državna svojina"
                    },
                    new OblikSvojine
                    {
                        OblikSvojineID = Guid.Parse("4938990b-fee0-464f-8fa7-835a0d1e71e1"),
                        NazivOblikaSvojine = "Društvena svojina"
                    },
                    new OblikSvojine
                    {
                        OblikSvojineID = Guid.Parse("38f76c13-8b55-45f6-bf1a-7adfa9007aba"),
                        NazivOblikaSvojine = "Zadružna svojina"
                    },
                    new OblikSvojine
                    {
                        OblikSvojineID = Guid.Parse("cf1e7bd8-2a93-4a08-9768-57e172643630"),
                        NazivOblikaSvojine = "Mešovita svojina"
                    },
                    new OblikSvojine
                    {
                        OblikSvojineID = Guid.Parse("718462dc-15e4-4dce-81cf-e150ce5846bd"),
                        NazivOblikaSvojine = "Drugi oblici"
                    }
            );

            modelBuilder.Entity<Odvodnjavanje>()
                .HasData(
                    new Odvodnjavanje
                    {
                        OdvodnjavanjeID = Guid.Parse("814f8e70-0edf-4cf5-8729-5091f7590b68"),
                        NazivOdvodnjavanja = "Površinsko odvodnjavanje"
                    },
                    new Odvodnjavanje
                    {
                        OdvodnjavanjeID = Guid.Parse("0f18b42e-3451-426d-8720-cdd50110989b"),
                        NazivOdvodnjavanja = "Podzemno odvodnjavanje"
                    }
            );

            modelBuilder.Entity<Parcela>()
                .HasData(
                    new Parcela
                    {
                        ParcelaID = Guid.Parse("ae463837-b971-4354-9f40-92cc819edf25"),
                        KorisnikParceleID = Guid.Parse("efb3be0f-7082-4998-858d-51340d2abbab"),
                        Povrsina = 100,
                        BrojParcele = "PC-2601",
                        KatastarskaOpstinaID = Guid.Parse("870742be-2358-45f2-bb1b-1d4efa6bf9d2"),
                        BrojListaNepokretnosti = "LN101",
                        KulturaID = Guid.Parse("a644ca6e-89c2-4b16-84d8-6f5b76909ad2"),
                        KlasaID = Guid.Parse("5c3ad689-4409-4e4d-8e93-fd452776c770"),
                        ObradivostID = Guid.Parse("9e1a4745-8838-4130-8662-4337d153a5fd"),
                        ZasticenaZonaID = Guid.Parse("308a9704-2235-4310-9092-9cde4a40164b"),
                        OblikSvojineID = Guid.Parse("84226e05-ec99-4c6c-827f-f0ccf63a0990"),
                        OdvodnjavanjeID = Guid.Parse("0f18b42e-3451-426d-8720-cdd50110989b"),
                        KulturaStvarnoStanje = "",
                        KlasaStvarnoStanje = "",
                        ObradivostStvarnoStanje = "",
                        ZasticenaZonaStvarnoStanje = "",
                        OdvodnjavanjeStvarnoStanje = ""
                    }
            );

            modelBuilder.Entity<DeoParcele>()
                .HasData(
                    new DeoParcele
                    {
                        DeoParceleID = Guid.Parse("bdafb306-0e28-4dd3-ad97-14532d86552c"),
                        ParcelaID = Guid.Parse("ae463837-b971-4354-9f40-92cc819edf25"),
                        IdealniDeoParcele = 100,
                        StvarniDeoParcele = 100
                    }
            );
        }

    }
}
