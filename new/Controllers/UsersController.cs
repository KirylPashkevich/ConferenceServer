using ConferenceManager.Contracts;
using ConferenceManager.Models;
using ConferenceManager.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UsersController(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserResponseDto>> GetUsers()
    {
        var users = _userRepository.GetAllUsers(false);
        var response = users.Select(u => new UserResponseDto
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            RoleId = u.RoleId,
            RoleName = u.Role?.Name,
            FirstName = u.UserProfile?.FirstName,
            LastName = u.UserProfile?.LastName,
            Bio = u.UserProfile?.Bio,
            Affiliation = u.UserProfile?.Affiliation,
            Website = u.UserProfile?.Website,
            PhoneNumber = u.UserProfile?.PhoneNumber,
            ProfilePicture = u.UserProfile?.ProfilePicture
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<UserResponseDto> GetUser(int id)
    {
        var user = _userRepository.GetUser(id, false);
        if (user == null)
        {
            return NotFound();
        }

        var response = new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            RoleId = user.RoleId,
            RoleName = user.Role?.Name,
            FirstName = user.UserProfile?.FirstName,
            LastName = user.UserProfile?.LastName,
            Bio = user.UserProfile?.Bio,
            Affiliation = user.UserProfile?.Affiliation,
            Website = user.UserProfile?.Website,
            PhoneNumber = user.UserProfile?.PhoneNumber,
            ProfilePicture = user.UserProfile?.ProfilePicture
        };
        return Ok(response);
    }

    [HttpGet("email/{email}")]
    public ActionResult<UserResponseDto> GetUserByEmail(string email)
    {
        var user = _userRepository.GetUserByEmail(email, false);
        if (user == null)
        {
            return NotFound();
        }

        var response = new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            RoleId = user.RoleId,
            RoleName = user.Role?.Name,
            FirstName = user.UserProfile?.FirstName,
            LastName = user.UserProfile?.LastName,
            Bio = user.UserProfile?.Bio,
            Affiliation = user.UserProfile?.Affiliation,
            Website = user.UserProfile?.Website,
            PhoneNumber = user.UserProfile?.PhoneNumber,
            ProfilePicture = user.UserProfile?.ProfilePicture
        };
        return Ok(response);
    }

    [HttpGet("username/{username}")]
    public ActionResult<UserResponseDto> GetUserByUsername(string username)
    {
        var user = _userRepository.GetUserByUsername(username, false);
        if (user == null)
        {
            return NotFound();
        }

        var response = new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            RoleId = user.RoleId,
            RoleName = user.Role?.Name,
            FirstName = user.UserProfile?.FirstName,
            LastName = user.UserProfile?.LastName,
            Bio = user.UserProfile?.Bio,
            Affiliation = user.UserProfile?.Affiliation,
            Website = user.UserProfile?.Website,
            PhoneNumber = user.UserProfile?.PhoneNumber,
            ProfilePicture = user.UserProfile?.ProfilePicture
        };
        return Ok(response);
    }

    [HttpPost]
    public ActionResult<UserResponseDto> CreateUser(UserCreateDto userDto)
    {
        if (userDto.RoleId.HasValue)
        {
            var role = _roleRepository.GetRole(userDto.RoleId.Value, false);
            if (role == null)
            {
                return BadRequest("Invalid role ID");
            }
        }

        var user = new User
        {
            Username = userDto.Username,
            Password = userDto.Password, // В реальном приложении здесь должно быть хеширование
            Email = userDto.Email,
            RoleId = userDto.RoleId,
            UserProfile = new UserProfile
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Bio = userDto.Bio,
                Affiliation = userDto.Affiliation,
                Website = userDto.Website,
                PhoneNumber = userDto.PhoneNumber,
                ProfilePicture = userDto.ProfilePicture
            }
        };

        var createdUser = _userRepository.CreateUser(user);
        var response = new UserResponseDto
        {
            Id = createdUser.Id,
            Username = createdUser.Username,
            Email = createdUser.Email,
            RoleId = createdUser.RoleId,
            RoleName = createdUser.Role?.Name,
            FirstName = createdUser.UserProfile?.FirstName,
            LastName = createdUser.UserProfile?.LastName,
            Bio = createdUser.UserProfile?.Bio,
            Affiliation = createdUser.UserProfile?.Affiliation,
            Website = createdUser.UserProfile?.Website,
            PhoneNumber = createdUser.UserProfile?.PhoneNumber,
            ProfilePicture = createdUser.UserProfile?.ProfilePicture
        };
        return CreatedAtAction(nameof(GetUser), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, UserUpdateDto userDto)
    {
        var existingUser = _userRepository.GetUser(id, true);
        if (existingUser == null)
        {
            return NotFound();
        }

        if (userDto.RoleId.HasValue)
        {
            var role = _roleRepository.GetRole(userDto.RoleId.Value, false);
            if (role == null)
            {
                return BadRequest("Invalid role ID");
            }
            existingUser.RoleId = userDto.RoleId.Value;
        }

        // Обновляем только те поля, которые были предоставлены
        if (userDto.Username != null)
            existingUser.Username = userDto.Username;
        
        if (userDto.Password != null)
            existingUser.Password = userDto.Password; // В реальном приложении здесь должно быть хеширование
        
        if (userDto.Email != null)
            existingUser.Email = userDto.Email;

        // Обновляем профиль пользователя
        if (existingUser.UserProfile == null)
        {
            existingUser.UserProfile = new UserProfile();
        }

        if (userDto.FirstName != null)
            existingUser.UserProfile.FirstName = userDto.FirstName;
        
        if (userDto.LastName != null)
            existingUser.UserProfile.LastName = userDto.LastName;
        
        if (userDto.Bio != null)
            existingUser.UserProfile.Bio = userDto.Bio;
        
        if (userDto.Affiliation != null)
            existingUser.UserProfile.Affiliation = userDto.Affiliation;
        
        if (userDto.Website != null)
            existingUser.UserProfile.Website = userDto.Website;
        
        if (userDto.PhoneNumber != null)
            existingUser.UserProfile.PhoneNumber = userDto.PhoneNumber;
        
        if (userDto.ProfilePicture != null)
            existingUser.UserProfile.ProfilePicture = userDto.ProfilePicture;

        _userRepository.UpdateUser(existingUser);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _userRepository.GetUser(id, false);
        if (user == null)
        {
            return NotFound();
        }

        _userRepository.DeleteUser(user);
        return NoContent();
    }

    [HttpGet("{id}/profile")]
    public ActionResult<UserProfileDto> GetUserProfile(int id)
    {
        var profile = _userRepository.GetUserProfile(id, false);
        if (profile == null)
        {
            return NotFound();
        }

        var response = new UserProfileDto
        {
            UserId = profile.UserId,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Bio = profile.Bio,
            Affiliation = profile.Affiliation,
            Website = profile.Website,
            PhoneNumber = profile.PhoneNumber,
            ProfilePicture = profile.ProfilePicture
        };
        return Ok(response);
    }

    [HttpPut("{id}/profile")]
    public IActionResult UpdateUserProfile(int id, UserProfileDto profileDto)
    {
        if (id != profileDto.UserId)
        {
            return BadRequest();
        }

        var existingProfile = _userRepository.GetUserProfile(id, true);
        if (existingProfile == null)
        {
            return NotFound();
        }

        // Обновляем только те поля, которые были предоставлены
        if (profileDto.FirstName != null)
            existingProfile.FirstName = profileDto.FirstName;
        
        if (profileDto.LastName != null)
            existingProfile.LastName = profileDto.LastName;
        
        if (profileDto.Bio != null)
            existingProfile.Bio = profileDto.Bio;
        
        if (profileDto.Affiliation != null)
            existingProfile.Affiliation = profileDto.Affiliation;
        
        if (profileDto.Website != null)
            existingProfile.Website = profileDto.Website;
        
        if (profileDto.PhoneNumber != null)
            existingProfile.PhoneNumber = profileDto.PhoneNumber;
        
        if (profileDto.ProfilePicture != null)
            existingProfile.ProfilePicture = profileDto.ProfilePicture;

        _userRepository.UpdateUserProfile(existingProfile);
        return NoContent();
    }
} 