using WebApi.Application.Dtos;
using WebApi.Application.Interfaces;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Application.UseCases;

public class UserService(IUserRepository users)
{
    public async Task<IReadOnlyList<UserListItemDto>> ListAsync(bool onlyActive, CancellationToken ct)
    {
        var list = await users.ListAsync(onlyActive, ct);
        return list
            .Select(u => new UserListItemDto(u.Id, u.FirstName, u.LastName, u.IsActive, u.MonthlyExpenseLimit))
            .ToList();
    }
    
    public async Task<UserDetailDto?> GetByIdAsync(Guid userId, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(userId, ct);
        if (user == null)
        {
            return null;
        }

        return new UserDetailDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.IsActive,
            user.MonthlyExpenseLimit,
            user.PostalAddress.Street,
            user.PostalAddress.City,
            user.PostalAddress.PostalCode,
            user.PostalAddress.Country
        );
    }
    
    public async Task<Guid> CreateAsync(CreateUserDto dto, CancellationToken ct)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            IsActive = dto.IsActive,
            MonthlyExpenseLimit = dto.MonthlyExpenseLimit,
            PostalAddress = new PostalAddress(
                dto.Street,
                dto.PostalCode,
                dto.City,
                dto.Country
            ),
            IsDeleted = false
        };

        await users.AddAsync(user, ct);
        await users.SaveChangesAsync(ct);
        
        return user.Id;
    }
    
    public async Task<bool> UpdateAsync(Guid userId, UpdateUserDto dto, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(userId, ct);
        if (user == null)
        {
            return false;
        }
        
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.IsActive = dto.IsActive;
        user.MonthlyExpenseLimit = dto.MonthlyExpenseLimit;
        user.PostalAddress = new PostalAddress(
            dto.Street,
            dto.PostalCode,
            dto.City,
            dto.Country
        );

        await users.SaveChangesAsync(ct);
        return true;
    }
    
    public async Task<bool> DeleteAsync(Guid userId, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(userId, ct);
        if (user == null)
        {
            return false;
        }

        await users.DeleteAsync(userId, ct);
        await users.SaveChangesAsync(ct);
        return true;
    }
}