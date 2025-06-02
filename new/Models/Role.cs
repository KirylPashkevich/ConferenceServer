using System.ComponentModel.DataAnnotations;

namespace ConferenceManager.Models;

public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    // Navigation property
    public virtual ICollection<User> Users { get; set; } = new List<User>();
} 