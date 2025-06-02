using ConferenceManager.Contracts;
using ConferenceManager.Models;
using ConferenceManager.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConferenceAttendeesController : ControllerBase
{
    private readonly IConferenceAttendeeRepository _attendeeRepository;

    public ConferenceAttendeesController(IConferenceAttendeeRepository attendeeRepository)
    {
        _attendeeRepository = attendeeRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ConferenceAttendee>> GetAttendees()
    {
        var attendees = _attendeeRepository.GetAllConferenceAttendees(false);
        return Ok(attendees);
    }

    [HttpGet("conference/{conferenceId}")]
    public ActionResult<IEnumerable<ConferenceAttendee>> GetConferenceAttendees(int conferenceId)
    {
        var attendees = _attendeeRepository.GetConferenceAttendees(conferenceId, false);
        return Ok(attendees);
    }

    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<ConferenceAttendee>> GetUserAttendances(int userId)
    {
        var attendances = _attendeeRepository.GetUserAttendances(userId, false);
        return Ok(attendances);
    }

    [HttpGet("conference/{conferenceId}/user/{userId}")]
    public ActionResult<ConferenceAttendee> GetConferenceAttendee(int conferenceId, int userId)
    {
        var attendee = _attendeeRepository.GetConferenceAttendee(conferenceId, userId, false);
        if (attendee == null)
        {
            return NotFound();
        }
        return Ok(attendee);
    }

    [HttpPost]
    public ActionResult<ConferenceAttendee> CreateAttendee(ConferenceAttendeeDto attendeeDto)
    {
        var attendee = new ConferenceAttendee
        {
            ConferenceId = attendeeDto.ConferenceId,
            UserId = attendeeDto.UserId
        };

        var createdAttendee = _attendeeRepository.CreateConferenceAttendee(attendee);
        return CreatedAtAction(
            nameof(GetConferenceAttendee),
            new { conferenceId = createdAttendee.ConferenceId, userId = createdAttendee.UserId },
            createdAttendee);
    }

    [HttpDelete("conference/{conferenceId}/user/{userId}")]
    public IActionResult DeleteAttendee(int conferenceId, int userId)
    {
        var attendee = _attendeeRepository.GetConferenceAttendee(conferenceId, userId, false);
        if (attendee == null)
        {
            return NotFound();
        }

        _attendeeRepository.DeleteConferenceAttendee(attendee);
        return NoContent();
    }

    [HttpGet("conference/{conferenceId}/user/{userId}/check")]
    public ActionResult<bool> IsUserAttendingConference(int conferenceId, int userId)
    {
        var isAttending = _attendeeRepository.IsUserAttendingConference(userId, conferenceId, false);
        return Ok(isAttending);
    }
} 