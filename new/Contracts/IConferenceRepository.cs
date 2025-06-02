using ConferenceManager.Models;

namespace ConferenceManager.Contracts;

public interface IConferenceRepository
{
    IEnumerable<Conference> GetAllConferences(bool trackChanges);
    Conference? GetConference(int id, bool trackChanges);
    Conference CreateConference(Conference conference);
    void DeleteConference(Conference conference);
    void UpdateConference(Conference conference);
} 