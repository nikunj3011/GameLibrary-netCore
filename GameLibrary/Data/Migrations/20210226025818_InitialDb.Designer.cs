﻿// <auto-generated />
using System;
using GameLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameLibrary.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20210226025818_InitialDb")]
    partial class InitialDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameLibrary.Data.Entities.GameShop", b =>
                {
                    b.Property<int>("GameShopID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameLibraryID")
                        .HasColumnType("int");

                    b.Property<string>("GameShopName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameSystemID")
                        .HasColumnType("int");

                    b.HasKey("GameShopID");

                    b.HasIndex("GameLibraryID");

                    b.HasIndex("GameSystemID");

                    b.ToTable("GameShops");
                });

            modelBuilder.Entity("GameLibrary.Data.Entities.GameSystem", b =>
                {
                    b.Property<int>("GameSystemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("GameSystemID");

                    b.ToTable("GameSystems");
                });

            modelBuilder.Entity("GameLibrary.Data.Entities.Library", b =>
                {
                    b.Property<int>("GameLibraryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GameSystemID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("GameLibraryID");

                    b.HasIndex("GameSystemID");

                    b.ToTable("GameLibraries");
                });

            modelBuilder.Entity("GameLibrary.Data.Entities.GameShop", b =>
                {
                    b.HasOne("GameLibrary.Data.Entities.Library", "GameLibrary")
                        .WithMany()
                        .HasForeignKey("GameLibraryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameLibrary.Data.Entities.GameSystem", "GameSystem")
                        .WithMany()
                        .HasForeignKey("GameSystemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameLibrary");

                    b.Navigation("GameSystem");
                });

            modelBuilder.Entity("GameLibrary.Data.Entities.Library", b =>
                {
                    b.HasOne("GameLibrary.Data.Entities.GameSystem", "GameSystems")
                        .WithMany("GameLibrary")
                        .HasForeignKey("GameSystemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameSystems");
                });

            modelBuilder.Entity("GameLibrary.Data.Entities.GameSystem", b =>
                {
                    b.Navigation("GameLibrary");
                });
#pragma warning restore 612, 618
        }
    }
}
