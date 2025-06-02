using ConferenceManager.Contracts;
using ConferenceManager.Data;
using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Repositories;

public class SponsorRepository : ISponsorRepository
{
    private readonly ApplicationDbContext _context;

    public SponsorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Sponsor> GetAllSponsors(bool trackChanges)
    {
        return trackChanges
            ? _context.Sponsors
                .Include(s => s.Conferences)
                .ToList()
            : _context.Sponsors
                .Include(s => s.Conferences)
                .AsNoTracking()
                .ToList();
    }

    public Sponsor? GetSponsor(int id, bool trackChanges)
    {
        return trackChanges
            ? _context.Sponsors
                .Include(s => s.Conferences)
                .FirstOrDefault(s => s.Id == id)
            : _context.Sponsors
                .Include(s => s.Conferences)
                .AsNoTracking()
                .FirstOrDefault(s => s.Id == id);
    }

    public Sponsor CreateSponsor(Sponsor sponsor)
    {
        _context.Sponsors.Add(sponsor);
        _context.SaveChanges();
        return sponsor;
    }

    public void DeleteSponsor(Sponsor sponsor)
    {
        _context.Sponsors.Remove(sponsor);
        _context.SaveChanges();
    }

    public void UpdateSponsor(Sponsor sponsor)
    {
        _context.Sponsors.Update(sponsor);
        _context.SaveChanges();
    }

    public IEnumerable<Sponsor> GetConferenceSponsors(int conferenceId, bool trackChanges)
    {
        var query = _context.Conferences
            .Where(c => c.Id == conferenceId)
            .SelectMany(c => c.Sponsors);

        return trackChanges
            ? query.ToList()
            : query.AsNoTracking().ToList();
    }

    public void AddSponsorToConference(int conferenceId, int sponsorId)
    {
        var conference = _context.Conferences
            .Include(c => c.Sponsors)
            .FirstOrDefault(c => c.Id == conferenceId);

        var sponsor = _context.Sponsors.Find(sponsorId);

        if (conference != null && sponsor != null)
        {
            conference.Sponsors.Add(sponsor);
            _context.SaveChanges();
        }
    }

    public void RemoveSponsorFromConference(int conferenceId, int sponsorId)
    {
        var conference = _context.Conferences
            .Include(c => c.Sponsors)
            .FirstOrDefault(c => c.Id == conferenceId);

        var sponsor = _context.Sponsors.Find(sponsorId);

        if (conference != null && sponsor != null)
        {
            conference.Sponsors.Remove(sponsor);
            _context.SaveChanges();
        }
    }
} 