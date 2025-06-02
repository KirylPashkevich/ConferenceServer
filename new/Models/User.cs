using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceManager.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Required]
    [StringLength(255)]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public int? RoleId { get; set; }

    [ForeignKey("RoleId")]
    public virtual Role? Role { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual UserProfile? UserProfile { get; set; }
    public virtual ICollection<Conference> OrganizedConferences { get; set; } = new List<Conference>();
    public virtual ICollection<ConferenceOrganizer> ConferenceOrganizations { get; set; } = new List<ConferenceOrganizer>();
    public virtual ICollection<ConferenceAttendee> ConferenceAttendances { get; set; } = new List<ConferenceAttendee>();
    public virtual ICollection<LocationReview> LocationReviews { get; set; } = new List<LocationReview>();
    public virtual ICollection<ConferenceSubscription> ConferenceSubscriptions { get; set; } = new List<ConferenceSubscription>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public virtual ICollection<Presentation> Presentations { get; set; } = new List<Presentation>();
} 