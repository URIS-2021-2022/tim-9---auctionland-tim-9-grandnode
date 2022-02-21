﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Uplata.Entities;

namespace Uplata.Migrations
{
    [DbContext(typeof(UplataContext))]
    partial class UplataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Uplata.Entities.KursnaLista", b =>
                {
                    b.Property<Guid>("KursnaListaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Valuta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Vrednost")
                        .HasColumnType("real");

                    b.HasKey("KursnaListaID");

                    b.ToTable("KursneListe");

                    b.HasData(
                        new
                        {
                            KursnaListaID = new Guid("c8a3972c-ed80-4030-a6a3-61c37cc5b36d"),
                            Datum = new DateTime(2022, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Valuta = "RSD",
                            Vrednost = 1234f
                        },
                        new
                        {
                            KursnaListaID = new Guid("f9d0d94c-a332-4437-a8d1-e2b64349e0ad"),
                            Datum = new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Valuta = "RSD",
                            Vrednost = 4321f
                        });
                });

            modelBuilder.Entity("Uplata.Entities.Uplata", b =>
                {
                    b.Property<Guid>("UplataID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BrojRacuna")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<float>("Iznos")
                        .HasColumnType("real");

                    b.Property<Guid>("JavnoNadmetanjeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KupacID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KursnaListaID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PozivNaBroj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SvrhaUplate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UplataID");

                    b.HasIndex("KursnaListaID");

                    b.ToTable("Uplate");

                    b.HasData(
                        new
                        {
                            UplataID = new Guid("556228f6-4afd-4a7a-9767-371b608abaab"),
                            BrojRacuna = "236541",
                            Datum = new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Iznos = 4321f,
                            JavnoNadmetanjeID = new Guid("13d6ced2-ab84-4132-bf67-e96037f4813d"),
                            KupacID = new Guid("8aaa90c8-56f3-4a76-b07a-f895eded5a84"),
                            KursnaListaID = new Guid("c8a3972c-ed80-4030-a6a3-61c37cc5b36d"),
                            PozivNaBroj = "147852",
                            SvrhaUplate = "Uplata javnog nadmetanja"
                        },
                        new
                        {
                            UplataID = new Guid("3c08638f-86c4-4744-a6fa-e0863adccdc6"),
                            BrojRacuna = "236541",
                            Datum = new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Iznos = 4321f,
                            JavnoNadmetanjeID = new Guid("13d6ced2-ab84-4132-bf67-e96037f4813d"),
                            KupacID = new Guid("d371e7b7-9b08-4831-a300-0df4c200a3d8"),
                            KursnaListaID = new Guid("c8a3972c-ed80-4030-a6a3-61c37cc5b36d"),
                            PozivNaBroj = "147852",
                            SvrhaUplate = "Uplata javnog nadmetanja"
                        });
                });

            modelBuilder.Entity("Uplata.Entities.Uplata", b =>
                {
                    b.HasOne("Uplata.Entities.KursnaLista", "KursnaLista")
                        .WithMany()
                        .HasForeignKey("KursnaListaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KursnaLista");
                });
#pragma warning restore 612, 618
        }
    }
}