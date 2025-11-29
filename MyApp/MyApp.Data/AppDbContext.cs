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
                UserId = Guid.Parse("CF763AC7-7D37-402C-919A-CABF7E1FE9FA"),
                HobbyId = Guid.Parse("6D005B97-0D1B-4C17-BC21-050B463BE6EC"),
                SkillLevel = "Expert",
                IsSynced = false,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }
    }
}
