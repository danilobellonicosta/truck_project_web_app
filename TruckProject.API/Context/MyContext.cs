using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TruckProject.API.Models;

namespace TruckProject.API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Truck> Trucks { get; set; }
        public DbSet<ModelTruck> ModelTrucks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            DataSeed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        protected void DataSeed(ModelBuilder modelBuilder)
        {
            var modelTruckOne = new ModelTruck
            {
                Id = Guid.NewGuid(),
                Model = "FC"
            };
            var modelTruckTwo = new ModelTruck
            {
                Id = Guid.NewGuid(),
                Model = "FH"
            };
            var modelTruckThree = new ModelTruck
            {
                Id = Guid.NewGuid(),
                Model = "FM"
            };

            modelBuilder.Entity<ModelTruck>().HasData(modelTruckOne);
            modelBuilder.Entity<ModelTruck>().HasData(modelTruckTwo);
            modelBuilder.Entity<ModelTruck>().HasData(modelTruckThree);
        }
    }
}
