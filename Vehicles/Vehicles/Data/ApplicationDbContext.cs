using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Vehicles.Entities;

namespace Vehicles.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Detail> Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>()
                .HasOne(bc => bc.VehicleType)
                .WithMany(b => b.Vehicles)
                .HasForeignKey(bc => bc.VehicleTypeId);
            modelBuilder.Entity<Vehicle>()
                .HasOne(bc => bc.Brand)
                .WithMany(b => b.Vehicles)
                .HasForeignKey(bc => bc.BrandId);

            modelBuilder.Entity<History>()
                .HasOne(bc => bc.Vehicle)
                .WithMany(b => b.Histories)
                .HasForeignKey(bc => bc.VehicleId);

            modelBuilder.Entity<Detail>()
                .HasOne(bc => bc.History)
                .WithMany(b => b.Details)
                .HasForeignKey(bc => bc.HistoryId);
            modelBuilder.Entity<Detail>()
                .HasOne(bc => bc.Procedure)
                .WithMany(b => b.Details)
                .HasForeignKey(bc => bc.ProcedureId);


            base.OnModelCreating(modelBuilder);
        }

    }
}
