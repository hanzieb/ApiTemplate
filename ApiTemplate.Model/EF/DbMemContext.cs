using ApiTemplate.Model.EF.Entities;
using ApiTemplate.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;

namespace ApiTemplate.Model.EF
{
    public class DbMemContext : DbContext
    {
        public virtual DbSet<Animal> Animals { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        public DbMemContext(DbContextOptions<DbMemContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasMany(e => e.Photos);
                entity.Property(e => e.AnimalType)
                    .HasConversion(v => (int)v, v => (AnimalTypes)v);
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(c => c.Animal);
            });
        }
    }
}
