using WebApi.Domain.Entities;

namespace WebApi.Application.Interfaces;

internal interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid userId, CancellationToken ct);
    Task<IReadOnlyList<User>> GetUsersAsync(bool onlyActive, CancellationToken ct);
    Task AddUserAsync(User user, CancellationToken ct);
    Task UpdateUserAsync(User user, CancellationToken ct);
    Task DeleteUserAsync(Guid userId, CancellationToken ct);
}