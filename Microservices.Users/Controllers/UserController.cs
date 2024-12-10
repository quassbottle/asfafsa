using Microservices.Core.Auth.Models;
using Microservices.Core.Controllers;
using Microservices.Users.Core.Entities;
using Microservices.Users.Core.Services;
using Microservices.Users.Controllers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Users.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService): ControllerWithJwt
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UserEntity>))]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await userService.GetAllAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(UserEntity))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetUserById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await userService.GetByIdAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(UserEntity))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await userService.CreateAsync(request));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(JwtModel))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UserUpdateRequest updateUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        AssertUserIdIsSameAsAuth(id);

        return Ok(await userService.UpdateAsync(id, updateUser));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204, Type = typeof(JwtModel))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        AssertUserIdIsSameAsAuth(id);

        return Ok(await userService.DeleteAsync(id));
    }

    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteAllUsers()
    {
        await userService.DeleteAllAsync();
        return Ok();
    }
    
    [HttpPost("login")]
    [ProducesResponseType(200, Type = typeof(JwtModel))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await userService.LoginAsync(request));
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        return Ok(await userService.GetByIdAsync(UserId));
    }
}