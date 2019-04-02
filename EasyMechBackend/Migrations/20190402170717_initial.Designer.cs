﻿// <auto-generated />
using System;
using EasyMechBackend.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EasyMechBackend.Migrations
{
    [DbContext(typeof(EMContext))]
    [Migration("20190402170717_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Kunde", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Firma")
                        .HasMaxLength(128);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Nachname")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<string>("Notiz");

                    b.Property<string>("Ort")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("PLZ");

                    b.Property<string>("Telefon")
                        .HasMaxLength(128);

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Kunden","public");
                });
#pragma warning restore 612, 618
        }
    }
}
