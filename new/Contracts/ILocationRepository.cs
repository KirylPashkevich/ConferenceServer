using ConferenceManager.Models;

namespace ConferenceManager.Contracts;

public interface ILocationRepository
{
    IEnumerable<Location> GetAllLocations(bool trackChanges);
    Location? GetLocation(int id, bool trackChanges);
    Location CreateLocation(Location location);
    void DeleteLocation(Location location);
    void UpdateLocation(Location location);
    IEnumerable<LocationReview> GetLocationReviews(int locationId, bool trackChanges);
    LocationReview? GetLocationReview(int id, bool trackChanges);
    LocationReview CreateLocationReview(LocationReview review);
    void DeleteLocationReview(LocationReview review);
    void UpdateLocationReview(LocationReview review);
} 