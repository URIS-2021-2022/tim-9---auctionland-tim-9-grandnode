// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UgovorService.Entities;

namespace UgovorService.Migrations
{
    [DbContext(typeof(UgovorContext))]
    partial class UgovorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UgovorService.Entities.TipGarancijeEnt", b =>
                {
                    b.Property<Guid>("TipID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tip")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipID");

                    b.ToTable("TipGarancijeEnt");

                    b.HasData(
                        new
                        {
                            TipID = new Guid("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                            Tip = "Mesecna"
                        },
                        new
                        {
                            TipID = new Guid("5a36d34e-1620-40c6-ab8c-b96b0be49332"),
                            Tip = "Kvartalna"
                        });
                });

            modelBuilder.Entity("UgovorService.Entities.UgovorEnt", b =>
                {
                    b.Property<Guid>("UgovorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DatumPot")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumZavo")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DokumentID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JavnoNadmetanjeID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KupacID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Mesto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Rok")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TipID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ZavodniBr")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UgovorID");

                    b.HasIndex("TipID");

                    b.ToTable("UgovorEnt");

                    b.HasData(
                        new
                        {
                            UgovorID = new Guid("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                            DatumPot = new DateTime(2020, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumZavo = new DateTime(2020, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            DokumentID = new Guid("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                            JavnoNadmetanjeID = new Guid("bf33a825-ad63-4e04-a812-74ffbebdadbb"),
                            KupacID = new Guid("7091a32b-7d21-43a7-9b41-a0419ac8edcc"),
                            Mesto = "Novi Sad",
                            Rok = new DateTime(2023, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            TipID = new Guid("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                            ZavodniBr = "471/RS"
                        },
                        new
                        {
                            UgovorID = new Guid("6f4967b3-beb3-4acf-95aa-488b16f8fc9a"),
                            DatumPot = new DateTime(2019, 12, 15, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            DatumZavo = new DateTime(2019, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            DokumentID = new Guid("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                            JavnoNadmetanjeID = new Guid("5a36d34e-1620-40c6-ab8c-b96b0be49333"),
                            KupacID = new Guid("7091a32b-7d21-43a7-9b41-a0419ac8ed8c"),
                            Mesto = "Beograd",
                            Rok = new DateTime(2022, 11, 15, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            TipID = new Guid("5a36d34e-1620-40c6-ab8c-b96b0be49332"),
                            ZavodniBr = "471/RS"
                        });
                });

            modelBuilder.Entity("UgovorService.Entities.UgovorEnt", b =>
                {
                    b.HasOne("UgovorService.Entities.TipGarancijeEnt", "TipGarancijeEnt")
                        .WithMany()
                        .HasForeignKey("TipID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipGarancijeEnt");
                });
#pragma warning restore 612, 618
        }
    }
}
