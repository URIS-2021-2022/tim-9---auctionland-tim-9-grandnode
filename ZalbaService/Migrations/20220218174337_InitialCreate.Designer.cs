﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZalbaService.Entities.DataContext;

namespace ZalbaService.Migrations
{
    [DbContext(typeof(ZalbaContext))]
    [Migration("20220218174337_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ZalbaService.Entities.Radnja", b =>
                {
                    b.Property<Guid>("RadnjaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivRadnje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RadnjaId");

                    b.ToTable("Radnja");

                    b.HasData(
                        new
                        {
                            RadnjaId = new Guid("b0f02aee-2bf3-43e2-bdd4-eb9e80f4bcc9"),
                            NazivRadnje = "JN ide u drugi krug sa novim uslovima"
                        },
                        new
                        {
                            RadnjaId = new Guid("62462998-fe3f-49f2-861d-9f226264beba"),
                            NazivRadnje = "JN ide u drugi krug sa starim uslovima"
                        },
                        new
                        {
                            RadnjaId = new Guid("3eeede02-9e9e-46d2-8034-d21125e45b43"),
                            NazivRadnje = "JN ne ide u drugi krug"
                        });
                });

            modelBuilder.Entity("ZalbaService.Entities.StatusZalbe", b =>
                {
                    b.Property<Guid>("StatusZalbeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivStatusa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusZalbeId");

                    b.ToTable("StatusZalbe");

                    b.HasData(
                        new
                        {
                            StatusZalbeId = new Guid("212b6e83-ab50-49ec-bd95-92cd5e8f8a25"),
                            NazivStatusa = "Usvojena"
                        },
                        new
                        {
                            StatusZalbeId = new Guid("0d377d88-9e91-43c0-adfd-ecc8bd406809"),
                            NazivStatusa = "Odbijena"
                        },
                        new
                        {
                            StatusZalbeId = new Guid("b1c8cea8-c996-4344-a211-6a7e7e257e46"),
                            NazivStatusa = "Otvorena"
                        });
                });

            modelBuilder.Entity("ZalbaService.Entities.TipZalbe", b =>
                {
                    b.Property<Guid>("TipZalbeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivTipa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipZalbeId");

                    b.ToTable("TipZalbe");

                    b.HasData(
                        new
                        {
                            TipZalbeId = new Guid("cd155ba7-f573-4f24-b412-e41994ef8073"),
                            NazivTipa = "Žalba na tok javnog nadmetanja"
                        },
                        new
                        {
                            TipZalbeId = new Guid("1fbd2475-b35e-4e47-af39-f784c8d49497"),
                            NazivTipa = "Žalba na Odluku o davanju u zakup"
                        },
                        new
                        {
                            TipZalbeId = new Guid("c925bbc9-bbc1-4917-9aea-d5b253abc8a5"),
                            NazivTipa = "Žalba na Odluku o davanju na korišćenje"
                        });
                });

            modelBuilder.Entity("ZalbaService.Entities.Zalba", b =>
                {
                    b.Property<Guid>("ZalbaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojOdluke")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojResenja")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumResenja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumZalbe")
                        .HasColumnType("datetime2");

                    b.Property<string>("Obrazlozenje")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PodnosilacZalbe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Radnja")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Razlog")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StatusZalbe")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TipZalbe")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ZalbaId");

                    b.ToTable("Zalba");

                    b.HasData(
                        new
                        {
                            ZalbaId = new Guid("007ed3b2-abb5-4bb8-90d5-f193907079ad"),
                            BrojOdluke = "1221",
                            BrojResenja = "1035",
                            DatumResenja = new DateTime(2021, 6, 3, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumZalbe = new DateTime(2021, 4, 20, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            Obrazlozenje = "Neispravnost prilikom dodeljivanja parcele",
                            PodnosilacZalbe = "Marko Markovic",
                            Radnja = new Guid("3eeede02-9e9e-46d2-8034-d21125e45b43"),
                            Razlog = "Krsenje pravilnika za javno nadmetanje",
                            StatusZalbe = new Guid("212b6e83-ab50-49ec-bd95-92cd5e8f8a25"),
                            TipZalbe = new Guid("cd155ba7-f573-4f24-b412-e41994ef8073")
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
