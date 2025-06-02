using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models.DTOs;

public class ConferenceUpdateDto
{
    [StringLength(255)]
    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? LocationId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? OrganizerId { get; set; }

    public bool? IsAnnounced { get; set; }
} 