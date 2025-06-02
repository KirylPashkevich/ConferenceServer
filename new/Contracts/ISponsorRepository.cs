using ConferenceManager.Models;

namespace ConferenceManager.Contracts;

public interface ISponsorRepository
{
    IEnumerable<Sponsor> GetAllSponsors(bool trackChanges);
    Sponsor? GetSponsor(int id, bool trackChanges);
    Sponsor CreateSponsor(Sponsor sponsor);
    void DeleteSponsor(Sponsor sponsor);
    void UpdateSponsor(Sponsor sponsor);
    IEnumerable<Sponsor> GetConferenceSponsors(int conferenceId, bool trackChanges);
    void AddSponsorToConference(int conferenceId, int sponsorId);
    void RemoveSponsorFromConference(int conferenceId, int sponsorId);
} 