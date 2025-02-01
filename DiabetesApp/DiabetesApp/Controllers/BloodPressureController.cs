using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.BloodPressureDtos;
using DiabetesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiabetesApp.Controllers
{
    [ApiController]
    [Route("api/pressure-entries")]
    public class BloodPressureController : ControllerBase
    {
        private readonly DiabetesDbContext _context;
        private readonly IBloodPressureService _bloodPressureService;
        public BloodPressureController(DiabetesDbContext context, IBloodPressureService bloodPressureService)
        {
            _context = context;
            _bloodPressureService = bloodPressureService;
        }

        [HttpPost("create-pressure")]
        public async Task<IActionResult> CreateBloodPressureEntry([FromBody] CreateBloodPressureEntryDto dto)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Nie jesteś zalogowany");
            }
            await _bloodPressureService.CreateBloodPressureEntry(dto, userId);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ModifyBloodPressureEntry([FromBody] ModifyBloodPressureDto dto, [FromRoute] int id)
        {
            await _bloodPressureService.ModifyBloodPressureEntry(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodPressureEntry([FromRoute]int id)
        {
            await _bloodPressureService.DeleteBloodPressureEntry(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBloodPressureEntries([FromQuery] int days = 7)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Brak identyfikatora uzytkownika w tokenie");
            }
            var list = await _bloodPressureService.GetAllBloodPressureEntries(userId, days);
            return Ok(list);
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBloodPressureEntryById([FromRoute] int id)
        {
            var entry = await _bloodPressureService.GetBloodPressureById(id);
            return Ok(entry);
        }

    }
}
