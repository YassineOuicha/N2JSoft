using WebApi.Domain.Entities;

namespace WebApi.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId, CancellationToken ct);
    Task<IReadOnlyList<User>> ListAsync(bool onlyActive, CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
    Task DeleteAsync(Guid userId, CancellationToken ct);
}