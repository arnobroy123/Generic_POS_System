﻿// <auto-generated />
using System;
using Generic_POS_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Generic_POS_System.Migrations
{
    [DbContext(typeof(PosContext))]
    [Migration("20220503064350_AnnotationsAdded")]
    partial class AnnotationsAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Generic_POS_System.Data.Products", b =>
                {
                    b.Property<int>("productId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("categoryId")
                        .HasColumnType("int");

                    b.Property<decimal?>("productDiscount")
                        .HasColumnType("decimal(3,2)");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("char(15)");

                    b.Property<int>("totalProducts")
                        .HasColumnType("int");

                    b.Property<decimal>("unitPrice")
                        .HasColumnType("decimal(7,2)");

                    b.HasKey("productId");

                    b.ToTable("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
