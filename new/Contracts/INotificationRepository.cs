using ConferenceManager.Models;

namespace ConferenceManager.Contracts;

public interface INotificationRepository
{
    IEnumerable<Notification> GetAllNotifications(bool trackChanges);
    Notification? GetNotification(int id, bool trackChanges);
    IEnumerable<Notification> GetUserNotifications(int userId, bool trackChanges);
    Notification CreateNotification(Notification notification);
    void DeleteNotification(Notification notification);
    void UpdateNotification(Notification notification);
    void MarkNotificationAsRead(int notificationId);
    void MarkAllUserNotificationsAsRead(int userId);
} 