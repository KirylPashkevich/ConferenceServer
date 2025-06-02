using ConferenceManager.Contracts;
using ConferenceManager.Data;
using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _context;

    public LocationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Location> GetAllLocations(bool trackChanges)
    {
        return trackChanges
            ? _context.Locations
                .Include(l => l.Reviews)
                .ToList()
            : _context.Locations
                .Include(l => l.Reviews)
                .AsNoTracking()
                .ToList();
    }

    public Location? GetLocation(int id, bool trackChanges)
    {
        return trackChanges
            ? _context.Locations
                .Include(l => l.Reviews)
                .FirstOrDefault(l => l.Id == id)
            : _context.Locations
                .Include(l => l.Reviews)
                .AsNoTracking()
                .FirstOrDefault(l => l.Id == id);
    }

    public Location CreateLocation(Location location)
    {
        _context.Locations.Add(location);
        _context.SaveChanges();
        return location;
    }

    public void DeleteLocation(Location location)
    {
        _context.Locations.Remove(location);
        _context.SaveChanges();
    }

    public void UpdateLocation(Location location)
    {
        _context.Locations.Update(location);
        _context.SaveChanges();
    }

    public IEnumerable<LocationReview> GetLocationReviews(int locationId, bool trackChanges)
    {
        return trackChanges
            ? _context.LocationReviews
                .Include(r => r.User)
                .Where(r => r.LocationId == locationId)
                .ToList()
            : _context.LocationReviews
                .Include(r => r.User)
                .Where(r => r.LocationId == locationId)
                .AsNoTracking()
                .ToList();
    }

    public LocationReview? GetLocationReview(int id, bool trackChanges)
    {
        return trackChanges
            ? _context.LocationReviews
                .Include(r => r.User)
                .FirstOrDefault(r => r.Id == id)
            : _context.LocationReviews
                .Include(r => r.User)
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
    }

    public LocationReview CreateLocationReview(LocationReview review)
    {
        _context.LocationReviews.Add(review);
        _context.SaveChanges();
        return review;
    }

    public void DeleteLocationReview(LocationReview review)
    {
        _context.LocationReviews.Remove(review);
        _context.SaveChanges();
    }

    public void UpdateLocationReview(LocationReview review)
    {
        _context.LocationReviews.Update(review);
        _context.SaveChanges();
    }
} 