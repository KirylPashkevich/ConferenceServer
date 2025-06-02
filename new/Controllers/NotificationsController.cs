using ConferenceManager.Contracts;
using ConferenceManager.Models;
using ConferenceManager.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly INotificationRepository _notificationRepository;

    public NotificationsController(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Notification>> GetNotifications()
    {
        var notifications = _notificationRepository.GetAllNotifications(false);
        return Ok(notifications);
    }

    [HttpGet("{id}")]
    public ActionResult<Notification> GetNotification(int id)
    {
        var notification = _notificationRepository.GetNotification(id, false);
        if (notification == null)
        {
            return NotFound();
        }
        return Ok(notification);
    }

    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<Notification>> GetUserNotifications(int userId)
    {
        var notifications = _notificationRepository.GetUserNotifications(userId, false);
        return Ok(notifications);
    }

    [HttpPost]
    public ActionResult<Notification> CreateNotification(NotificationDto notificationDto)
    {
        var notification = new Notification
        {
            UserId = notificationDto.UserId,
            Message = notificationDto.Message,
            Type = notificationDto.Type,
            Data = notificationDto.Data,
            IsRead = notificationDto.IsRead
        };

        var createdNotification = _notificationRepository.CreateNotification(notification);
        return CreatedAtAction(nameof(GetNotification), new { id = createdNotification.Id }, createdNotification);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateNotification(int id, NotificationUpdateDto notificationDto)
    {
        var existingNotification = _notificationRepository.GetNotification(id, true);
        if (existingNotification == null)
        {
            return NotFound();
        }

        // Обновляем только те поля, которые были предоставлены
        if (notificationDto.Message != null)
            existingNotification.Message = notificationDto.Message;
        
        if (notificationDto.Type != null)
            existingNotification.Type = notificationDto.Type;
        
        if (notificationDto.Data != null)
            existingNotification.Data = notificationDto.Data;
        
        if (notificationDto.IsRead != null)
            existingNotification.IsRead = notificationDto.IsRead.Value;

        _notificationRepository.UpdateNotification(existingNotification);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteNotification(int id)
    {
        var notification = _notificationRepository.GetNotification(id, false);
        if (notification == null)
        {
            return NotFound();
        }

        _notificationRepository.DeleteNotification(notification);
        return NoContent();
    }

    [HttpPut("{id}/read")]
    public IActionResult MarkNotificationAsRead(int id)
    {
        var notification = _notificationRepository.GetNotification(id, true);
        if (notification == null)
        {
            return NotFound();
        }

        _notificationRepository.MarkNotificationAsRead(id);
        return NoContent();
    }

    [HttpPut("user/{userId}/read-all")]
    public IActionResult MarkAllUserNotificationsAsRead(int userId)
    {
        _notificationRepository.MarkAllUserNotificationsAsRead(userId);
        return NoContent();
    }
} 