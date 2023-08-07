﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace Technical.Business.Data
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("sqllitetest.database.Bar", b =>
                {
                    b.Property<Guid>("BarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BarId");

                    b.ToTable("Bars");
                });

            modelBuilder.Entity("sqllitetest.database.Beer", b =>
                {
                    b.Property<Guid>("BeerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PercentageAlcoholByVolume")
                        .HasColumnType("TEXT");

                    b.HasKey("BeerId");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("sqllitetest.database.BeerBarMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BarId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BeerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BarId");

                    b.HasIndex("BeerId");

                    b.ToTable("BeersBarsMapping");
                });

            modelBuilder.Entity("sqllitetest.database.Brewery", b =>
                {
                    b.Property<Guid>("BreweryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BreweryId");

                    b.ToTable("Breweries");
                });

            modelBuilder.Entity("sqllitetest.database.BreweryBeerMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BeerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BreweryId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("BreweryId");

                    b.ToTable("BrewersBeersMapping");
                });

            modelBuilder.Entity("sqllitetest.database.BeerBarMapping", b =>
                {
                    b.HasOne("sqllitetest.database.Bar", "Bar")
                        .WithMany()
                        .HasForeignKey("BarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sqllitetest.database.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bar");

                    b.Navigation("Beer");
                });

            modelBuilder.Entity("sqllitetest.database.BreweryBeerMapping", b =>
                {
                    b.HasOne("sqllitetest.database.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("sqllitetest.database.Brewery", "Brewery")
                        .WithMany()
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Brewery");
                });
#pragma warning restore 612, 618
        }
    }
}
