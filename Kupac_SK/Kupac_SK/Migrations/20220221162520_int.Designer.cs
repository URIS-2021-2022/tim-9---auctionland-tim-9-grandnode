﻿// <auto-generated />
using System;
using Kupac_SK.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kupac_SK.Migrations
{
    [DbContext(typeof(KupacContext))]
    [Migration("20220221162520_int")]
    partial class @int
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kupac_SK.Entities.FizickoLice", b =>
                {
                    b.Property<Guid>("KupacID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdresaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrTel1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrTel2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojRacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DuzinaZabrane")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FizPravno")
                        .HasColumnType("bit");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JMBG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OstvarenaPovrsina")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OvlascenoLiceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PocetakZabrane")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PrestanakZabrane")
                        .HasColumnType("datetime2");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PrioritetID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UplataID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Zabrana")
                        .HasColumnType("bit");

                    b.HasKey("KupacID");

                    b.ToTable("fizLica");

                    b.HasData(
                        new
                        {
                            KupacID = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            AdresaID = "bulevar 13",
                            BrTel1 = "064111558",
                            BrTel2 = "225447",
                            BrojRacuna = "170000000082",
                            DuzinaZabrane = "0",
                            Email = "imejl@gmail.com",
                            FizPravno = true,
                            Ime = "Sara",
                            JMBG = "160999979894",
                            OstvarenaPovrsina = "15000",
                            OvlascenoLiceID = new Guid("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                            PocetakZabrane = new DateTime(1900, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            PrestanakZabrane = new DateTime(1900, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Prezime = "Kijanovic",
                            PrioritetID = new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                            UplataID = "yyyyyyyyyyyyyyy",
                            Zabrana = false
                        });
                });

            modelBuilder.Entity("Kupac_SK.Entities.KontaktOsobaModel", b =>
                {
                    b.Property<Guid>("KontaktOsobaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Funkcija")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KontaktOsobaID");

                    b.ToTable("kontaktOsoba");

                    b.HasData(
                        new
                        {
                            KontaktOsobaID = new Guid("c658a3cf-df57-4818-8a38-00b42bccc8a1"),
                            Funkcija = "Zastupnik1",
                            Ime = "Sara",
                            Prezime = "Kijanovic",
                            Telefon = " 12345687"
                        },
                        new
                        {
                            KontaktOsobaID = new Guid("b60955b8-fb83-4947-a72a-ec7050cb3454"),
                            Funkcija = "Zastupnik2",
                            Ime = "Teodora",
                            Prezime = "Kijanovic",
                            Telefon = " 18915517"
                        });
                });

            modelBuilder.Entity("Kupac_SK.Entities.PravnoLice", b =>
                {
                    b.Property<Guid>("KupacID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdresaID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrTel1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrTel2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojRacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DuzinaZabrane")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FizPravno")
                        .HasColumnType("bit");

                    b.Property<Guid>("KontaktOsoba")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MatBr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OstvarenaPovrsina")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OvlascenoLiceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PocetakZabrane")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PrestanakZabrane")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PrioritetID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UplataID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Zabrana")
                        .HasColumnType("bit");

                    b.HasKey("KupacID");

                    b.ToTable("pravnaLica");

                    b.HasData(
                        new
                        {
                            KupacID = new Guid("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                            AdresaID = "bulevar 13",
                            BrTel1 = "064111558",
                            BrTel2 = "225447",
                            BrojRacuna = "170000000082",
                            DuzinaZabrane = "0",
                            Email = "imejl@gmail.com",
                            Faks = "741258",
                            FizPravno = false,
                            KontaktOsoba = new Guid("b60955b8-fb83-4947-a72a-ec7050cb3454"),
                            MatBr = "12345678",
                            Naziv = "doo x",
                            OstvarenaPovrsina = "15000",
                            OvlascenoLiceID = new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                            PocetakZabrane = new DateTime(1900, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            PrestanakZabrane = new DateTime(1900, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            PrioritetID = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            UplataID = "xxxx",
                            Zabrana = false
                        });
                });

            modelBuilder.Entity("Kupac_SK.Entities.PrioritetModel", b =>
                {
                    b.Property<Guid>("PrioritetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OpisPrioriteta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrioritetID");

                    b.ToTable("prioriteti");

                    b.HasData(
                        new
                        {
                            PrioritetID = new Guid("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                            OpisPrioriteta = "drugi testni prioritet"
                        },
                        new
                        {
                            PrioritetID = new Guid("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                            OpisPrioriteta = "prvi testni prioritet"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
