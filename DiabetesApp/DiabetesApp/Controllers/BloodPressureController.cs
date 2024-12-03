using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.BloodPressureDtos;
using DiabetesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> CreateBloodPressureEntry([FromBody] CreateBloodPressureEntryDto dto)
        {
            await _bloodPressureService.CreateBloodPressureEntry(dto);
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
        public async Task<IActionResult> GetAllBloodPressureEntries()
        {
            var list = await _bloodPressureService.GetAllBloodPressureEntries();
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
