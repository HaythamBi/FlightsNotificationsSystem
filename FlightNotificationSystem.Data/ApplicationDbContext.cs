using Microsoft.EntityFrameworkCore;
using FlightNotificationSystem.Data.Models;

namespace FlightNotificationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<UserAlert> UserAlerts { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

            modelBuilder.Entity<Flight>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Alert>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<UserAlert>()
                .HasKey(ua => ua.UserAlertId);

            modelBuilder.Entity<UserAlert>()
                .HasOne(ua => ua.Alert)
                .WithMany()
                .HasForeignKey(ua => ua.AlertId);

            // Configure the User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            });

            // Configure the Flight entity
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FlightNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Airline).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
            });

            // Configure the Alert entity
            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FlightNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.TargetPrice).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.AlertDate).IsRequired();

                entity.HasOne(a => a.User)
                      .WithMany()
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
