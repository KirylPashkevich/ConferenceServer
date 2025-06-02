using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceManager.Models;

public class ConferenceSponsor
{
    public int ConferenceId { get; set; }
    public int SponsorId { get; set; }

    [ForeignKey("ConferenceId")]
    public virtual Conference Conference { get; set; } = null!;

    [ForeignKey("SponsorId")]
    public virtual Sponsor Sponsor { get; set; } = null!;
} 