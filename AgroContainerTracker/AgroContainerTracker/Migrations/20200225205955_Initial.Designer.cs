﻿// <auto-generated />
using System;
using AgroContainerTracker.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgroContainerTracker.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200225205955_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AgroContainerTracker.Data.Entities.Container", b =>
                {
                    b.Property<int>("ContainerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Size")
                        .HasColumnType("double");

                    b.HasKey("ContainerId");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("AgroContainerTracker.Data.Entities.Fruit", b =>
                {
                    b.Property<Guid>("FruitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("FruitId");

                    b.ToTable("Fruits");
                });

            modelBuilder.Entity("AgroContainerTracker.Data.Entities.FruitBuyer", b =>
                {
                    b.Property<Guid>("FruitBuyerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("FruitBuyerId");

                    b.ToTable("FruitBuyers");
                });

            modelBuilder.Entity("AgroContainerTracker.Data.Entities.FruitGrower", b =>
                {
                    b.Property<Guid>("FruitBuyerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("FruitBuyerId");

                    b.ToTable("FruitGrowers");
                });

            modelBuilder.Entity("AgroContainerTracker.Data.Entities.Palot", b =>
                {
                    b.Property<string>("PalotId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Arrival")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ArrivalNumber")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("ContainerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Departure")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FruitBuyerId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("FruitGrowerId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("FruitId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("double");

                    b.HasKey("PalotId");

                    b.HasIndex("ContainerId");

                    b.HasIndex("FruitBuyerId");

                    b.HasIndex("FruitGrowerId");

                    b.HasIndex("FruitId");

                    b.ToTable("Palots");
                });

            modelBuilder.Entity("AgroContainerTracker.Data.Entities.Palot", b =>
                {
                    b.HasOne("AgroContainerTracker.Data.Entities.Container", null)
                        .WithMany("Palots")
                        .HasForeignKey("ContainerId");

                    b.HasOne("AgroContainerTracker.Data.Entities.FruitBuyer", "FruitBuyer")
                        .WithMany()
                        .HasForeignKey("FruitBuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgroContainerTracker.Data.Entities.FruitGrower", "FruitGrower")
                        .WithMany()
                        .HasForeignKey("FruitGrowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgroContainerTracker.Data.Entities.Fruit", "Fruit")
                        .WithMany()
                        .HasForeignKey("FruitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
