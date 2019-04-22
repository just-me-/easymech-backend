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
    [Migration("20190422105858_removedUmlaute")]
    partial class removedUmlaute
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Fahrzeugtyp", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Eigengewicht");

                    b.Property<string>("Fabrikat")
                        .HasMaxLength(128);

                    b.Property<int>("Fahrzeugbreite");

                    b.Property<int>("Fahrzeughoehe");

                    b.Property<int>("Fahrzeuglaenge");

                    b.Property<int>("Hubhoehe");

                    b.Property<int>("Hubkraft");

                    b.Property<string>("Motortyp")
                        .HasMaxLength(128);

                    b.Property<int>("Nutzlast");

                    b.Property<int>("Pneugroesse");

                    b.HasKey("Id");

                    b.ToTable("Fahrzeugtyp","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Kunde", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adresse")
                        .HasMaxLength(128);

                    b.Property<string>("Email")
                        .HasMaxLength(128);

                    b.Property<string>("Firma")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<bool?>("IsActive")
                        .IsRequired();

                    b.Property<string>("Nachname")
                        .HasMaxLength(128);

                    b.Property<string>("Notiz");

                    b.Property<string>("Ort")
                        .HasMaxLength(128);

                    b.Property<string>("PLZ")
                        .HasMaxLength(128);

                    b.Property<string>("Telefon")
                        .HasMaxLength(128);

                    b.Property<string>("Vorname")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Kunden","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Maschine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("BesitzerId");

                    b.Property<int>("Betriebsdauer");

                    b.Property<bool?>("IsActive")
                        .IsRequired();

                    b.Property<int>("Jahrgang");

                    b.Property<string>("Mastnummer")
                        .HasMaxLength(128);

                    b.Property<string>("Motorennummer")
                        .HasMaxLength(128);

                    b.Property<string>("Notiz");

                    b.Property<string>("Seriennummer")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("BesitzerId");

                    b.ToTable("Maschine","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Maschine", b =>
                {
                    b.HasOne("EasyMechBackend.DataAccessLayer.Kunde", "Besitzer")
                        .WithMany("Maschinen")
                        .HasForeignKey("BesitzerId");
                });
#pragma warning restore 612, 618
        }
    }
}
