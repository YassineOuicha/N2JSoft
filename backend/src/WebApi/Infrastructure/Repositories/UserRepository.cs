using Microsoft.EntityFrameworkCore;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Persistence;

namespace WebApi.Infrastructure.Repositories;

internal sealed class UserRepository(AppDbContext db): IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        return await db.Users
            .Where(u => !u.IsDeleted)
            .FirstOrDefaultAsync(u => u.Id == userId, ct);
    }

    public async Task<IReadOnlyList<User>> ListAsync(bool onlyActive, CancellationToken ct)
    {
        var query = db.Users.AsNoTracking().AsQueryable().Where(u => !u.IsDeleted);
        if (onlyActive)
        {
            query = query.Where(u => u.IsActive);
        }
        return await query
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .ToListAsync(ct);
    }

    public async Task AddAsync(User user, CancellationToken ct)
    {
        await db.Users.AddAsync(user, ct);
        await db.SaveChangesAsync(ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid userId, CancellationToken ct)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId, ct);
        if (user != null)
        {
            user.IsDeleted = true;
            await db.SaveChangesAsync(ct);
        }
    }
}