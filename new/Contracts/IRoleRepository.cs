using ConferenceManager.Models;

namespace ConferenceManager.Contracts;

public interface IRoleRepository
{
    IEnumerable<Role> GetAllRoles(bool trackChanges);
    Role? GetRole(int id, bool trackChanges);
    Role? GetRoleByName(string name, bool trackChanges);
    Role CreateRole(Role role);
    void DeleteRole(Role role);
    void UpdateRole(Role role);
    IEnumerable<User> GetRoleUsers(int roleId, bool trackChanges);
} 