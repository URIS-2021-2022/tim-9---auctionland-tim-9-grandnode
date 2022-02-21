﻿// <auto-generated />
using System;
using JavnoNadmetanje.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JavnoNadmetanje.Migrations
{
    [DbContext(typeof(JavnoNadmetanjeContext))]
    [Migration("20220220120028_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JavnoNadmetanje.Entities.JavnoNadmetanje", b =>
                {
                    b.Property<Guid>("JavnoNadmetanjeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdresaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BrojUcesnika")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("IzlicitiranaCena")
                        .HasColumnType("int");

                    b.Property<bool>("Izuzeto")
                        .HasColumnType("bit");

                    b.Property<int>("Krug")
                        .HasColumnType("int");

                    b.Property<Guid>("KupacID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OvlascenoLiceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PeriodZakupa")
                        .HasColumnType("int");

                    b.Property<int>("PocetnaCenaPoHektaru")
                        .HasColumnType("int");

                    b.Property<Guid>("StatusNadmetanjaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TipJavnogNadmetanjaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("VisinaDopuneDepozita")
                        .HasColumnType("int");

                    b.Property<DateTime>("VremeKraja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("VremePocetka")
                        .HasColumnType("datetime2");

                    b.HasKey("JavnoNadmetanjeID");

                    b.HasIndex("StatusNadmetanjaID");

                    b.HasIndex("TipJavnogNadmetanjaID");

                    b.ToTable("JavnaNadmetanja");

                    b.HasData(
                        new
                        {
                            JavnoNadmetanjeID = new Guid("208a48a5-371c-4f9d-ac23-18bb176ff8f3"),
                            AdresaID = new Guid("a06f99d2-0ba7-40ff-a241-304a03dfe4be"),
                            BrojUcesnika = 10,
                            Datum = new DateTime(2022, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IzlicitiranaCena = 7500,
                            Izuzeto = false,
                            Krug = 1,
                            KupacID = new Guid("8b3b7775-4293-4b41-9ccc-19f9cf694d68"),
                            OvlascenoLiceID = new Guid("5cfa282f-8324-4a8b-8c23-8d43502ca01e"),
                            PeriodZakupa = 12,
                            PocetnaCenaPoHektaru = 5000,
                            StatusNadmetanjaID = new Guid("8aaa90c8-56f3-4a76-b07a-f895eded5a84"),
                            TipJavnogNadmetanjaID = new Guid("4246a611-7b2f-429d-a9ba-0e539c81b82f"),
                            VisinaDopuneDepozita = 500,
                            VremeKraja = new DateTime(2022, 2, 17, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            VremePocetka = new DateTime(2022, 2, 17, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            JavnoNadmetanjeID = new Guid("13d6ced2-ab84-4132-bf67-e96037f4813d"),
                            AdresaID = new Guid("a06f99d2-0ba7-40ff-a241-304a03dfe4be"),
                            BrojUcesnika = 10,
                            Datum = new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IzlicitiranaCena = 6000,
                            Izuzeto = false,
                            Krug = 1,
                            KupacID = new Guid("8b3b7775-4293-4b41-9ccc-19f9cf694d68"),
                            OvlascenoLiceID = new Guid("5cfa282f-8324-4a8b-8c23-8d43502ca01e"),
                            PeriodZakupa = 12,
                            PocetnaCenaPoHektaru = 4000,
                            StatusNadmetanjaID = new Guid("8aaa90c8-56f3-4a76-b07a-f895eded5a84"),
                            TipJavnogNadmetanjaID = new Guid("4246a611-7b2f-429d-a9ba-0e539c81b82f"),
                            VisinaDopuneDepozita = 400,
                            VremeKraja = new DateTime(2022, 2, 18, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            VremePocetka = new DateTime(2022, 2, 18, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("JavnoNadmetanje.Entities.Licitacija", b =>
                {
                    b.Property<Guid>("LicitacijaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Broj")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("Godina")
                        .HasColumnType("int");

                    b.Property<Guid>("JavnoNadmetanjeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("KorakCene")
                        .HasColumnType("int");

                    b.Property<int>("Ogranicenja")
                        .HasColumnType("int");

                    b.Property<DateTime>("RokPrijava")
                        .HasColumnType("datetime2");

                    b.HasKey("LicitacijaID");

                    b.HasIndex("JavnoNadmetanjeID");

                    b.ToTable("Licitacije");

                    b.HasData(
                        new
                        {
                            LicitacijaID = new Guid("a215e4cb-a427-40cf-88b2-8488d140a939"),
                            Broj = 1,
                            Datum = new DateTime(2022, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Godina = 2022,
                            JavnoNadmetanjeID = new Guid("208a48a5-371c-4f9d-ac23-18bb176ff8f3"),
                            KorakCene = 100,
                            Ogranicenja = 1,
                            RokPrijava = new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            LicitacijaID = new Guid("1de13266-85e8-4120-8b1f-daacc32c5811"),
                            Broj = 2,
                            Datum = new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Godina = 2022,
                            JavnoNadmetanjeID = new Guid("208a48a5-371c-4f9d-ac23-18bb176ff8f3"),
                            KorakCene = 200,
                            Ogranicenja = 1,
                            RokPrijava = new DateTime(2022, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("JavnoNadmetanje.Entities.StatusNadmetanja", b =>
                {
                    b.Property<Guid>("StatusNadmetanjaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivStatusaNadmetanja")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusNadmetanjaID");

                    b.ToTable("StatusiNadmetanja");

                    b.HasData(
                        new
                        {
                            StatusNadmetanjaID = new Guid("8aaa90c8-56f3-4a76-b07a-f895eded5a84"),
                            NazivStatusaNadmetanja = "Prvi krug"
                        },
                        new
                        {
                            StatusNadmetanjaID = new Guid("b1ad846b-f76f-4485-8c89-08e2dfebd112"),
                            NazivStatusaNadmetanja = "Drugi krug sa starim uslovima"
                        },
                        new
                        {
                            StatusNadmetanjaID = new Guid("d85b4a71-27e4-4626-9a3e-0412430e03d6"),
                            NazivStatusaNadmetanja = "Drugi krug sa novim uslovima"
                        });
                });

            modelBuilder.Entity("JavnoNadmetanje.Entities.TipJavnogNadmetanja", b =>
                {
                    b.Property<Guid>("TipJavnogNadmetanjaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivTipaJavnogNadmetanja")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipJavnogNadmetanjaID");

                    b.ToTable("TipoviJavnihNadmetanja");

                    b.HasData(
                        new
                        {
                            TipJavnogNadmetanjaID = new Guid("4246a611-7b2f-429d-a9ba-0e539c81b82f"),
                            NazivTipaJavnogNadmetanja = "Javna licitacija"
                        },
                        new
                        {
                            TipJavnogNadmetanjaID = new Guid("99b6d6ec-4358-4898-936b-31b31d236324"),
                            NazivTipaJavnogNadmetanja = "Otvaranje zatvorenih ponuda"
                        });
                });

            modelBuilder.Entity("JavnoNadmetanje.Entities.JavnoNadmetanje", b =>
                {
                    b.HasOne("JavnoNadmetanje.Entities.StatusNadmetanja", "StatusNadmetanja")
                        .WithMany()
                        .HasForeignKey("StatusNadmetanjaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JavnoNadmetanje.Entities.TipJavnogNadmetanja", "TipJavnogNadmetanja")
                        .WithMany()
                        .HasForeignKey("TipJavnogNadmetanjaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusNadmetanja");

                    b.Navigation("TipJavnogNadmetanja");
                });

            modelBuilder.Entity("JavnoNadmetanje.Entities.Licitacija", b =>
                {
                    b.HasOne("JavnoNadmetanje.Entities.JavnoNadmetanje", "JavnoNadmetanje")
                        .WithMany()
                        .HasForeignKey("JavnoNadmetanjeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JavnoNadmetanje");
                });
#pragma warning restore 612, 618
        }
    }
}