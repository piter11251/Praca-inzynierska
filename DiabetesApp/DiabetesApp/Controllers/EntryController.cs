using Microsoft.AspNetCore.Mvc;
using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Services.Interfaces;
using DiabetesApp.Dto.EntryDtos;
using System.Formats.Asn1;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DiabetesApp.Controllers
{
    [ApiController]
    [Route("api/sugar-entries")]
    public class EntryController : ControllerBase
    {
        private readonly DiabetesDbContext _context;
        private readonly IEntryService _entryService;
        public EntryController(DiabetesDbContext context, IEntryService entryService)
        {
            _context = context;
            _entryService = entryService;
        }

        [HttpPost("create-entry")]
        public async Task<IActionResult> CreateEntry([FromBody] CreateEntryDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Nie jesteś zalogowany");
            }
            await _entryService.CreateEntry(dto, userId);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ModifyEntry([FromBody] ModifyEntryDto dto, int id)
        {
            await _entryService.ModifyEntry(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry([FromRoute] int id)
        {
            await _entryService.DeleteEntry(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEntries([FromQuery] int days = 7)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Brak identyfikatora uzytkownika w tokenie");
            }
            var entries = await _entryService.GetAllEntriesForUser(userId, days);
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntryById([FromRoute] int id)
        {
            var entry = await _entryService.GetEntryById(id);
            return Ok(entry);
        }
    }
}
