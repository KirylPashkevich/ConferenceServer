using ConferenceManager.Contracts;
using ConferenceManager.Data;
using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Repositories;

public class ConferenceRepository : IConferenceRepository
{
    private readonly ApplicationDbContext _context;

    public ConferenceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Conference> GetAllConferences(bool trackChanges)
    {
        return trackChanges
            ? _context.Conferences
                .Include(c => c.Location)
                .Include(c => c.Organizer)
                .ToList()
            : _context.Conferences
                .Include(c => c.Location)
                .Include(c => c.Organizer)
                .AsNoTracking()
                .ToList();
    }

    public Conference? GetConference(int id, bool trackChanges)
    {
        return trackChanges
            ? _context.Conferences
                .Include(c => c.Location)
                .Include(c => c.Organizer)
                .FirstOrDefault(c => c.Id == id)
            : _context.Conferences
                .Include(c => c.Location)
                .Include(c => c.Organizer)
                .AsNoTracking()
                .FirstOrDefault(c => c.Id == id);
    }

    public Conference CreateConference(Conference conference)
    {
        _context.Conferences.Add(conference);
        _context.SaveChanges();
        return conference;
    }

    public void DeleteConference(Conference conference)
    {
        _context.Conferences.Remove(conference);
        _context.SaveChanges();
    }

    public void UpdateConference(Conference conference)
    {
        _context.Conferences.Update(conference);
        _context.SaveChanges();
    }
} 