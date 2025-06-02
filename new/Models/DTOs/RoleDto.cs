using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models.DTOs;

public class RoleDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = null!;
} 