using ConferenceManager.Contracts;
using ConferenceManager.Models;
using ConferenceManager.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PresentationsController : ControllerBase
{
    private readonly IPresentationRepository _presentationRepository;
    private readonly IConferenceRepository _conferenceRepository;
    private readonly IUserRepository _userRepository;

    public PresentationsController(
        IPresentationRepository presentationRepository,
        IConferenceRepository conferenceRepository,
        IUserRepository userRepository)
    {
        _presentationRepository = presentationRepository;
        _conferenceRepository = conferenceRepository;
        _userRepository = userRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Presentation>> GetPresentations()
    {
        var presentations = _presentationRepository.GetAllPresentations(false);
        return Ok(presentations);
    }

    [HttpGet("{id}")]
    public ActionResult<Presentation> GetPresentation(int id)
    {
        var presentation = _presentationRepository.GetPresentation(id, false);
        if (presentation == null)
        {
            return NotFound();
        }
        return Ok(presentation);
    }

    [HttpGet("conference/{conferenceId}")]
    public ActionResult<IEnumerable<Presentation>> GetConferencePresentations(int conferenceId)
    {
        var presentations = _presentationRepository.GetConferencePresentations(conferenceId, false);
        return Ok(presentations);
    }

    [HttpPost]
    public ActionResult<Presentation> CreatePresentation(PresentationDto presentationDto)
    {
        // Проверяем существование конференции
        var conference = _conferenceRepository.GetConference(presentationDto.ConferenceId, false);
        if (conference == null)
        {
            return BadRequest("Указанная конференция не существует");
        }

        // Проверяем, что время презентации находится в рамках конференции
        if (presentationDto.StartTime < conference.StartDate || presentationDto.EndTime > conference.EndDate)
        {
            return BadRequest("Время презентации должно находиться в рамках конференции");
        }

        var presentation = new Presentation
        {
            ConferenceId = presentationDto.ConferenceId,
            Title = presentationDto.Title,
            Description = presentationDto.Description,
            StartTime = presentationDto.StartTime,
            EndTime = presentationDto.EndTime,
            Room = presentationDto.Room
        };

        var createdPresentation = _presentationRepository.CreatePresentation(presentation);

        // Добавляем спикеров, если они указаны
        if (presentationDto.SpeakerIds != null && presentationDto.SpeakerIds.Any())
        {
            foreach (var speakerId in presentationDto.SpeakerIds)
            {
                var user = _userRepository.GetUser(speakerId, false);
                if (user != null)
                {
                    _presentationRepository.AddSpeakerToPresentation(createdPresentation.Id, speakerId);
                }
            }
        }

        return CreatedAtAction(nameof(GetPresentation), new { id = createdPresentation.Id }, createdPresentation);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePresentation(int id, PresentationUpdateDto presentationDto)
    {
        var existingPresentation = _presentationRepository.GetPresentation(id, true);
        if (existingPresentation == null)
        {
            return NotFound();
        }

        // Если обновляется время, проверяем что оно находится в рамках конференции
        if (presentationDto.StartTime.HasValue || presentationDto.EndTime.HasValue)
        {
            var conference = _conferenceRepository.GetConference(existingPresentation.ConferenceId, false);
            if (conference == null)
            {
                return BadRequest("Конференция не найдена");
            }

            var startTime = presentationDto.StartTime ?? existingPresentation.StartTime;
            var endTime = presentationDto.EndTime ?? existingPresentation.EndTime;

            if (startTime < conference.StartDate || endTime > conference.EndDate)
            {
                return BadRequest("Время презентации должно находиться в рамках конференции");
            }
        }

        // Обновляем только те поля, которые были предоставлены
        if (presentationDto.Title != null)
            existingPresentation.Title = presentationDto.Title;
        
        if (presentationDto.Description != null)
            existingPresentation.Description = presentationDto.Description;
        
        if (presentationDto.StartTime.HasValue)
            existingPresentation.StartTime = presentationDto.StartTime.Value;
        
        if (presentationDto.EndTime.HasValue)
            existingPresentation.EndTime = presentationDto.EndTime.Value;
        
        if (presentationDto.Room != null)
            existingPresentation.Room = presentationDto.Room;

        _presentationRepository.UpdatePresentation(existingPresentation);

        // Обновляем спикеров, если они указаны
        if (presentationDto.SpeakerIds != null)
        {
            // Получаем текущих спикеров
            var currentSpeakers = _presentationRepository.GetPresentationSpeakers(id, false)
                .Select(s => s.Id)
                .ToList();

            // Удаляем спикеров, которых нет в новом списке
            foreach (var speakerId in currentSpeakers)
            {
                if (!presentationDto.SpeakerIds.Contains(speakerId))
                {
                    _presentationRepository.RemoveSpeakerFromPresentation(id, speakerId);
                }
            }

            // Добавляем новых спикеров
            foreach (var speakerId in presentationDto.SpeakerIds)
            {
                if (!currentSpeakers.Contains(speakerId))
                {
                    var user = _userRepository.GetUser(speakerId, false);
                    if (user != null)
                    {
                        _presentationRepository.AddSpeakerToPresentation(id, speakerId);
                    }
                }
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePresentation(int id)
    {
        var presentation = _presentationRepository.GetPresentation(id, false);
        if (presentation == null)
        {
            return NotFound();
        }

        _presentationRepository.DeletePresentation(presentation);
        return NoContent();
    }

    [HttpGet("{id}/speakers")]
    public ActionResult<IEnumerable<User>> GetPresentationSpeakers(int id)
    {
        var speakers = _presentationRepository.GetPresentationSpeakers(id, false);
        return Ok(speakers);
    }
} 