﻿// <auto-generated />
using System;
using FestivalApp.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FestivalApp.DAL.Migrations
{
    [DbContext(typeof(FestivalDbContext))]
    partial class FestivalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FestivalApp.DAL.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d6a8bbe4-ecbb-43c9-b9ea-e6ce7a734f4d"),
                            AddressLine1 = "Bozetechova",
                            AddressLine2 = "2/1",
                            City = "Brno",
                            Country = "Czechia",
                            PostalCode = "612 00",
                            State = "Jihomoravsky",
                            UserId = new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a")
                        },
                        new
                        {
                            Id = new Guid("66c446a1-2447-4f58-aecd-e385c02217b9"),
                            AddressLine1 = "Hradčany",
                            City = "Prague",
                            Country = "Czechia",
                            PostalCode = "119 08",
                            State = "Prazsky",
                            UserId = new Guid("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110")
                        });
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Artist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LongDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0d04a673-c0a4-4fb8-893c-3bc6b725c1d3"),
                            Country = "Great Britain",
                            Genre = "Rock",
                            LongDescription = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                            Name = "Freddy Mercury",
                            PhotoPath = "pic/freddy.jpg",
                            ShortDescription = "Mercury defied the conventions of a rock frontman, with his highly theatrical style influencing theartistic direction of Queen."
                        },
                        new
                        {
                            Id = new Guid("5469990b-d1f9-49a6-ae50-800270d77b88"),
                            Country = "Great Britain",
                            Genre = "Rock",
                            LongDescription = "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB",
                            Name = "Queen",
                            PhotoPath = "pic/Queen.png",
                            ShortDescription = "Short description..."
                        });
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Festival", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Capacity")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Festivals");

                    b.HasData(
                        new
                        {
                            Id = new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"),
                            Capacity = 10000L,
                            End = new DateTime(2021, 6, 13, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Start = new DateTime(2021, 6, 10, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Rock For People",
                            UserId = new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a")
                        },
                        new
                        {
                            Id = new Guid("424eebbe-f047-48f5-9db3-051f753d5c13"),
                            Capacity = 10000L,
                            End = new DateTime(2021, 7, 7, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Start = new DateTime(2021, 7, 4, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Pohoda",
                            UserId = new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a")
                        });
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Show", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ArtistId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndPlaying")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("LengthOfPlaying")
                        .HasColumnType("time");

                    b.Property<Guid>("StageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartPlaying")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("StageId");

                    b.ToTable("Shows");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8837e270-2243-4f59-98ba-b2189dcae3ce"),
                            ArtistId = new Guid("0d04a673-c0a4-4fb8-893c-3bc6b725c1d3"),
                            EndPlaying = new DateTime(2021, 6, 10, 18, 40, 0, 0, DateTimeKind.Unspecified),
                            LengthOfPlaying = new TimeSpan(0, 1, 40, 0, 0),
                            StageId = new Guid("fa804f29-e366-4018-b38b-71af32d8142f"),
                            StartPlaying = new DateTime(2021, 6, 10, 17, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("3c0cb3fa-2be5-408a-af45-f258a5d3df00"),
                            ArtistId = new Guid("5469990b-d1f9-49a6-ae50-800270d77b88"),
                            EndPlaying = new DateTime(2021, 6, 10, 22, 0, 0, 0, DateTimeKind.Unspecified),
                            LengthOfPlaying = new TimeSpan(0, 2, 0, 0, 0),
                            StageId = new Guid("40d3eb9b-8a13-46b4-815e-a0dfb1bef9be"),
                            StartPlaying = new DateTime(2021, 6, 10, 20, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("7714e9e2-8bba-49d3-b72f-184d94afa736"),
                            ArtistId = new Guid("0d04a673-c0a4-4fb8-893c-3bc6b725c1d3"),
                            EndPlaying = new DateTime(2021, 7, 4, 19, 30, 0, 0, DateTimeKind.Unspecified),
                            LengthOfPlaying = new TimeSpan(0, 1, 30, 0, 0),
                            StageId = new Guid("75fcfe46-c596-4d93-aa11-89574b2c7574"),
                            StartPlaying = new DateTime(2021, 7, 4, 18, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Stage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FestivalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FestivalId");

                    b.ToTable("Stages");

                    b.HasData(
                        new
                        {
                            Id = new Guid("40d3eb9b-8a13-46b4-815e-a0dfb1bef9be"),
                            Description = "Stage east of gate",
                            FestivalId = new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"),
                            Name = "Stage East",
                            PhotoPath = "stages/east.png"
                        },
                        new
                        {
                            Id = new Guid("fa804f29-e366-4018-b38b-71af32d8142f"),
                            Description = "Stage west of gate",
                            FestivalId = new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"),
                            Name = "Stage West",
                            PhotoPath = "stages/west.png"
                        },
                        new
                        {
                            Id = new Guid("75fcfe46-c596-4d93-aa11-89574b2c7574"),
                            Description = "Only stage",
                            FestivalId = new Guid("424eebbe-f047-48f5-9db3-051f753d5c13"),
                            Name = "Main Stage",
                            PhotoPath = "stages/main.png"
                        });
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FestivalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<decimal>("PriceAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PriceCurrency")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FestivalId");

                    b.HasIndex("UserId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a"),
                            FestivalId = new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"),
                            Length = 1,
                            PriceAmount = 1500m,
                            PriceCurrency = 2,
                            StartDate = new DateTime(2021, 6, 10, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0,
                            UserId = new Guid("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110")
                        },
                        new
                        {
                            Id = new Guid("ff603e7c-9ee3-4e54-8d3f-6ef2796e9113"),
                            FestivalId = new Guid("263aed9b-0cfc-4621-beef-8bfa8a001b81"),
                            Length = 1,
                            PriceAmount = 3000m,
                            PriceCurrency = 2,
                            StartDate = new DateTime(2021, 6, 10, 15, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 1,
                            UserId = new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a")
                        },
                        new
                        {
                            Id = new Guid("ef603e7c-9ee3-4e54-8d3f-6ef2796e9111"),
                            FestivalId = new Guid("424eebbe-f047-48f5-9db3-051f753d5c13"),
                            Length = 0,
                            PriceAmount = 30m,
                            PriceCurrency = 1,
                            StartDate = new DateTime(2021, 7, 5, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0,
                            UserId = new Guid("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110")
                        });
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4e70d683-fa6c-4384-8564-30bb2ec4af3a"),
                            AddressId = new Guid("d6a8bbe4-ecbb-43c9-b9ea-e6ce7a734f4d"),
                            Email = "e@mail.com",
                            Name = "John Doe",
                            PhoneNumber = "+42 987 654 321"
                        },
                        new
                        {
                            Id = new Guid("d3603e7c-9ee3-4e54-8d3f-6ef2796e9110"),
                            AddressId = new Guid("66c446a1-2447-4f58-aecd-e385c02217b9"),
                            Email = "milos@zeman.cz",
                            Name = "Milos Zeman",
                            PhoneNumber = "+42 000 000 000"
                        });
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Address", b =>
                {
                    b.HasOne("FestivalApp.DAL.Entities.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("FestivalApp.DAL.Entities.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Festival", b =>
                {
                    b.HasOne("FestivalApp.DAL.Entities.User", "User")
                        .WithMany("Festivals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Show", b =>
                {
                    b.HasOne("FestivalApp.DAL.Entities.Artist", "Artist")
                        .WithMany("Shows")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FestivalApp.DAL.Entities.Stage", "Stage")
                        .WithMany("Shows")
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Stage");
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Stage", b =>
                {
                    b.HasOne("FestivalApp.DAL.Entities.Festival", "Festival")
                        .WithMany("Stages")
                        .HasForeignKey("FestivalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Festival");
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Ticket", b =>
                {
                    b.HasOne("FestivalApp.DAL.Entities.Festival", "Festival")
                        .WithMany("Tickets")
                        .HasForeignKey("FestivalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FestivalApp.DAL.Entities.User", "User")
                        .WithMany("Tickets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Festival");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Artist", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Festival", b =>
                {
                    b.Navigation("Stages");

                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.Stage", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("FestivalApp.DAL.Entities.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Festivals");

                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
