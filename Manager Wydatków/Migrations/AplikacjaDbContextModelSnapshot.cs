﻿// <auto-generated />
using System;
using Manager_Wydatkow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ManagerWydatkow.Migrations
{
    [DbContext(typeof(AplikacjaDbContext))]
    partial class AplikacjaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Manager_Wydatkow.Models.Kategoria", b =>
                {
                    b.Property<int>("KategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KategoriaId"));

                    b.Property<string>("Ikona")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("Typ")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Tytul")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("KategoriaId");

                    b.ToTable("Kategorias");
                });

            modelBuilder.Entity("Manager_Wydatkow.Models.Transakcje", b =>
                {
                    b.Property<int>("TransakcjeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransakcjeId"));

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("KategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(75)");

                    b.HasKey("TransakcjeId");

                    b.HasIndex("KategoriaId");

                    b.ToTable("Transakcjes");
                });

            modelBuilder.Entity("Manager_Wydatkow.Models.Transakcje", b =>
                {
                    b.HasOne("Manager_Wydatkow.Models.Kategoria", "Kategoria")
                        .WithMany()
                        .HasForeignKey("KategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategoria");
                });
#pragma warning restore 612, 618
        }
    }
}
