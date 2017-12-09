﻿// <auto-generated />
using Blockbusters.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Blockbusters.Migrations
{
    [DbContext(typeof(BlockbustersContext))]
    partial class BlockbustersContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Blockbusters.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Blockbusters.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Blockbusters.Entities.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("RentedAt");

                    b.Property<int>("RentedByCustomerId");

                    b.Property<DateTime?>("ReturnedAt");

                    b.Property<DateTime>("ShouldBeReturnedAt");

                    b.Property<int>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("RentedByCustomerId");

                    b.HasIndex("VideoId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("Blockbusters.Entities.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Added");

                    b.Property<string>("FromYear");

                    b.Property<int>("LengthInMinutes");

                    b.Property<decimal>("Price");

                    b.Property<string>("Storyline");

                    b.Property<string>("Subtitle");

                    b.Property<string>("Title");

                    b.Property<int>("VideoTypeId");

                    b.HasKey("Id");

                    b.HasIndex("VideoTypeId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("Blockbusters.Entities.VideoToGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GenreId");

                    b.Property<int>("VideoId");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("VideoId");

                    b.ToTable("VideoToGenres");
                });

            modelBuilder.Entity("Blockbusters.Entities.VideoType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("VideoTypes");
                });

            modelBuilder.Entity("Blockbusters.Entities.Rental", b =>
                {
                    b.HasOne("Blockbusters.Entities.Customer", "Customer")
                        .WithMany("Rentals")
                        .HasForeignKey("RentedByCustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blockbusters.Entities.Video", "Video")
                        .WithMany("Rentals")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blockbusters.Entities.Video", b =>
                {
                    b.HasOne("Blockbusters.Entities.VideoType", "VideoType")
                        .WithMany()
                        .HasForeignKey("VideoTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Blockbusters.Entities.VideoToGenre", b =>
                {
                    b.HasOne("Blockbusters.Entities.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Blockbusters.Entities.Video", "Video")
                        .WithMany("VideoToGenres")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
