using ConferenceManager.Models;

namespace ConferenceManager.Contracts;

public interface IConferenceOrganizerRepository
{
    IEnumerable<ConferenceOrganizer> GetAllConferenceOrganizers(bool trackChanges);
    IEnumerable<ConferenceOrganizer> GetConferenceOrganizers(int conferenceId, bool trackChanges);
    IEnumerable<ConferenceOrganizer> GetUserOrganizations(int userId, bool trackChanges);
    ConferenceOrganizer? GetConferenceOrganizer(int conferenceId, int userId, bool trackChanges);
    ConferenceOrganizer CreateConferenceOrganizer(ConferenceOrganizer organizer);
    void DeleteConferenceOrganizer(ConferenceOrganizer organizer);
    bool IsUserOrganizingConference(int userId, int conferenceId, bool trackChanges);
} 