using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models.DTOs;

public class NotificationDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public string Message { get; set; } = null!;

    [Required]
    public string Type { get; set; } = null!;

    public string? Data { get; set; }

    public bool IsRead { get; set; } = false;
}

public class NotificationUpdateDto
{
    public string? Message { get; set; }

    public string? Type { get; set; }

    public string? Data { get; set; }

    public bool? IsRead { get; set; }
} 