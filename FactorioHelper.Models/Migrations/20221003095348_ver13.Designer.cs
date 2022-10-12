﻿// <auto-generated />
using System;
using FactorioHelper.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FactorioHelper.Models.Migrations
{
    [DbContext(typeof(SQLiteContext))]
    [Migration("20221003095348_ver13")]
    partial class ver13
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("FactorioHelper.Models.Ingredient", b =>
                {
                    b.Property<int>("AmountNeeded")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TimeToCraftMainItem")
                        .HasColumnType("REAL");

                    b.Property<long>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("AmountNeededPerSec")
                        .HasColumnType("REAL");

                    b.Property<long?>("ItemId1")
                        .HasColumnType("INTEGER");

                    b.HasKey("AmountNeeded", "TimeToCraftMainItem", "ItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("ItemId1");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("FactorioHelper.Models.Item", b =>
                {
                    b.Property<long>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountCrafted")
                        .HasColumnType("INTEGER");

                    b.Property<double>("AmountPerSec")
                        .HasColumnType("REAL");

                    b.Property<int>("IsAssemblingMachine")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("TimeToCraft")
                        .HasColumnType("REAL");

                    b.HasKey("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("FactorioHelper.Models.Ingredient", b =>
                {
                    b.HasOne("FactorioHelper.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FactorioHelper.Models.Item", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("ItemId1");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("FactorioHelper.Models.Item", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
