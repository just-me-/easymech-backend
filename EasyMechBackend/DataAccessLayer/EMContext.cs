using Microsoft.EntityFrameworkCore;
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

        public DbSet<GeplanterService> GeplanteServices { get; set; }
        public DbSet<ServiceDurchfuehrung> ServiceDurchfuehrungen { get; set; }
        public DbSet<Materialposten> Materialposten { get; set; }
        public DbSet<Arbeitsschritt> Arbeitsschritte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Kunde>().ForNpgsqlUseXminAsConcurrencyToken();

            //TODO:
            //Subject to change: You deleted all machines of a certain type
            //What happens if you delete the machine type?
            //only hard-deletion supported (no ianactive flag)
            //Suggestion: Cascade. If it's not cascade, you hardly can delete a machinetyp as soon as it was once assigned to a machine.
            //you first need to hard delete all machines... all manually...

            //(no) Option: SetNull: But then the Machinetype-Property must be optional which we decided not to do.
            //The business logic ensures you will not delete an active in-use machine type.
            modelBuilder.Entity<Maschinentyp>()
                .HasMany(t => t.Maschinen)
                .WithOne(t => t.Maschinentyp)
                .OnDelete(DeleteBehavior.Cascade);

            //TODO:
            //Subject to change: You deleted all machines belonging to a customer.
            //What happens tp the machines if you hard-delete the customer?
            //Suggestion: Also Hard-Delete the machines as this is an admin operation, not
            //a user operation. The user will set things only to inactive.
            modelBuilder.Entity<Kunde>()
                .HasMany(t => t.Maschinen)
                .WithOne(t => t.Besitzer)
                .OnDelete(DeleteBehavior.Cascade);

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
                //string connection = Startup.Configuration.GetConnectionString("DefaultConnection")
            }
        }

    }

}
