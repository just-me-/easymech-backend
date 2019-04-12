using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMechBackend.DataAccessLayer
{
    public class EMContext : DbContext
    {
        public EMContext(DbContextOptions<EMContext> options) : base(options) { }
        public EMContext() : base() { } //Das son Ding jetzt hier...

        public DbSet<Kunde> Kunden { get; set; }
        //public DbSet<Maschine> Maschinen { get; set; }
        //public DbSet<Fahrzeugtyp> Fahrzeugtypen { get; set; }

        //public DbSet<Reservation> Reservationen { get; set; }
        //public DbSet<Transaktion> Transaktion { get; set; }
        //public DbSet<GeplanterService> GeplanteServices { get; set; }

        //public DbSet<FahrzeugUebergabe> FahrzeugUebergaben { get; set; }
        //public DbSet<FahrzeugRuecknahme> FahrzeugRuecknahmen { get; set; }
        //public DbSet<ServiceDurchfuehrung> ServiceDurchfuehrungen { get; set; }
        //public DbSet<Materialposten> Materialposten { get; set; }
        //public DbSet<Arbeitsschritt> Arbeitsschritte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kunde>().ForNpgsqlUseXminAsConcurrencyToken();
        }

        //Logging
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
                //Todo: string aus config abgreifen:
                //string connection = Startup.Configuration.GetConnectionString("DefaultConnection") <--Aufwärtsref
            }
        }

    }

}
