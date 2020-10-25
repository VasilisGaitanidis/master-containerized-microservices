﻿// <auto-generated />
using System;
using Catalog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalog.Infrastructure.Migrations
{
    [DbContext(typeof(CatalogDataContext))]
    partial class CatalogDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Catalog.Domain.Models.CatalogItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("_catalogTypeId")
                        .HasColumnName("CatalogTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("_description")
                        .HasColumnName("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("_name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("_price")
                        .HasColumnName("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("_stock")
                        .HasColumnName("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("_catalogTypeId");

                    b.ToTable("CatalogItems","catalog");
                });

            modelBuilder.Entity("Catalog.Domain.Models.CatalogType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("_name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CatalogTypes","catalog");
                });

            modelBuilder.Entity("Catalog.Domain.Models.CatalogItem", b =>
                {
                    b.HasOne("Catalog.Domain.Models.CatalogType", "CatalogType")
                        .WithMany()
                        .HasForeignKey("_catalogTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
