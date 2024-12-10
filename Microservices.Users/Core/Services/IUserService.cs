using Microservices.Core.Auth.Models;
using Microservices.Users.Core.DTO;
using Microservices.Users.Core.Entities;

namespace Microservices.Users.Core.Services;

public interface IUserService
{
    Task<JwtModel> CreateAsync(UserCreateDTO dto);
    Task<UserEntity> UpdateAsync(int id, UserUpdateDTO dto);
    Task<UserEntity> DeleteAsync(int id);
    Task<UserEntity> GetByIdAsync(int id);
    Task<ICollection<UserEntity>> GetAllAsync();
    Task DeleteAllAsync();

    Task<UserEntity> GetByUsernameAsync(string username);
    Task<JwtModel> LoginAsync(UserLoginDTO dto);
    Task<bool> RegisterLanguageAsync(int userId, int languageId);
}