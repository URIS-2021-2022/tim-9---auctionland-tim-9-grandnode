﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OvlascenoLice.Entities;

namespace OvlascenoLice.Migrations
{
    [DbContext(typeof(OvlascenoLiceContext))]
    partial class OvlascenoLiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OvlascenoLice.Entities.OvlascenoLiceModel", b =>
                {
                    b.Property<Guid>("OvlascenoLiceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AdresaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojDokumenta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTable")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OvlascenoLiceID");

                    b.ToTable("OvlascenaLica");

                    b.HasData(
                        new
                        {
                            OvlascenoLiceID = new Guid("5dc3dfcd-de07-4e5f-878e-a07636db322f"),
                            AdresaID = new Guid("7280c84a-a070-4516-94e7-ef905c7dcf8b"),
                            BrojDokumenta = "4585248",
                            BrojTable = "74474",
                            Ime = "Sara",
                            Prezime = "Kijanovic"
                        },
                        new
                        {
                            OvlascenoLiceID = new Guid("668e0c43-810b-4443-82a7-649b4f25a840"),
                            AdresaID = new Guid("4ead0649-3ad7-42cb-92b3-80e504006df9"),
                            BrojDokumenta = "465548",
                            BrojTable = "7434664",
                            Ime = "Marko",
                            Prezime = "Ruzic"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
