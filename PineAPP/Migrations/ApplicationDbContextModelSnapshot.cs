﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PineAPP.Data;

#nullable disable

namespace PineAPP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PineAPP.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Back")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("CurrentCardIndex")
                        .HasColumnType("int");

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<string>("Examples")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Front")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("TotalCardsInDeck")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Back = "4",
                            CurrentCardIndex = 0,
                            DeckId = 1,
                            Examples = "",
                            Front = "2 + 2 = ?",
                            TotalCardsInDeck = 0
                        },
                        new
                        {
                            Id = 2,
                            Back = "3",
                            CurrentCardIndex = 0,
                            DeckId = 1,
                            Examples = "",
                            Front = "5 - 2 = ?",
                            TotalCardsInDeck = 0
                        },
                        new
                        {
                            Id = 3,
                            Back = "12",
                            CurrentCardIndex = 0,
                            DeckId = 1,
                            Examples = "",
                            Front = "4 * 3 = ?",
                            TotalCardsInDeck = 0
                        },
                        new
                        {
                            Id = 4,
                            Back = "4",
                            CurrentCardIndex = 0,
                            DeckId = 2,
                            Examples = "",
                            Front = "2 + 2 = ?",
                            TotalCardsInDeck = 0
                        },
                        new
                        {
                            Id = 5,
                            Back = "3",
                            CurrentCardIndex = 0,
                            DeckId = 2,
                            Examples = "",
                            Front = "5 - 2 = ?",
                            TotalCardsInDeck = 0
                        },
                        new
                        {
                            Id = 6,
                            Back = "12",
                            CurrentCardIndex = 0,
                            DeckId = 2,
                            Examples = "",
                            Front = "4 * 3 = ?",
                            TotalCardsInDeck = 0
                        });
                });

            modelBuilder.Entity("PineAPP.Models.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Correct")
                        .HasColumnType("int");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsPersonal")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Wrong")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Decks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Correct = 0,
                            CreatorId = 1,
                            Description = "A few cards to test your basic math skills",
                            IsPersonal = false,
                            Name = "Simple Math (Community)",
                            Wrong = 0
                        },
                        new
                        {
                            Id = 2,
                            Correct = 0,
                            CreatorId = 1,
                            Description = "A few cards to test your basic math skills",
                            IsPersonal = true,
                            Name = "Simple Math (Personal)",
                            Wrong = 0
                        });
                });

            modelBuilder.Entity("PineAPP.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("nvarchar(320)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "admin@gmail.com",
                            Password = "admin",
                            UserName = "admin"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "vardenis.pavardenis@gmail.com",
                            Password = "testavicius",
                            UserName = "testas"
                        });
                });

            modelBuilder.Entity("PineAPP.Models.Card", b =>
                {
                    b.HasOne("PineAPP.Models.Deck", null)
                        .WithMany("Cards")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PineAPP.Models.Deck", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
