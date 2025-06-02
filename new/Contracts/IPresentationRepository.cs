using ConferenceManager.Models;

namespace ConferenceManager.Contracts;

public interface IPresentationRepository
{
    IEnumerable<Presentation> GetAllPresentations(bool includeSpeakers);
    Presentation? GetPresentation(int id, bool includeSpeakers);
    IEnumerable<Presentation> GetConferencePresentations(int conferenceId, bool includeSpeakers);
    IEnumerable<User> GetPresentationSpeakers(int presentationId, bool includeProfile);
    Presentation CreatePresentation(Presentation presentation);
    void UpdatePresentation(Presentation presentation);
    void DeletePresentation(Presentation presentation);
    void AddSpeakerToPresentation(int presentationId, int userId);
    void RemoveSpeakerFromPresentation(int presentationId, int userId);
} 