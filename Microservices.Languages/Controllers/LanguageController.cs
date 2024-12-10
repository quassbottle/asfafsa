using Microservices.Languages.Controllers.Models;
using Microservices.Languages.Core.Entities;
using Microservices.Languages.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Languages.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguageController(ILanguageService languageService) : Controller
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<LanguageEntity>))]
    public async Task<IActionResult> GetAllLanguages()
    {
        return Ok(await languageService.GetAllAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(LanguageEntity))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetLanguageById(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await languageService.GetByIdAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(LanguageEntity))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateLanguage([FromBody] LanguageCreateRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await languageService.CreateAsync(request));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(LanguageEntity))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateLanguage([FromRoute] int id, [FromBody] LanguageUpdateRequest updateLanguage)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await languageService.UpdateAsync(id, updateLanguage));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(200, Type = typeof(LanguageEntity))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteLanguage(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(await languageService.DeleteAsync(id));
    }

    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeleteAllLanguages()
    {
        await languageService.DeleteAllAsync();
        return Ok();
    }
}