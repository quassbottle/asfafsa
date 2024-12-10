using Microservices.Languages.Core.DTO;
using Microservices.Languages.Core.Entities;

namespace Microservices.Languages.Core.Services;

public interface ILanguageService
{
    Task<LanguageEntity> CreateAsync(LanguageCreateDTO dto);
    Task<LanguageEntity> UpdateAsync(int id, LanguageUpdateDTO dto);
    Task<LanguageEntity> DeleteAsync(int id);
    Task<LanguageEntity> GetByIdAsync(int id);
    Task<ICollection<LanguageEntity>> GetAllAsync();
    Task DeleteAllAsync();
    Task<LanguageEntity> UpdateStatusAsync(int id, string status);
}