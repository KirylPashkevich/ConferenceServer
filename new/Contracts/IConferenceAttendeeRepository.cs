using ConferenceManager.Models;

namespace ConferenceManager.Contracts;

public interface IConferenceAttendeeRepository
{
    IEnumerable<ConferenceAttendee> GetAllConferenceAttendees(bool trackChanges);
    IEnumerable<ConferenceAttendee> GetConferenceAttendees(int conferenceId, bool trackChanges);
    IEnumerable<ConferenceAttendee> GetUserAttendances(int userId, bool trackChanges);
    ConferenceAttendee? GetConferenceAttendee(int conferenceId, int userId, bool trackChanges);
    ConferenceAttendee CreateConferenceAttendee(ConferenceAttendee attendee);
    void DeleteConferenceAttendee(ConferenceAttendee attendee);
    bool IsUserAttendingConference(int userId, int conferenceId, bool trackChanges);
} 