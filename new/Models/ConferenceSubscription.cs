using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceManager.Models;

public class ConferenceSubscription
{
    [Key]
    public int Id { get; set; }

    public int ConferenceId { get; set; }

    [ForeignKey("ConferenceId")]
    public virtual Conference Conference { get; set; } = null!;

    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
} 