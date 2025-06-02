using ConferenceManager.Contracts;
using ConferenceManager.Data;
using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly ApplicationDbContext _context;

    public NotificationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Notification> GetAllNotifications(bool trackChanges)
    {
        return trackChanges
            ? _context.Notifications
                .Include(n => n.User)
                .ToList()
            : _context.Notifications
                .Include(n => n.User)
                .AsNoTracking()
                .ToList();
    }

    public Notification? GetNotification(int id, bool trackChanges)
    {
        return trackChanges
            ? _context.Notifications
                .Include(n => n.User)
                .FirstOrDefault(n => n.Id == id)
            : _context.Notifications
                .Include(n => n.User)
                .AsNoTracking()
                .FirstOrDefault(n => n.Id == id);
    }

    public IEnumerable<Notification> GetUserNotifications(int userId, bool trackChanges)
    {
        return trackChanges
            ? _context.Notifications
                .Where(n => n.UserId == userId)
                .ToList()
            : _context.Notifications
                .Where(n => n.UserId == userId)
                .AsNoTracking()
                .ToList();
    }

    public Notification CreateNotification(Notification notification)
    {
        _context.Notifications.Add(notification);
        _context.SaveChanges();
        return notification;
    }

    public void DeleteNotification(Notification notification)
    {
        _context.Notifications.Remove(notification);
        _context.SaveChanges();
    }

    public void UpdateNotification(Notification notification)
    {
        _context.Notifications.Update(notification);
        _context.SaveChanges();
    }

    public void MarkNotificationAsRead(int notificationId)
    {
        var notification = _context.Notifications.Find(notificationId);
        if (notification != null)
        {
            notification.IsRead = true;
            _context.SaveChanges();
        }
    }

    public void MarkAllUserNotificationsAsRead(int userId)
    {
        var notifications = _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToList();

        foreach (var notification in notifications)
        {
            notification.IsRead = true;
        }

        _context.SaveChanges();
    }
} 