using Microsoft.AspNetCore.Mvc;
using WebApi.Application.Dtos;
using WebApi.Application.UseCases;

namespace WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(UserService userService) : ControllerBase
{
    // GET api/users?onlyActive=true
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserListItemDto>>> Get([FromQuery] bool onlyActive, CancellationToken ct){
        var users = await userService.ListAsync(onlyActive, ct);
        return Ok(users);
    }

    // GET api/users/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDetailDto>> GetById(Guid id, CancellationToken ct)
    {
        var user = await userService.GetByIdAsync(id, ct);
        return user == null ? NotFound("No user found for the specified Id") : Ok(user);
    }
    
    // Post api/users
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto, CancellationToken ct)
    {
        var id = await userService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    
    // PUT api/users/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto, CancellationToken ct)
    {
        var isUpdated = await userService.UpdateAsync(id, dto, ct);
        return isUpdated? NoContent(): NotFound("User specified not found");
    }
    
    // DELETE api/users/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var isDeleted = await userService.DeleteAsync(id, ct);
        return isDeleted ? NoContent() : NotFound("User specified not found");
    }
}