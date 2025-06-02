using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models.DTOs;

public class PresentationDto
{
    [Required]
    public int ConferenceId { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = null!;

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public string Room { get; set; } = null!;

    public List<int>? SpeakerIds { get; set; }
}

public class PresentationUpdateDto
{
    [StringLength(100)]
    public string? Title { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? Room { get; set; }

    public List<int>? SpeakerIds { get; set; }
}

public class PresentationSpeakerDto
{
    [Required]
    public int PresentationId { get; set; }

    [Required]
    public int UserId { get; set; }
} 