using ConferenceManager.Contracts;
using ConferenceManager.Data;
using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Repositories;

public class ConferenceAttendeeRepository : IConferenceAttendeeRepository
{
    private readonly ApplicationDbContext _context;

    public ConferenceAttendeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ConferenceAttendee> GetAllConferenceAttendees(bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceAttendees
                .Include(ca => ca.Conference)
                .Include(ca => ca.User)
                .ToList()
            : _context.ConferenceAttendees
                .Include(ca => ca.Conference)
                .Include(ca => ca.User)
                .AsNoTracking()
                .ToList();
    }

    public IEnumerable<ConferenceAttendee> GetConferenceAttendees(int conferenceId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceAttendees
                .Include(ca => ca.User)
                .Where(ca => ca.ConferenceId == conferenceId)
                .ToList()
            : _context.ConferenceAttendees
                .Include(ca => ca.User)
                .Where(ca => ca.ConferenceId == conferenceId)
                .AsNoTracking()
                .ToList();
    }

    public IEnumerable<ConferenceAttendee> GetUserAttendances(int userId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceAttendees
                .Include(ca => ca.Conference)
                .Where(ca => ca.UserId == userId)
                .ToList()
            : _context.ConferenceAttendees
                .Include(ca => ca.Conference)
                .Where(ca => ca.UserId == userId)
                .AsNoTracking()
                .ToList();
    }

    public ConferenceAttendee? GetConferenceAttendee(int conferenceId, int userId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceAttendees
                .Include(ca => ca.Conference)
                .Include(ca => ca.User)
                .FirstOrDefault(ca => ca.ConferenceId == conferenceId && ca.UserId == userId)
            : _context.ConferenceAttendees
                .Include(ca => ca.Conference)
                .Include(ca => ca.User)
                .AsNoTracking()
                .FirstOrDefault(ca => ca.ConferenceId == conferenceId && ca.UserId == userId);
    }

    public ConferenceAttendee CreateConferenceAttendee(ConferenceAttendee conferenceAttendee)
    {
        _context.ConferenceAttendees.Add(conferenceAttendee);
        _context.SaveChanges();
        return conferenceAttendee;
    }

    public void DeleteConferenceAttendee(ConferenceAttendee conferenceAttendee)
    {
        _context.ConferenceAttendees.Remove(conferenceAttendee);
        _context.SaveChanges();
    }

    public bool IsUserAttendingConference(int userId, int conferenceId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceAttendees.Any(a => a.UserId == userId && a.ConferenceId == conferenceId)
            : _context.ConferenceAttendees.AsNoTracking().Any(a => a.UserId == userId && a.ConferenceId == conferenceId);
    }
} 