using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models;

public class Location
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Address { get; set; }

    [StringLength(255)]
    public string? City { get; set; }

    [StringLength(255)]
    public string? Country { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public virtual ICollection<Conference> Conferences { get; set; } = new List<Conference>();
    public virtual ICollection<LocationReview> Reviews { get; set; } = new List<LocationReview>();
} 