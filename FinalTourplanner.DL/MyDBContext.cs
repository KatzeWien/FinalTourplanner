using FinalTourplanner.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTourplanner.DL
{
    public class MyDBContext : DbContext 
    {
        public DbSet<Tour> Tour => Set<Tour>();
        public DbSet<TourLog> TourLog => Set<TourLog>();

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("tour");
                entity.HasKey(t => t.Name);
                foreach (var property in entity.Metadata.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }
            });
            modelBuilder.Entity<TourLog>(entity =>
            {
                entity.ToTable("tourlog");
                entity.HasKey(t => t.Id);
                foreach (var property in entity.Metadata.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }
            });
        }
    }
}
