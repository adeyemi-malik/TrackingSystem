using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.DLayer.Models
{
    public class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppLog>().HasIndex(a => a.Key);

            modelBuilder.Entity<ApiClient>().HasIndex(a => a.ClientName)
                .IsUnique();

            modelBuilder.Entity<Role>().HasMany(u => u.UserRoles)
           .WithOne(u => u.Role).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Role>().HasIndex(r => r.Name)
             .IsUnique();

            modelBuilder.Entity<User>().HasMany(u => u.UserRoles)
               .WithOne(u => u.User).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasOne(u => u.Farmer)
               .WithOne(f => f.User).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasIndex(u => u.UserName)
            .IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<UserRole>().HasOne(u => u.User)
              .WithMany(u => u.UserRoles).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserRole>().HasOne(u => u.Role)
              .WithMany(r => r.UserRoles).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Animal>().HasMany(a => a.AnimalTrackers)
              .WithOne(a => a.Animal).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Animal>().HasMany(a => a.FarmerAnimals)
             .WithOne(f => f.Animal).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Animal>().HasIndex(a => a.Tag)
           .IsUnique();

            modelBuilder.Entity<AnimalTracker>().HasOne(a => a.Animal)
             .WithMany(a => a.AnimalTrackers).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AnimalTracker>().HasOne(a => a.Tracker)
            .WithMany(t => t.AnimalTrackers).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AnimalTracker>().HasMany(a => a.Locations)
            .WithOne(l => l.AnimalTracker).OnDelete(DeleteBehavior.Restrict);
         

            modelBuilder.Entity<Farmer>().HasMany(f => f.FarmerAnimals)
            .WithOne(f => f.Farmer).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Farmer>().HasOne(f => f.User)
          .WithOne(u => u.Farmer).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FarmerAnimal>().HasOne(f => f.Farmer)
           .WithMany(f => f.FarmerAnimals).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FarmerAnimal>().HasOne(f => f.Animal)
          .WithMany(a => a.FarmerAnimals).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>().HasOne(l => l.AnimalTracker)
           .WithMany(a => a.Locations).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tracker>().HasMany(t => t.AnimalTrackers)
          .WithOne(a => a.Tracker).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Tracker>().HasIndex(t => t.Tag)
          .IsUnique();            
        }

        public DbSet<ApiClient> ApiClients { get; set; }

        public DbSet<AppLog> AppLogs { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Tracker> Trackers { get; set; }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<AnimalTracker> AnimalTrackers { get; set; }

        public DbSet<Farmer> Farmers { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<FarmerAnimal> FarmerAnimals { get; set; }


    }
}
