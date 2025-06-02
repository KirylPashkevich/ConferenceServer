using ConferenceManager.Contracts;
using ConferenceManager.Data;
using ConferenceManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceManager.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Role> GetAllRoles(bool trackChanges)
    {
        return trackChanges
            ? _context.Roles
                .Include(r => r.Users)
                .ToList()
            : _context.Roles
                .Include(r => r.Users)
                .AsNoTracking()
                .ToList();
    }

    public Role? GetRole(int id, bool trackChanges)
    {
        return trackChanges
            ? _context.Roles
                .Include(r => r.Users)
                .FirstOrDefault(r => r.Id == id)
            : _context.Roles
                .Include(r => r.Users)
                .AsNoTracking()
                .FirstOrDefault(r => r.Id == id);
    }

    public Role? GetRoleByName(string name, bool trackChanges)
    {
        return trackChanges
            ? _context.Roles
                .Include(r => r.Users)
                .FirstOrDefault(r => r.Name == name)
            : _context.Roles
                .Include(r => r.Users)
                .AsNoTracking()
                .FirstOrDefault(r => r.Name == name);
    }

    public Role CreateRole(Role role)
    {
        _context.Roles.Add(role);
        _context.SaveChanges();
        return role;
    }

    public void DeleteRole(Role role)
    {
        _context.Roles.Remove(role);
        _context.SaveChanges();
    }

    public void UpdateRole(Role role)
    {
        _context.Roles.Update(role);
        _context.SaveChanges();
    }

    public IEnumerable<User> GetRoleUsers(int roleId, bool trackChanges)
    {
        return trackChanges
            ? _context.Users
                .Include(u => u.UserProfile)
                .Where(u => u.RoleId == roleId)
                .ToList()
            : _context.Users
                .Include(u => u.UserProfile)
                .Where(u => u.RoleId == roleId)
                .AsNoTracking()
                .ToList();
    }
} 