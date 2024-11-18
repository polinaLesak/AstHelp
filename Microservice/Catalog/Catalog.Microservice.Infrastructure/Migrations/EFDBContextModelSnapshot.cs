﻿// <auto-generated />
using System;
using Catalog.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Catalog.Microservice.Infrastructure.Migrations
{
    [DbContext(typeof(EFDBContext))]
    partial class EFDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.Attribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AttributeTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AttributeTypeId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.AttributeType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AttributeTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Строковый"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Число"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Большое число"
                        });
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.AttributeValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AttributeId")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int?>("ValueInt")
                        .HasColumnType("integer");

                    b.Property<decimal?>("ValueNumeric")
                        .HasColumnType("numeric");

                    b.Property<string>("ValueString")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("ProductId");

                    b.ToTable("AttributeValues");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.Catalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.CatalogAttribute", b =>
                {
                    b.Property<int>("CatalogId")
                        .HasColumnType("integer");

                    b.Property<int>("AttributeId")
                        .HasColumnType("integer");

                    b.HasKey("CatalogId", "AttributeId");

                    b.HasIndex("AttributeId");

                    b.ToTable("CatalogAttributes");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BrandId")
                        .HasColumnType("integer");

                    b.Property<int>("CatalogId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CatalogId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.Attribute", b =>
                {
                    b.HasOne("Catalog.Microservice.Domain.Entities.AttributeType", "AttributeType")
                        .WithMany("Attributes")
                        .HasForeignKey("AttributeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AttributeType");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.AttributeValue", b =>
                {
                    b.HasOne("Catalog.Microservice.Domain.Entities.Attribute", "Attribute")
                        .WithMany("AttributeValues")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Microservice.Domain.Entities.Product", "Product")
                        .WithMany("AttributeValues")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.CatalogAttribute", b =>
                {
                    b.HasOne("Catalog.Microservice.Domain.Entities.Attribute", "Attribute")
                        .WithMany("CatalogAttributes")
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Microservice.Domain.Entities.Catalog", "Catalog")
                        .WithMany("CatalogAttributes")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("Catalog");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.Product", b =>
                {
                    b.HasOne("Catalog.Microservice.Domain.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Catalog.Microservice.Domain.Entities.Catalog", "Catalog")
                        .WithMany("Products")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Catalog");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.Attribute", b =>
                {
                    b.Navigation("AttributeValues");

                    b.Navigation("CatalogAttributes");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.AttributeType", b =>
                {
                    b.Navigation("Attributes");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.Catalog", b =>
                {
                    b.Navigation("CatalogAttributes");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Catalog.Microservice.Domain.Entities.Product", b =>
                {
                    b.Navigation("AttributeValues");
                });
#pragma warning restore 612, 618
        }
    }
}
