using ConferenceManager.Contracts;
using ConferenceManager.Data;
using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Repositories;

public class ConferenceSubscriptionRepository : IConferenceSubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public ConferenceSubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ConferenceSubscription> GetAllSubscriptions(bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceSubscriptions
                .Include(cs => cs.Conference)
                .Include(cs => cs.User)
                .ToList()
            : _context.ConferenceSubscriptions
                .Include(cs => cs.Conference)
                .Include(cs => cs.User)
                .AsNoTracking()
                .ToList();
    }

    public IEnumerable<ConferenceSubscription> GetConferenceSubscriptions(int conferenceId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceSubscriptions
                .Include(cs => cs.User)
                .Where(cs => cs.ConferenceId == conferenceId)
                .ToList()
            : _context.ConferenceSubscriptions
                .Include(cs => cs.User)
                .Where(cs => cs.ConferenceId == conferenceId)
                .AsNoTracking()
                .ToList();
    }

    public IEnumerable<ConferenceSubscription> GetUserSubscriptions(int userId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceSubscriptions
                .Include(cs => cs.Conference)
                .Where(cs => cs.UserId == userId)
                .ToList()
            : _context.ConferenceSubscriptions
                .Include(cs => cs.Conference)
                .Where(cs => cs.UserId == userId)
                .AsNoTracking()
                .ToList();
    }

    public ConferenceSubscription? GetSubscription(int id, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceSubscriptions
                .Include(cs => cs.Conference)
                .Include(cs => cs.User)
                .FirstOrDefault(cs => cs.Id == id)
            : _context.ConferenceSubscriptions
                .Include(cs => cs.Conference)
                .Include(cs => cs.User)
                .AsNoTracking()
                .FirstOrDefault(cs => cs.Id == id);
    }

    public ConferenceSubscription? GetUserConferenceSubscription(int userId, int conferenceId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceSubscriptions
                .Include(cs => cs.Conference)
                .Include(cs => cs.User)
                .FirstOrDefault(cs => cs.UserId == userId && cs.ConferenceId == conferenceId)
            : _context.ConferenceSubscriptions
                .Include(cs => cs.Conference)
                .Include(cs => cs.User)
                .AsNoTracking()
                .FirstOrDefault(cs => cs.UserId == userId && cs.ConferenceId == conferenceId);
    }

    public ConferenceSubscription CreateSubscription(ConferenceSubscription subscription)
    {
        _context.ConferenceSubscriptions.Add(subscription);
        _context.SaveChanges();
        return subscription;
    }

    public void DeleteSubscription(ConferenceSubscription subscription)
    {
        _context.ConferenceSubscriptions.Remove(subscription);
        _context.SaveChanges();
    }

    public bool IsUserSubscribedToConference(int userId, int conferenceId, bool trackChanges)
    {
        return trackChanges
            ? _context.ConferenceSubscriptions
                .Any(cs => cs.UserId == userId && cs.ConferenceId == conferenceId)
            : _context.ConferenceSubscriptions
                .AsNoTracking()
                .Any(cs => cs.UserId == userId && cs.ConferenceId == conferenceId);
    }
} 