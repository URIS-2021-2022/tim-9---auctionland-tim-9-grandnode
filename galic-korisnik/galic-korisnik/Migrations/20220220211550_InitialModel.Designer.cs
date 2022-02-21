﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using galic_korisnik.Entities;

namespace galic_korisnik.Migrations
{
    [DbContext(typeof(KorisnikContext))]
    [Migration("20220220211550_InitialModel")]
    partial class InitialModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("galic_korisnik.Entities.Korisnik", b =>
                {
                    b.Property<Guid>("korisnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("korisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lozinka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("prezime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("tipKorisnikaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("korisnikId");

                    b.ToTable("Korisnik");

                    b.HasData(
                        new
                        {
                            korisnikId = new Guid("f7a20259-5aeb-3135-64ea-32cf7a96b98a"),
                            ime = "Petar",
                            korisnickoIme = "PPetrovic",
                            lozinka = "123456",
                            prezime = "Petrovic",
                            tipKorisnikaId = new Guid("ce4a6a8a-b25d-d5d0-9364-3dee56521821")
                        },
                        new
                        {
                            korisnikId = new Guid("e8920f41-e035-da6d-27d1-ee8909f6271d"),
                            ime = "Marko",
                            korisnickoIme = "MMarkovic",
                            lozinka = "123456",
                            prezime = "Markovic",
                            tipKorisnikaId = new Guid("22caf793-fbaa-a3f5-8266-7fc3dcc798dc")
                        });
                });

            modelBuilder.Entity("galic_korisnik.Entities.TipKorisnika", b =>
                {
                    b.Property<Guid>("tipKorisnikaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("uloga")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tipKorisnikaId");

                    b.ToTable("TipKorisnika");

                    b.HasData(
                        new
                        {
                            tipKorisnikaId = new Guid("9d8004cb-fad6-40a9-9d9e-978ff4f98481"),
                            uloga = "Admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
