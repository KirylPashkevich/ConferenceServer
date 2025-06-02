using ConferenceManager.Contracts;
using ConferenceManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;

    public RolesController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Role>> GetRoles()
    {
        var roles = _roleRepository.GetAllRoles(false);
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public ActionResult<Role> GetRole(int id)
    {
        var role = _roleRepository.GetRole(id, false);
        if (role == null)
        {
            return NotFound();
        }
        return Ok(role);
    }

    [HttpGet("name/{name}")]
    public ActionResult<Role> GetRoleByName(string name)
    {
        var role = _roleRepository.GetRoleByName(name, false);
        if (role == null)
        {
            return NotFound();
        }
        return Ok(role);
    }

    [HttpPost]
    public ActionResult<Role> CreateRole(Role role)
    {
        var createdRole = _roleRepository.CreateRole(role);
        return CreatedAtAction(nameof(GetRole), new { id = createdRole.Id }, createdRole);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRole(int id, Role role)
    {
        if (id != role.Id)
        {
            return BadRequest();
        }

        var existingRole = _roleRepository.GetRole(id, true);
        if (existingRole == null)
        {
            return NotFound();
        }

        _roleRepository.UpdateRole(role);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRole(int id)
    {
        var role = _roleRepository.GetRole(id, false);
        if (role == null)
        {
            return NotFound();
        }

        _roleRepository.DeleteRole(role);
        return NoContent();
    }

    [HttpGet("{id}/users")]
    public ActionResult<IEnumerable<User>> GetRoleUsers(int id)
    {
        var users = _roleRepository.GetRoleUsers(id, false);
        return Ok(users);
    }
} 