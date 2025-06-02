using ConferenceManager.Contracts;
using ConferenceManager.Models;
using ConferenceManager.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConferenceOrganizersController : ControllerBase
{
    private readonly IConferenceOrganizerRepository _organizerRepository;

    public ConferenceOrganizersController(IConferenceOrganizerRepository organizerRepository)
    {
        _organizerRepository = organizerRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ConferenceOrganizer>> GetOrganizers()
    {
        var organizers = _organizerRepository.GetAllConferenceOrganizers(false);
        return Ok(organizers);
    }

    [HttpGet("conference/{conferenceId}")]
    public ActionResult<IEnumerable<ConferenceOrganizer>> GetConferenceOrganizers(int conferenceId)
    {
        var organizers = _organizerRepository.GetConferenceOrganizers(conferenceId, false);
        return Ok(organizers);
    }

    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<ConferenceOrganizer>> GetUserOrganizations(int userId)
    {
        var organizations = _organizerRepository.GetUserOrganizations(userId, false);
        return Ok(organizations);
    }

    [HttpGet("conference/{conferenceId}/user/{userId}")]
    public ActionResult<ConferenceOrganizer> GetConferenceOrganizer(int conferenceId, int userId)
    {
        var organizer = _organizerRepository.GetConferenceOrganizer(conferenceId, userId, false);
        if (organizer == null)
        {
            return NotFound();
        }
        return Ok(organizer);
    }

    [HttpPost]
    public ActionResult<ConferenceOrganizer> CreateOrganizer(ConferenceOrganizerDto organizerDto)
    {
        if (_organizerRepository.IsUserOrganizingConference(organizerDto.UserId, organizerDto.ConferenceId, false))
        {
            return BadRequest("This user is already an organizer of this conference");
        }

        var organizer = new ConferenceOrganizer
        {
            ConferenceId = organizerDto.ConferenceId,
            UserId = organizerDto.UserId
        };

        var createdOrganizer = _organizerRepository.CreateConferenceOrganizer(organizer);
        return CreatedAtAction(
            nameof(GetConferenceOrganizer),
            new { conferenceId = createdOrganizer.ConferenceId, userId = createdOrganizer.UserId },
            createdOrganizer);
    }

    [HttpDelete("conference/{conferenceId}/user/{userId}")]
    public IActionResult DeleteOrganizer(int conferenceId, int userId)
    {
        var organizer = _organizerRepository.GetConferenceOrganizer(conferenceId, userId, false);
        if (organizer == null)
        {
            return NotFound();
        }

        _organizerRepository.DeleteConferenceOrganizer(organizer);
        return NoContent();
    }

    [HttpGet("conference/{conferenceId}/user/{userId}/check")]
    public ActionResult<bool> IsUserOrganizingConference(int conferenceId, int userId)
    {
        var isOrganizing = _organizerRepository.IsUserOrganizingConference(userId, conferenceId, false);
        return Ok(isOrganizing);
    }
} 