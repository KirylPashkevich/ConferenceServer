using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceManager.Models;

public class Conference
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? LocationId { get; set; }

    [ForeignKey("LocationId")]
    public virtual Location? Location { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int? OrganizerId { get; set; }

    [ForeignKey("OrganizerId")]
    public virtual User? Organizer { get; set; }

    public bool IsAnnounced { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<ConferenceOrganizer> Organizers { get; set; } = new List<ConferenceOrganizer>();
    public virtual ICollection<ConferenceAttendee> Attendees { get; set; } = new List<ConferenceAttendee>();
    public virtual ICollection<Presentation> Presentations { get; set; } = new List<Presentation>();
    public virtual ICollection<ConferenceSubscription> Subscriptions { get; set; } = new List<ConferenceSubscription>();
    public virtual ICollection<Sponsor> Sponsors { get; set; } = new List<Sponsor>();
} 