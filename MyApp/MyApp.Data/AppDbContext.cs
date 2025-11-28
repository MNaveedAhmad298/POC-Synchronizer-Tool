using Microsoft.EntityFrameworkCore;
using MyApp.Core.Entities;
using System;

namespace MyApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Hobby> Hobbies => Set<Hobby>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hobby>(entity =>
            {
                entity.HasKey(h => h.Id);
                entity.Property(h => h.Id).ValueGeneratedNever();
                entity.Property(h => h.Name).IsRequired().HasMaxLength(200);
                entity.HasOne(h => h.User)
                      .WithMany(u => u.Hobbies)
                      .HasForeignKey(h => h.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>()
                .Property(u => u.IsSynced)
                .HasDefaultValue(false); 

            modelBuilder.Entity<User>()
                .Property(u => u.IsDeleted)
                .HasDefaultValue(false); 

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<UserHobby>()
                .HasKey(uh => new { uh.UserId, uh.HobbyId });

            modelBuilder.Entity<UserHobby>()
                .HasOne(uh => uh.User)
                .WithMany(u => u.UserHobbies)
                .HasForeignKey(uh => uh.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserHobby>()
                .HasOne(uh => uh.Hobby)
                .WithMany(h => h.UserHobbies)
                .HasForeignKey(uh => uh.HobbyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserHobby>().HasData(
            new UserHobby
            {
                UserId = Guid.Parse("64614F8F-5731-442E-B581-AACED7C2741B"),
                HobbyId = Guid.Parse("A2311CC1-F0AA-49CF-9ADF-927734BF9398"),
                SkillLevel = "Expert",
                IsSynced = false,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }
    }
}
