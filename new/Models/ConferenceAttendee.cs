using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceManager.Models;

public class ConferenceAttendee
{
    public int ConferenceId { get; set; }
    public int UserId { get; set; }

    [ForeignKey("ConferenceId")]
    public virtual Conference Conference { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
} 