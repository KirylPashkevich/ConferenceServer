using ConferenceManager.Contracts;
using ConferenceManager.Data;
using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAllUsers(bool trackChanges)
    {
        return trackChanges
            ? _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProfile)
                .ToList()
            : _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProfile)
                .AsNoTracking()
                .ToList();
    }

    public User? GetUser(int id, bool trackChanges)
    {
        return trackChanges
            ? _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProfile)
                .FirstOrDefault(u => u.Id == id)
            : _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProfile)
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == id);
    }

    public User? GetUserByEmail(string email, bool trackChanges)
    {
        return trackChanges
            ? _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProfile)
                .FirstOrDefault(u => u.Email == email)
            : _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProfile)
                .AsNoTracking()
                .FirstOrDefault(u => u.Email == email);
    }

    public User? GetUserByUsername(string username, bool trackChanges)
    {
        return trackChanges
            ? _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProfile)
                .FirstOrDefault(u => u.Username == username)
            : _context.Users
                .Include(u => u.Role)
                .Include(u => u.UserProfile)
                .AsNoTracking()
                .FirstOrDefault(u => u.Username == username);
    }

    public User CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public UserProfile? GetUserProfile(int userId, bool trackChanges)
    {
        return trackChanges
            ? _context.UserProfiles
                .Include(p => p.User)
                .FirstOrDefault(p => p.UserId == userId)
            : _context.UserProfiles
                .Include(p => p.User)
                .AsNoTracking()
                .FirstOrDefault(p => p.UserId == userId);
    }

    public void UpdateUserProfile(UserProfile profile)
    {
        _context.UserProfiles.Update(profile);
        _context.SaveChanges();
    }
} 