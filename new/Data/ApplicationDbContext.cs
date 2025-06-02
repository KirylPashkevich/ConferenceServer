using Microsoft.EntityFrameworkCore;
using ConferenceManager.Models;

namespace ConferenceManager.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Conference> Conferences { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<LocationReview> LocationReviews { get; set; }
    public DbSet<Presentation> Presentations { get; set; }
    public DbSet<Sponsor> Sponsors { get; set; }
    public DbSet<ConferenceAttendee> ConferenceAttendees { get; set; }
    public DbSet<ConferenceOrganizer> ConferenceOrganizers { get; set; }
    public DbSet<ConferenceSubscription> ConferenceSubscriptions { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure unique constraints
        modelBuilder.Entity<Role>()
            .HasIndex(r => r.Name)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Configure composite keys for many-to-many relationships
        modelBuilder.Entity<ConferenceOrganizer>()
            .HasKey(co => new { co.ConferenceId, co.UserId });

        modelBuilder.Entity<ConferenceAttendee>()
            .HasKey(ca => new { ca.ConferenceId, ca.UserId });

        // Configure relationships
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserProfile)
            .WithOne(up => up.User)
            .HasForeignKey<UserProfile>(up => up.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<Conference>()
            .HasOne(c => c.Location)
            .WithMany(l => l.Conferences)
            .HasForeignKey(c => c.LocationId);

        modelBuilder.Entity<Conference>()
            .HasOne(c => c.Organizer)
            .WithMany(u => u.OrganizedConferences)
            .HasForeignKey(c => c.OrganizerId);

        modelBuilder.Entity<LocationReview>()
            .HasOne(lr => lr.Location)
            .WithMany(l => l.Reviews)
            .HasForeignKey(lr => lr.LocationId);

        modelBuilder.Entity<LocationReview>()
            .HasOne(lr => lr.User)
            .WithMany(u => u.LocationReviews)
            .HasForeignKey(lr => lr.UserId);

        // Configure cascade delete behavior
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserProfile)
            .WithOne(up => up.User)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Conference>()
            .HasMany(c => c.Sponsors)
            .WithMany(s => s.Conferences)
            .UsingEntity(j => j.ToTable("ConferenceSponsors"));

        modelBuilder.Entity<Conference>()
            .HasMany(c => c.Presentations)
            .WithOne(p => p.Conference)
            .HasForeignKey(p => p.ConferenceId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Location>()
            .HasMany(l => l.Reviews)
            .WithOne(r => r.Location)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Presentation
        modelBuilder.Entity<Presentation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.Room).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();

            entity.HasOne(e => e.Conference)
                .WithMany(c => c.Presentations)
                .HasForeignKey(e => e.ConferenceId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Speakers)
                .WithMany(u => u.Presentations)
                .UsingEntity(j => j.ToTable("PresentationSpeakers"));
        });
    }
} 