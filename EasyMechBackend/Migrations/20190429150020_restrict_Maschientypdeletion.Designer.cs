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
    [Migration("20190429150020_restrict_Maschientypdeletion")]
    partial class restrict_Maschientypdeletion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Arbeitsschritt", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Arbeitsstunden");

                    b.Property<string>("Bezeichnung")
                        .HasMaxLength(256);

                    b.Property<long>("ServiceDurchfuehrungId");

                    b.Property<double?>("Stundenansatz");

                    b.HasKey("Id");

                    b.HasIndex("ServiceDurchfuehrungId");

                    b.ToTable("Arbeitsschritt","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.GeplanterService", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Beginn");

                    b.Property<string>("Bezeichnung")
                        .HasMaxLength(128);

                    b.Property<DateTime>("Ende");

                    b.Property<long>("KundenId");

                    b.Property<long>("MaschinenId");

                    b.HasKey("Id");

                    b.HasIndex("KundenId");

                    b.HasIndex("MaschinenId");

                    b.ToTable("GeplanterService","public");
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

                    b.Property<bool?>("IstAktiv")
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

                    b.Property<long>("BesitzerId");

                    b.Property<int?>("Betriebsdauer");

                    b.Property<bool?>("IstAktiv")
                        .IsRequired();

                    b.Property<int?>("Jahrgang");

                    b.Property<long>("MaschinentypId");

                    b.Property<string>("Mastnummer")
                        .HasMaxLength(128);

                    b.Property<string>("Motorennummer")
                        .HasMaxLength(128);

                    b.Property<string>("Notiz");

                    b.Property<string>("Seriennummer")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("BesitzerId");

                    b.HasIndex("MaschinentypId");

                    b.ToTable("Maschine","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.MaschinenRuecknahme", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datum");

                    b.Property<long>("MaschinenUebergabeId");

                    b.HasKey("Id");

                    b.HasIndex("MaschinenUebergabeId")
                        .IsUnique();

                    b.ToTable("MaschinenRuecknahme","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Maschinentyp", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Eigengewicht");

                    b.Property<string>("Fabrikat")
                        .HasMaxLength(128);

                    b.Property<int?>("Hubhoehe");

                    b.Property<int?>("Hubkraft");

                    b.Property<int?>("Maschinenbreite");

                    b.Property<int?>("Maschinenhoehe");

                    b.Property<int?>("Maschinenlaenge");

                    b.Property<string>("Motortyp")
                        .HasMaxLength(128);

                    b.Property<int?>("Nutzlast");

                    b.Property<int?>("Pneugroesse");

                    b.HasKey("Id");

                    b.ToTable("Maschinentyp","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.MaschinenUebergabe", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Datum");

                    b.Property<long>("ReservationsId");

                    b.HasKey("Id");

                    b.HasIndex("ReservationsId")
                        .IsUnique();

                    b.ToTable("MaschinenUebergabe","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Materialposten", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Anzahl");

                    b.Property<string>("Bezeichnung")
                        .HasMaxLength(256);

                    b.Property<long>("ServiceDurchfuehrungId");

                    b.Property<double>("Stueckpreis");

                    b.HasKey("Id");

                    b.HasIndex("ServiceDurchfuehrungId");

                    b.ToTable("Materialposten","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Reservation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Enddatum");

                    b.Property<long?>("KundenId");

                    b.Property<long>("MaschinenId");

                    b.Property<string>("Standort")
                        .HasMaxLength(256);

                    b.Property<DateTime>("Startdatum");

                    b.HasKey("Id");

                    b.HasIndex("KundenId");

                    b.HasIndex("MaschinenId");

                    b.ToTable("Reservationen","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.ServiceDurchfuehrung", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("GeplanterServiceId");

                    b.HasKey("Id");

                    b.HasIndex("GeplanterServiceId")
                        .IsUnique();

                    b.ToTable("ServiceDurchfuehrung","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Transaktion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Datum");

                    b.Property<long?>("KundenId");

                    b.Property<long>("MaschinenId");

                    b.Property<double>("Preis");

                    b.Property<int>("Typ");

                    b.HasKey("Id");

                    b.HasIndex("KundenId");

                    b.HasIndex("MaschinenId");

                    b.ToTable("Transaktionen","public");
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Arbeitsschritt", b =>
                {
                    b.HasOne("EasyMechBackend.DataAccessLayer.ServiceDurchfuehrung", "ServiceDurchfuehrung")
                        .WithMany("Arbeitsschritte")
                        .HasForeignKey("ServiceDurchfuehrungId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.GeplanterService", b =>
                {
                    b.HasOne("EasyMechBackend.DataAccessLayer.Kunde", "Kunde")
                        .WithMany("Services")
                        .HasForeignKey("KundenId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EasyMechBackend.DataAccessLayer.Maschine", "Maschine")
                        .WithMany("Services")
                        .HasForeignKey("MaschinenId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Maschine", b =>
                {
                    b.HasOne("EasyMechBackend.DataAccessLayer.Kunde", "Besitzer")
                        .WithMany("Maschinen")
                        .HasForeignKey("BesitzerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EasyMechBackend.DataAccessLayer.Maschinentyp", "Maschinentyp")
                        .WithMany("Maschinen")
                        .HasForeignKey("MaschinentypId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.MaschinenRuecknahme", b =>
                {
                    b.HasOne("EasyMechBackend.DataAccessLayer.MaschinenUebergabe", "MaschinenUebergabe")
                        .WithOne("Ruecknahme")
                        .HasForeignKey("EasyMechBackend.DataAccessLayer.MaschinenRuecknahme", "MaschinenUebergabeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.MaschinenUebergabe", b =>
                {
                    b.HasOne("EasyMechBackend.DataAccessLayer.Reservation", "Reservation")
                        .WithOne("Uebergabe")
                        .HasForeignKey("EasyMechBackend.DataAccessLayer.MaschinenUebergabe", "ReservationsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Materialposten", b =>
                {
                    b.HasOne("EasyMechBackend.DataAccessLayer.ServiceDurchfuehrung", "ServiceDurchfuehrung")
                        .WithMany("Materialposten")
                        .HasForeignKey("ServiceDurchfuehrungId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Reservation", b =>
                {
                    b.HasOne("EasyMechBackend.DataAccessLayer.Kunde", "Kunde")
                        .WithMany("Reservationen")
                        .HasForeignKey("KundenId");

                    b.HasOne("EasyMechBackend.DataAccessLayer.Maschine", "Maschine")
                        .WithMany("Reservationen")
                        .HasForeignKey("MaschinenId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.ServiceDurchfuehrung", b =>
                {
                    b.HasOne("EasyMechBackend.DataAccessLayer.GeplanterService", "GeplanterService")
                        .WithOne("ServiceDurchfuehrung")
                        .HasForeignKey("EasyMechBackend.DataAccessLayer.ServiceDurchfuehrung", "GeplanterServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EasyMechBackend.DataAccessLayer.Transaktion", b =>
                {
                    b.HasOne("EasyMechBackend.DataAccessLayer.Kunde", "Kunde")
                        .WithMany("Transaktionen")
                        .HasForeignKey("KundenId");

                    b.HasOne("EasyMechBackend.DataAccessLayer.Maschine", "Maschine")
                        .WithMany("Transaktionen")
                        .HasForeignKey("MaschinenId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
