﻿// <auto-generated />
using System;
using KomisijaService.Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KomisijaService.Migrations
{
    [DbContext(typeof(KomisijaContext))]
    partial class KomisijaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KomisijaService.Entities.Clanovi", b =>
                {
                    b.Property<Guid>("ClanoviId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("KomisijaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClanoviId");

                    b.HasIndex("KomisijaId");

                    b.ToTable("Clanovi");

                    b.HasData(
                        new
                        {
                            ClanoviId = new Guid("ea3d75d9-61aa-4cc5-9e2a-6f01190b9044"),
                            KomisijaId = new Guid("c1b8a40c-0e1c-44a6-87d2-1593ab638e94")
                        },
                        new
                        {
                            ClanoviId = new Guid("c84a7948-81c5-44d2-86c1-c601fdab526b"),
                            KomisijaId = new Guid("0648b913-c49e-4939-95ae-10185e475ef7")
                        },
                        new
                        {
                            ClanoviId = new Guid("06cfa3e0-6d39-46c6-b5bb-98e0a64a9637"),
                            KomisijaId = new Guid("bf1c58fd-ba25-4bd9-837a-37c06ad29ea5")
                        });
                });

            modelBuilder.Entity("KomisijaService.Entities.Komisija", b =>
                {
                    b.Property<Guid>("KomisijaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NazivKomisije")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PredsednikId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("KomisijaId");

                    b.HasIndex("PredsednikId");

                    b.ToTable("Komisija");

                    b.HasData(
                        new
                        {
                            KomisijaId = new Guid("c1b8a40c-0e1c-44a6-87d2-1593ab638e94"),
                            NazivKomisije = "Prva komisija",
                            PredsednikId = new Guid("61ef85bf-765a-4a53-a50a-9d99255cdeaf")
                        },
                        new
                        {
                            KomisijaId = new Guid("0648b913-c49e-4939-95ae-10185e475ef7"),
                            NazivKomisije = "Druga komisija",
                            PredsednikId = new Guid("efcbf7d7-de6b-4468-a8f7-d3907d541262")
                        },
                        new
                        {
                            KomisijaId = new Guid("bf1c58fd-ba25-4bd9-837a-37c06ad29ea5"),
                            NazivKomisije = "PTreca komisija",
                            PredsednikId = new Guid("ebfc69f7-8626-48c4-8c92-c06ca85cf7b1")
                        });
                });

            modelBuilder.Entity("KomisijaService.Entities.Predsednik", b =>
                {
                    b.Property<Guid>("PredsednikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PredsednikId");

                    b.ToTable("Predsednik");

                    b.HasData(
                        new
                        {
                            PredsednikId = new Guid("61ef85bf-765a-4a53-a50a-9d99255cdeaf")
                        },
                        new
                        {
                            PredsednikId = new Guid("efcbf7d7-de6b-4468-a8f7-d3907d541262")
                        },
                        new
                        {
                            PredsednikId = new Guid("ebfc69f7-8626-48c4-8c92-c06ca85cf7b1")
                        });
                });

            modelBuilder.Entity("KomisijaService.Entities.Clanovi", b =>
                {
                    b.HasOne("KomisijaService.Entities.Komisija", "Komisija")
                        .WithMany()
                        .HasForeignKey("KomisijaId");

                    b.Navigation("Komisija");
                });

            modelBuilder.Entity("KomisijaService.Entities.Komisija", b =>
                {
                    b.HasOne("KomisijaService.Entities.Predsednik", "Predsednik")
                        .WithMany()
                        .HasForeignKey("PredsednikId");

                    b.Navigation("Predsednik");
                });
#pragma warning restore 612, 618
        }
    }
}
