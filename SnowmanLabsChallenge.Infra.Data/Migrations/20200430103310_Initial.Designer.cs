﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using SnowmanLabsChallenge.Infra.Data.Context;

namespace SnowmanLabsChallenge.Infra.Data.Migrations
{
    [DbContext(typeof(DefaultContext))]
    [Migration("20200430103310_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SnowmanLabsChallenge.Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CreatedOn")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(null);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Uuid")
                        .HasColumnName("Uuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Uuid");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("SnowmanLabsChallenge.Domain.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CreatedOn")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(null);

                    b.Property<Guid>("Uuid")
                        .HasColumnName("Uuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Uuid");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("SnowmanLabsChallenge.Domain.Models.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CreatedOn")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(null);

                    b.Property<Guid>("Uuid")
                        .HasColumnName("Uuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Uuid");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("SnowmanLabsChallenge.Domain.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CreatedOn")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(null);

                    b.Property<Guid>("Uuid")
                        .HasColumnName("Uuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Uuid");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("SnowmanLabsChallenge.Domain.Models.TouristSpot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("Active")
                        .HasColumnType("bit");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CreatedOn")
                        .HasColumnType("datetime2")
                        .HasDefaultValue(null);

                    b.Property<Point>("Location")
                        .HasColumnType("geography");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasMaxLength(255);

                    b.Property<Guid>("Uuid")
                        .HasColumnName("Uuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Uuid");

                    b.ToTable("TouristSpot");
                });

            modelBuilder.Entity("SnowmanLabsChallenge.Domain.Models.TouristSpot", b =>
                {
                    b.HasOne("SnowmanLabsChallenge.Domain.Models.Category", "Category")
                        .WithMany("TouristSpots")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
