using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceManager.Models;

public class Presentation
{
    [Key]
    public int Id { get; set; }

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

    // Навигационные свойства
    public Conference Conference { get; set; } = null!;
    public ICollection<User> Speakers { get; set; } = new List<User>();

    public bool IsApproved { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
} 