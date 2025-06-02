using ConferenceManager.Contracts;
using ConferenceManager.Data;
using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Repositories;

public class ConferenceOrganizerRepository : IConferenceOrganizerRepository
{
    private readonly ApplicationDbContext _context;

    public ConferenceOrganizerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ConferenceOrganizer> GetAllConferenceOrganizers(bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceOrganizers
                .Include(co => co.Conference)
                .Include(co => co.User)
                .ToList()
            : _context.ConferenceOrganizers
                .Include(co => co.Conference)
                .Include(co => co.User)
                .AsNoTracking()
                .ToList();
    }

    public IEnumerable<ConferenceOrganizer> GetConferenceOrganizers(int conferenceId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceOrganizers
                .Include(co => co.User)
                .Where(co => co.ConferenceId == conferenceId)
                .ToList()
            : _context.ConferenceOrganizers
                .Include(co => co.User)
                .Where(co => co.ConferenceId == conferenceId)
                .AsNoTracking()
                .ToList();
    }

    public IEnumerable<ConferenceOrganizer> GetUserOrganizations(int userId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceOrganizers
                .Include(co => co.Conference)
                .Where(co => co.UserId == userId)
                .ToList()
            : _context.ConferenceOrganizers
                .Include(co => co.Conference)
                .Where(co => co.UserId == userId)
                .AsNoTracking()
                .ToList();
    }

    public ConferenceOrganizer? GetConferenceOrganizer(int conferenceId, int userId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceOrganizers
                .Include(co => co.Conference)
                .Include(co => co.User)
                .FirstOrDefault(co => co.ConferenceId == conferenceId && co.UserId == userId)
            : _context.ConferenceOrganizers
                .Include(co => co.Conference)
                .Include(co => co.User)
                .AsNoTracking()
                .FirstOrDefault(co => co.ConferenceId == conferenceId && co.UserId == userId);
    }

    public ConferenceOrganizer CreateConferenceOrganizer(ConferenceOrganizer organizer)
    {
        _context.ConferenceOrganizers.Add(organizer);
        _context.SaveChanges();
        return organizer;
    }

    public void DeleteConferenceOrganizer(ConferenceOrganizer organizer)
    {
        _context.ConferenceOrganizers.Remove(organizer);
        _context.SaveChanges();
    }

    public bool IsUserOrganizingConference(int userId, int conferenceId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceOrganizers
                .Any(co => co.UserId == userId && co.ConferenceId == conferenceId)
            : _context.ConferenceOrganizers
                .AsNoTracking()
                .Any(co => co.UserId == userId && co.ConferenceId == conferenceId);
    }
} 