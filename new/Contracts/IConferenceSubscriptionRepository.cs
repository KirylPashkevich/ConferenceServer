using ConferenceManager.Models;

namespace ConferenceManager.Contracts;

public interface IConferenceSubscriptionRepository
{
    IEnumerable<ConferenceSubscription> GetAllSubscriptions(bool trackChanges);
    IEnumerable<ConferenceSubscription> GetConferenceSubscriptions(int conferenceId, bool trackChanges);
    IEnumerable<ConferenceSubscription> GetUserSubscriptions(int userId, bool trackChanges);
    ConferenceSubscription? GetSubscription(int id, bool trackChanges);
    ConferenceSubscription? GetUserConferenceSubscription(int userId, int conferenceId, bool trackChanges);
    ConferenceSubscription CreateSubscription(ConferenceSubscription subscription);
    void DeleteSubscription(ConferenceSubscription subscription);
    bool IsUserSubscribedToConference(int userId, int conferenceId, bool trackChanges);
} 