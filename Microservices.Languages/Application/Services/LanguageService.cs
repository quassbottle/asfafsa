using Confluent.Kafka;
using MassTransit;
using Microservices.Core.Messages.Languages;
using Microservices.Languages.Core.DTO;
using Microservices.Languages.Core.Entities;
using Microservices.Languages.Core.Exceptions;
using Microservices.Languages.Core.Services;
using Microservices.Languages.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Languages.Application.Services;

public class LanguageService(DataContext context, ITopicProducer<OnLanguageCreatedMessage> producer) : ILanguageService
{
    private async Task AssertLanguageExistsAsync(int id)
    {
        var candidate = await context.Languages.FirstOrDefaultAsync(entity => entity.Id == id);

        if (candidate is null)
        {
            throw new LanguageNotFoundException();
        }
    }
    
    private async Task AssertRepeatingLanguageNameAsync(string name)
    {
        var languageExists = (await context.Languages
                .AsNoTracking()
                .ToListAsync()) 
            .FirstOrDefault(entity =>
                string.Equals(entity.Name.Trim(),  name.TrimEnd(), StringComparison.CurrentCultureIgnoreCase));

        if (languageExists is not null)
        {
            throw new LanguageAlreadyExistsException();
        }
    }
    
    public async Task<LanguageEntity> CreateAsync(LanguageCreateDTO dto)
    {
        await AssertRepeatingLanguageNameAsync(dto.Name);
        
        var candidate = await context.Languages.AddAsync(new LanguageEntity
        {
            Description = dto.Description, Name = dto.Name, LenghtOfCourse = dto.LengthOfCourse, UserId = dto.UserId
        });
        
        await context.SaveChangesAsync();

        await producer.Produce(new OnLanguageCreatedMessage
        {
            LanguageId = candidate.Entity.Id,
            UserId = candidate.Entity.UserId
        });

        return candidate.Entity;
    }

    public async Task<LanguageEntity> UpdateAsync(int id, LanguageUpdateDTO dto)
    {
        await AssertLanguageExistsAsync(id);
        
        var candidate = await context.Languages.FirstOrDefaultAsync(entity => entity.Id == id);
        
        await AssertRepeatingLanguageNameAsync(dto.Name);
        
        candidate.Description = dto.Description;
        candidate.LenghtOfCourse = dto.LengthOfCourse;
        candidate.Name = dto.Name;

        await context.SaveChangesAsync();

        return candidate;
    }

    public async Task<LanguageEntity> DeleteAsync(int id)
    {
        await AssertLanguageExistsAsync(id);
        
        var candidate = await context.Languages.FirstOrDefaultAsync(entity => entity.Id == id);
        
        var result = context.Languages.Remove(candidate);

        await context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<LanguageEntity> GetByIdAsync(int id)
    {
        await AssertLanguageExistsAsync(id);
        
        var candidate = await context.Languages.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id);

        if (candidate is null)
        {
            throw new LanguageNotFoundException();
        }

        return candidate;
    }

    public async Task<ICollection<LanguageEntity>> GetAllAsync()
    {
        return await context.Languages.OrderBy(entity => entity.Id).ToListAsync();
    }

    public async Task DeleteAllAsync()
    {
       context.Languages.RemoveRange(context.Languages);

       await context.SaveChangesAsync();
    }

    public async Task<LanguageEntity> UpdateStatusAsync(int id, string status)
    {
        await AssertLanguageExistsAsync(id);
        
        var candidate = await context.Languages.FirstOrDefaultAsync(entity => entity.Id == id);

        candidate!.Status = status;
        
        await context.SaveChangesAsync();

        return candidate;
    }
}