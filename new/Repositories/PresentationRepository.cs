using ConferenceManager.Contracts;
using ConferenceManager.Data;
using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Repositories;

public class PresentationRepository : IPresentationRepository
{
    private readonly ApplicationDbContext _context;

    public PresentationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Presentation> GetAllPresentations(bool includeSpeakers)
    {
        var query = _context.Presentations
            .Include(p => p.Conference)
            .AsQueryable();

        if (includeSpeakers)
        {
            query = query.Include(p => p.Speakers);
        }

        return query.ToList();
    }

    public Presentation? GetPresentation(int id, bool includeSpeakers)
    {
        var query = _context.Presentations
            .Include(p => p.Conference)
            .AsQueryable();

        if (includeSpeakers)
        {
            query = query.Include(p => p.Speakers);
        }

        return query.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Presentation> GetConferencePresentations(int conferenceId, bool includeSpeakers)
    {
        var query = _context.Presentations
            .Include(p => p.Conference)
            .Where(p => p.ConferenceId == conferenceId)
            .AsQueryable();

        if (includeSpeakers)
        {
            query = query.Include(p => p.Speakers);
        }

        return query.ToList();
    }

    public IEnumerable<User> GetPresentationSpeakers(int presentationId, bool includeProfile)
    {
        var query = _context.Presentations
            .Include(p => p.Speakers)
            .FirstOrDefault(p => p.Id == presentationId)
            ?.Speakers
            .AsQueryable();

        if (includeProfile)
        {
            query = query?.Include(u => u.UserProfile);
        }

        return query?.ToList() ?? new List<User>();
    }

    public Presentation CreatePresentation(Presentation presentation)
    {
        _context.Presentations.Add(presentation);
        _context.SaveChanges();
        return presentation;
    }

    public void UpdatePresentation(Presentation presentation)
    {
        _context.Entry(presentation).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeletePresentation(Presentation presentation)
    {
        _context.Presentations.Remove(presentation);
        _context.SaveChanges();
    }

    public void AddSpeakerToPresentation(int presentationId, int userId)
    {
        var presentation = _context.Presentations
            .Include(p => p.Speakers)
            .FirstOrDefault(p => p.Id == presentationId);

        var user = _context.Users.Find(userId);

        if (presentation != null && user != null)
        {
            presentation.Speakers.Add(user);
            _context.SaveChanges();
        }
    }

    public void RemoveSpeakerFromPresentation(int presentationId, int userId)
    {
        var presentation = _context.Presentations
            .Include(p => p.Speakers)
            .FirstOrDefault(p => p.Id == presentationId);

        var user = _context.Users.Find(userId);

        if (presentation != null && user != null)
        {
            presentation.Speakers.Remove(user);
            _context.SaveChanges();
        }
    }
} 