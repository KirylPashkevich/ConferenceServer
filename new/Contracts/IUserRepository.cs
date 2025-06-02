using ConferenceManager.Models;

namespace ConferenceManager.Contracts;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers(bool trackChanges);
    User? GetUser(int id, bool trackChanges);
    User? GetUserByEmail(string email, bool trackChanges);
    User? GetUserByUsername(string username, bool trackChanges);
    User CreateUser(User user);
    void DeleteUser(User user);
    void UpdateUser(User user);
    UserProfile? GetUserProfile(int userId, bool trackChanges);
    void UpdateUserProfile(UserProfile profile);
} 