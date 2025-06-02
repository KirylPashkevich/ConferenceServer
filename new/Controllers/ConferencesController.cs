using ConferenceManager.Contracts;
using ConferenceManager.Models;
using ConferenceManager.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConferencesController : ControllerBase
{
    private readonly IConferenceRepository _conferenceRepository;

    public ConferencesController(IConferenceRepository conferenceRepository)
    {
        _conferenceRepository = conferenceRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Conference>> GetConferences()
    {
        var conferences = _conferenceRepository.GetAllConferences(false);
        return Ok(conferences);
    }

    [HttpGet("{id}")]
    public ActionResult<Conference> GetConference(int id)
    {
        var conference = _conferenceRepository.GetConference(id, false);
        if (conference == null)
        {
            return NotFound();
        }
        return Ok(conference);
    }

    [HttpPost]
    public ActionResult<Conference> CreateConference(ConferenceDto conferenceDto)
    {
        var conference = new Conference
        {
            Title = conferenceDto.Title,
            Description = conferenceDto.Description,
            LocationId = conferenceDto.LocationId,
            StartDate = conferenceDto.StartDate,
            EndDate = conferenceDto.EndDate,
            OrganizerId = conferenceDto.OrganizerId,
            IsAnnounced = conferenceDto.IsAnnounced
        };

        var createdConference = _conferenceRepository.CreateConference(conference);
        return CreatedAtAction(nameof(GetConference), new { id = createdConference.Id }, createdConference);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateConference(int id, ConferenceUpdateDto conferenceDto)
    {
        var existingConference = _conferenceRepository.GetConference(id, true);
        if (existingConference == null)
        {
            return NotFound();
        }

        // Обновляем только те поля, которые были предоставлены
        if (conferenceDto.Title != null)
            existingConference.Title = conferenceDto.Title;
        
        if (conferenceDto.Description != null)
            existingConference.Description = conferenceDto.Description;
        
        if (conferenceDto.LocationId != null)
            existingConference.LocationId = conferenceDto.LocationId.Value;
        
        if (conferenceDto.StartDate != null)
            existingConference.StartDate = conferenceDto.StartDate.Value;
        
        if (conferenceDto.EndDate != null)
            existingConference.EndDate = conferenceDto.EndDate.Value;
        
        if (conferenceDto.OrganizerId != null)
            existingConference.OrganizerId = conferenceDto.OrganizerId.Value;
        
        if (conferenceDto.IsAnnounced != null)
            existingConference.IsAnnounced = conferenceDto.IsAnnounced.Value;

        existingConference.UpdatedAt = DateTime.UtcNow;

        _conferenceRepository.UpdateConference(existingConference);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteConference(int id)
    {
        var conference = _conferenceRepository.GetConference(id, false);
        if (conference == null)
        {
            return NotFound();
        }

        _conferenceRepository.DeleteConference(conference);
        return NoContent();
    }
} 