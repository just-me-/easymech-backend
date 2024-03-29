﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using EasyMechBackend.DataAccessLayer.Entities;

namespace EasyMechBackend.DataAccessLayer
{
    public class EMContext : DbContext
    {
        public EMContext(DbContextOptions<EMContext> options) : base(options) { }
        public EMContext() { }


        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Maschine> Maschinen { get; set; }
        public DbSet<Maschinentyp> Maschinentypen { get; set; }

        public DbSet<Transaktion> Transaktionen { get; set; }

        public DbSet<Reservation> Reservationen { get; set; }
        public DbSet<MaschinenUebergabe> MaschinenUebergaben { get; set; }
        public DbSet<MaschinenRuecknahme> MaschinenRuecknahmen { get; set; }

        public DbSet<Service> Services { get; set; }
        public DbSet<Materialposten> Materialposten { get; set; }
        public DbSet<Arbeitsschritt> Arbeitsschritte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Maschinentyp>()
                .HasMany(t => t.Maschinen)
                .WithOne(t => t.Maschinentyp)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Kunde>()
                .HasMany(t => t.Maschinen)
                .WithOne(t => t.Besitzer)
                .OnDelete(DeleteBehavior.Cascade);
        }


        public static readonly LoggerFactory LoggerFactory = new LoggerFactory(
        new[] { new ConsoleLoggerProvider((_, logLevel) => logLevel >= LogLevel.Information, true) }
        );

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseLoggerFactory(LoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
                    .UseNpgsql("Host=sinv-56057.edu.hsr.ch;Port=40005;Username=em;Password=em19;Database=easymech;");
                //string connection = Startup.Configuration.GetConnectionString("DefaultConnection")
            }
        }

    }

}
