using ConferenceManager.Contracts;
using ConferenceManager.Models;
using ConferenceManager.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SponsorsController : ControllerBase
{
    private readonly ISponsorRepository _sponsorRepository;
    private readonly IConferenceRepository _conferenceRepository;

    public SponsorsController(
        ISponsorRepository sponsorRepository,
        IConferenceRepository conferenceRepository)
    {
        _sponsorRepository = sponsorRepository;
        _conferenceRepository = conferenceRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Sponsor>> GetSponsors()
    {
        var sponsors = _sponsorRepository.GetAllSponsors(false);
        return Ok(sponsors);
    }

    [HttpGet("{id}")]
    public ActionResult<Sponsor> GetSponsor(int id)
    {
        var sponsor = _sponsorRepository.GetSponsor(id, false);
        if (sponsor == null)
        {
            return NotFound();
        }
        return Ok(sponsor);
    }

    [HttpGet("conference/{conferenceId}")]
    public ActionResult<IEnumerable<Sponsor>> GetConferenceSponsors(int conferenceId)
    {
        var sponsors = _sponsorRepository.GetConferenceSponsors(conferenceId, false);
        return Ok(sponsors);
    }

    [HttpPost]
    public ActionResult<Sponsor> CreateSponsor(SponsorDto sponsorDto)
    {
        var sponsor = new Sponsor
        {
            Name = sponsorDto.Name,
            Description = sponsorDto.Description,
            Website = sponsorDto.Website,
            LogoUrl = sponsorDto.LogoUrl,
            ContactPerson = sponsorDto.ContactPerson,
            ContactEmail = sponsorDto.ContactEmail,
            ContactPhone = sponsorDto.ContactPhone
        };

        var createdSponsor = _sponsorRepository.CreateSponsor(sponsor);
        return CreatedAtAction(nameof(GetSponsor), new { id = createdSponsor.Id }, createdSponsor);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSponsor(int id, SponsorUpdateDto sponsorDto)
    {
        var existingSponsor = _sponsorRepository.GetSponsor(id, true);
        if (existingSponsor == null)
        {
            return NotFound();
        }

        // Обновляем только те поля, которые были предоставлены
        if (sponsorDto.Name != null)
            existingSponsor.Name = sponsorDto.Name;
        
        if (sponsorDto.Description != null)
            existingSponsor.Description = sponsorDto.Description;
        
        if (sponsorDto.Website != null)
            existingSponsor.Website = sponsorDto.Website;
        
        if (sponsorDto.LogoUrl != null)
            existingSponsor.LogoUrl = sponsorDto.LogoUrl;
        
        if (sponsorDto.ContactPerson != null)
            existingSponsor.ContactPerson = sponsorDto.ContactPerson;
        
        if (sponsorDto.ContactEmail != null)
            existingSponsor.ContactEmail = sponsorDto.ContactEmail;
        
        if (sponsorDto.ContactPhone != null)
            existingSponsor.ContactPhone = sponsorDto.ContactPhone;

        _sponsorRepository.UpdateSponsor(existingSponsor);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSponsor(int id)
    {
        var sponsor = _sponsorRepository.GetSponsor(id, false);
        if (sponsor == null)
        {
            return NotFound();
        }

        _sponsorRepository.DeleteSponsor(sponsor);
        return NoContent();
    }

    [HttpPost("conference/{conferenceId}/sponsor/{sponsorId}")]
    public IActionResult AddSponsorToConference(int conferenceId, int sponsorId)
    {
        var conference = _conferenceRepository.GetConference(conferenceId, false);
        if (conference == null)
        {
            return NotFound("Конференция не найдена");
        }

        var sponsor = _sponsorRepository.GetSponsor(sponsorId, false);
        if (sponsor == null)
        {
            return NotFound("Спонсор не найден");
        }

        _sponsorRepository.AddSponsorToConference(conferenceId, sponsorId);
        return NoContent();
    }

    [HttpDelete("conference/{conferenceId}/sponsor/{sponsorId}")]
    public IActionResult RemoveSponsorFromConference(int conferenceId, int sponsorId)
    {
        var conference = _conferenceRepository.GetConference(conferenceId, false);
        if (conference == null)
        {
            return NotFound("Конференция не найдена");
        }

        var sponsor = _sponsorRepository.GetSponsor(sponsorId, false);
        if (sponsor == null)
        {
            return NotFound("Спонсор не найден");
        }

        _sponsorRepository.RemoveSponsorFromConference(conferenceId, sponsorId);
        return NoContent();
    }
} 