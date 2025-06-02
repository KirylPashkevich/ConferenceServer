using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models.DTOs;

public class ConferenceOrganizerDto
{
    [Required]
    public int ConferenceId { get; set; }

    [Required]
    public int UserId { get; set; }
} 