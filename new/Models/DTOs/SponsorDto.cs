using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models.DTOs;

public class SponsorDto
{
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
}

public class SponsorUpdateDto
{
    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Url]
    [StringLength(200)]
    public string? Website { get; set; }

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
}

public class ConferenceSponsorDto
{
    [Required]
    public int ConferenceId { get; set; }

    [Required]
    public int SponsorId { get; set; }
} 