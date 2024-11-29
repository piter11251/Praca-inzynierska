using Microsoft.AspNetCore.Mvc;
using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Services.Interfaces;
using DiabetesApp.Dto;

namespace DiabetesApp.Controllers
{
    [ApiController]
    [Route("api/entries")]
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
    }
}
