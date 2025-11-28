using Microsoft.EntityFrameworkCore;
using MyApp.SyncAPI.Models;
using System;

namespace MyApp.SyncAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Hobby> Hobbies => Set<Hobby>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .ValueGeneratedNever();

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Age)
                      .IsRequired();

                entity.Property(e => e.IsDeleted)
                      .HasDefaultValue(false);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Hobby>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .ValueGeneratedNever(); 

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Description)
                      .HasMaxLength(1000)
                      .IsRequired(false);

                entity.Property(e => e.ProficiencyLevel)
                      .HasMaxLength(100)
                      .IsRequired(false);

                entity.Property(e => e.IsDeleted)
                      .HasDefaultValue(false);

                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                // Relationship: Hobby -> User (many-to-one)
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Hobbies)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserHobby>(entity =>
            {
                entity.HasKey(uh => new { uh.UserId, uh.HobbyId }); 

                entity.HasOne(uh => uh.User)
                      .WithMany(u => u.UserHobbies)
                      .HasForeignKey(uh => uh.UserId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(uh => uh.Hobby)
                      .WithMany(h => h.UserHobbies)
                      .HasForeignKey(uh => uh.HobbyId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
