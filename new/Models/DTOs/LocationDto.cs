using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models.DTOs;

public class LocationDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string City { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Country { get; set; } = null!;

    [StringLength(1000)]
    public string? Description { get; set; }
}

public class LocationUpdateDto
{
    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(100)]
    public string? Country { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }
}

public class LocationReviewDto
{
    [Required]
    public int LocationId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    [StringLength(1000)]
    public string Comment { get; set; } = null!;
} 