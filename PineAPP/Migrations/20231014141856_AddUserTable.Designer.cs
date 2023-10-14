﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PineAPP.Data;

#nullable disable

namespace PineAPP.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231014141856_AddUserTable")]
    partial class AddUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<string>("Examples")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Front")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.ToTable("Cards");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Back = "4",
                            DeckId = 1,
                            Examples = "",
                            Front = "2 + 2 = ?"
                        },
                        new
                        {
                            Id = 2,
                            Back = "3",
                            DeckId = 1,
                            Examples = "",
                            Front = "5 - 2 = ?"
                        },
                        new
                        {
                            Id = 3,
                            Back = "12",
                            DeckId = 1,
                            Examples = "",
                            Front = "4 * 3 = ?"
                        },
                        new
                        {
                            Id = 4,
                            Back = "4",
                            DeckId = 2,
                            Examples = "",
                            Front = "2 + 2 = ?"
                        },
                        new
                        {
                            Id = 5,
                            Back = "3",
                            DeckId = 2,
                            Examples = "",
                            Front = "5 - 2 = ?"
                        },
                        new
                        {
                            Id = 6,
                            Back = "12",
                            DeckId = 2,
                            Examples = "",
                            Front = "4 * 3 = ?"
                        });
                });

            modelBuilder.Entity("PineAPP.Models.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.Property<int>("Test")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Decks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatorId = 1,
                            Description = "A few cards to test your basic math skills",
                            IsPersonal = false,
                            Name = "Simple Math (Community)",
                            Test = 0
                        },
                        new
                        {
                            Id = 2,
                            CreatorId = 1,
                            Description = "A few cards to test your basic math skills",
                            IsPersonal = true,
                            Name = "Simple Math (Personal)",
                            Test = 0
                        });
                });

            modelBuilder.Entity("PineAPP.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "vardenis.pavardenis@gmail.com",
                            Password = "admin",
                            UserName = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "idk@gmail.com",
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
