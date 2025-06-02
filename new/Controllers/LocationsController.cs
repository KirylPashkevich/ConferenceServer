using ConferenceManager.Contracts;
using ConferenceManager.Models;
using ConferenceManager.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;

    public LocationsController(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Location>> GetLocations()
    {
        var locations = _locationRepository.GetAllLocations(false);
        return Ok(locations);
    }

    [HttpGet("{id}")]
    public ActionResult<Location> GetLocation(int id)
    {
        var location = _locationRepository.GetLocation(id, false);
        if (location == null)
        {
            return NotFound();
        }
        return Ok(location);
    }

    [HttpPost]
    public ActionResult<Location> CreateLocation(LocationDto locationDto)
    {
        var location = new Location
        {
            Name = locationDto.Name,
            Address = locationDto.Address,
            City = locationDto.City,
            Country = locationDto.Country,
            Description = locationDto.Description
        };

        var createdLocation = _locationRepository.CreateLocation(location);
        return CreatedAtAction(nameof(GetLocation), new { id = createdLocation.Id }, createdLocation);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateLocation(int id, LocationUpdateDto locationDto)
    {
        var existingLocation = _locationRepository.GetLocation(id, true);
        if (existingLocation == null)
        {
            return NotFound();
        }

        // Обновляем только те поля, которые были предоставлены
        if (locationDto.Name != null)
            existingLocation.Name = locationDto.Name;
        
        if (locationDto.Address != null)
            existingLocation.Address = locationDto.Address;
        
        if (locationDto.City != null)
            existingLocation.City = locationDto.City;
        
        if (locationDto.Country != null)
            existingLocation.Country = locationDto.Country;
        
        if (locationDto.Description != null)
            existingLocation.Description = locationDto.Description;

        existingLocation.UpdatedAt = DateTime.UtcNow;

        _locationRepository.UpdateLocation(existingLocation);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteLocation(int id)
    {
        var location = _locationRepository.GetLocation(id, false);
        if (location == null)
        {
            return NotFound();
        }

        _locationRepository.DeleteLocation(location);
        return NoContent();
    }

    [HttpGet("{id}/reviews")]
    public ActionResult<IEnumerable<LocationReview>> GetLocationReviews(int id)
    {
        var reviews = _locationRepository.GetLocationReviews(id, false);
        return Ok(reviews);
    }

    [HttpGet("reviews/{id}")]
    public ActionResult<LocationReview> GetLocationReview(int id)
    {
        var review = _locationRepository.GetLocationReview(id, false);
        if (review == null)
        {
            return NotFound();
        }
        return Ok(review);
    }

    [HttpPost("{id}/reviews")]
    public ActionResult<LocationReview> CreateLocationReview(int id, LocationReview review)
    {
        if (id != review.LocationId)
        {
            return BadRequest();
        }

        var createdReview = _locationRepository.CreateLocationReview(review);
        return CreatedAtAction(nameof(GetLocationReview), new { id = createdReview.Id }, createdReview);
    }

    [HttpPut("reviews/{id}")]
    public IActionResult UpdateLocationReview(int id, LocationReview review)
    {
        if (id != review.Id)
        {
            return BadRequest();
        }

        var existingReview = _locationRepository.GetLocationReview(id, true);
        if (existingReview == null)
        {
            return NotFound();
        }

        _locationRepository.UpdateLocationReview(review);
        return NoContent();
    }

    [HttpDelete("reviews/{id}")]
    public IActionResult DeleteLocationReview(int id)
    {
        var review = _locationRepository.GetLocationReview(id, false);
        if (review == null)
        {
            return NotFound();
        }

        _locationRepository.DeleteLocationReview(review);
        return NoContent();
    }
} 