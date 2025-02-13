using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.UserPreferencesDtos;
using DiabetesApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DiabetesApp.Controllers
{
    [ApiController]
    [Route("api/user-preferences")]
    [Authorize]
    public class UserPreferencesController: ControllerBase
    {
        private readonly IUserPreferencesService _userPreferencesService;
        public UserPreferencesController(IUserPreferencesService userPreferencesService)
        {
            _userPreferencesService = userPreferencesService;
        }

        [HttpGet]
        public async Task<ActionResult<GetUserPreferencesDto>> GetUserPreferences()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Brak identyfikatora uzytkownika");

            var result = await _userPreferencesService.GetUserPreferencesAsync(userId);
            if (result == null)
                return NotFound("Brak preferencji dla tego uzytkownika");

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserPreferences([FromBody] UpdateUserPreferencesDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Brak identyfikatora uzytkownika");
            await _userPreferencesService.UpdateUserPreferencesAsync(userId, dto);
            return Ok();
        }
    }
}
