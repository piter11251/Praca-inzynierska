using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.Account;
using DiabetesApp.Entities;
using DiabetesApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiabetesApp.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly DiabetesDbContext _context;
        private readonly IAccountService _accountService;
        public AccountController(DiabetesDbContext context, IAccountService accountService)
        {
            _context = context;
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _accountService.Register(dto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            Console.WriteLine($"Login attempt: {dto.Email}");
            var token = await _accountService.Login(dto);
            return Ok(token);
        }

        [HttpGet("get-info")]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Brak userId w tokenie");
            }
            try
            {
                var profileDto = await _accountService.GetUserProfileAsync(userId);
                return Ok(profileDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        
    }
}
