using Microsoft.AspNetCore.Mvc;
using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Services.Interfaces;
using DiabetesApp.Dto;

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
            await _entryService.CreateEntry(dto);
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
    }
}
