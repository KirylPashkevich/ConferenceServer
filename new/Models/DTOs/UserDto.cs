using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models.DTOs;

public class UserDto
{
    [Required]
    [StringLength(255)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    public int? RoleId { get; set; }

    // UserProfile properties
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(500)]
    public string Bio { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Affiliation { get; set; } = null!;

    [Required]
    [Url]
    [StringLength(200)]
    public string Website { get; set; } = null!;

    [Required]
    [Phone]
    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string ProfilePicture { get; set; } = null!;
}

public class UserProfileDto
{
    [Required]
    public int UserId { get; set; }

    [StringLength(255)]
    public string? FirstName { get; set; }

    [StringLength(255)]
    public string? LastName { get; set; }

    public string? Bio { get; set; }

    [StringLength(255)]
    public string? Affiliation { get; set; }

    [StringLength(255)]
    public string? Website { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    public string? ProfilePicture { get; set; }
}

public class UserCreateDto
{
    [Required]
    [StringLength(255)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(255)]
    public string Password { get; set; } = null!;

    [Required]
    [EmailAddress]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    public int? RoleId { get; set; }

    // UserProfile properties
    [StringLength(255)]
    public string? FirstName { get; set; }

    [StringLength(255)]
    public string? LastName { get; set; }

    public string? Bio { get; set; }

    [StringLength(255)]
    public string? Affiliation { get; set; }

    [StringLength(255)]
    public string? Website { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    public string? ProfilePicture { get; set; }
}

public class UserUpdateDto
{
    [StringLength(255)]
    public string? Username { get; set; }

    [StringLength(255)]
    public string? Password { get; set; }

    [EmailAddress]
    [StringLength(255)]
    public string? Email { get; set; }

    public int? RoleId { get; set; }

    // UserProfile properties
    [StringLength(255)]
    public string? FirstName { get; set; }

    [StringLength(255)]
    public string? LastName { get; set; }

    public string? Bio { get; set; }

    [StringLength(255)]
    public string? Affiliation { get; set; }

    [StringLength(255)]
    public string? Website { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [StringLength(255)]
    public string? ProfilePicture { get; set; }
}

public class UserResponseDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int? RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Bio { get; set; }
    public string? Affiliation { get; set; }
    public string? Website { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ProfilePicture { get; set; }
} 