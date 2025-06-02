using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models.DTOs;

public class ConferenceDto
{
    [Required]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public int LocationId { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int OrganizerId { get; set; }

    public bool IsAnnounced { get; set; } = false;
} 