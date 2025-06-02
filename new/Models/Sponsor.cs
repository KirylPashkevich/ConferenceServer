using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models;

public class Sponsor
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = null!;

    [Required]
    [Url]
    [StringLength(200)]
    public string Website { get; set; } = null!;

    [StringLength(200)]
    public string? LogoUrl { get; set; }

    [StringLength(100)]
    public string? ContactPerson { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? ContactEmail { get; set; }

    [Phone]
    [StringLength(20)]
    public string? ContactPhone { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation property
    public virtual ICollection<Conference> Conferences { get; set; } = new List<Conference>();
} 