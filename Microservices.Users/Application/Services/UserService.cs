using System.Security.Claims;
using Microservices.Core.Auth.Models;
using Microservices.Users.Core.DTO;
using Microservices.Users.Core.Entities;
using Microservices.Users.Core.Exceptions;
using Microservices.Users.Core.Services;
using Microservices.Users.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Users.Application.Services;

public class UserService(DataContext context, IJwtService jwtService, ILogger<UserService> logger) : IUserService
{
    private async Task AssertRepeatingUserNameAsync(string username)
    {
        var UserExists = (await context.Users
                .AsNoTracking()
                .ToListAsync()) 
            .FirstOrDefault(entity =>
                string.Equals(entity.Username.Trim(),  username.TrimEnd(), StringComparison.CurrentCultureIgnoreCase));

        if (UserExists is not null)
        {
            throw new UserAlreadyExistsException();
        }
    }

    private async Task AssertUserExistsAsync(int id)
    {
        var candidate = await context.Users.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);

        if (candidate is null)
        {
            throw new UserNotFoundException();
        }
    }
    
    public async Task<JwtModel> CreateAsync(UserCreateDTO dto)
    {
        await AssertRepeatingUserNameAsync(dto.Username);
        
        var candidate = await context.Users.AddAsync(new UserEntity
        {
            Username = dto.Username, Password = dto.Password
        });

        await context.SaveChangesAsync();
        
        var token = jwtService.CreateToken(new List<Claim>
        {
            new("username", candidate.Entity.Username),
            new("id", candidate.Entity.Id.ToString())
        });

        return token;
    }

    public async Task<UserEntity> UpdateAsync(int id, UserUpdateDTO dto)
    {
        await AssertUserExistsAsync(id);
        
        var candidate = await context.Users.FirstOrDefaultAsync(entity => entity.Id == id);

        candidate.Password = dto.Password;

        await context.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<UserEntity> DeleteAsync(int id)
    {
        await AssertUserExistsAsync(id);
        
        var candidate = await context.Users
            .Include(e => e.Languages)
            .FirstOrDefaultAsync(entity => entity.Id == id);
        
        var result = context.Users.Remove(candidate);

        await context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<UserEntity> GetByIdAsync(int id)
    {
        await AssertUserExistsAsync(id);
        
        return await context.Users
            .Include(e => e.Languages)
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<ICollection<UserEntity>> GetAllAsync()
    {
        return await context.Users
            .Include(e => e.Languages)
            .OrderBy(entity => entity.Id).ToListAsync();
    }

    public async Task DeleteAllAsync()
    {
       context.Users.RemoveRange(context.Users);

       await context.SaveChangesAsync();
    }

    public async Task<UserEntity> GetByUsernameAsync(string username)
    {
        var candidate = await context.Users
            .Include(e => e.Languages)
            .AsNoTracking()
            .FirstOrDefaultAsync(entity => entity.Username == username);

        if (candidate is null)
        {
            throw new UserNotFoundException();
        }

        return candidate;
    }
    
    public async Task<JwtModel> LoginAsync(UserLoginDTO dto)
    {
        var candidate = await GetByUsernameAsync(dto.Username);

        var equals = candidate.PasswordEquals(dto.Password);

        if (!equals)
        {
            throw new UserInvalidPasswordException();
        }
        
        var token = jwtService.CreateToken(new List<Claim>
        {
            new("username", candidate.Username),
            new("id", candidate.Id.ToString())
        });
        
        return token;
    }

    public async Task<bool> RegisterLanguageAsync(int userId, int languageId)
    {
        try
        {
            await AssertUserExistsAsync(userId);

            var language = new UserLanguageEntity
            {
                LanguageId = languageId,
                UserId = userId
            };

            await context.UserLanguages.AddAsync(language);
            await context.SaveChangesAsync();
            
            return true;    
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            return false;
        }
    }
}