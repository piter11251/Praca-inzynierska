using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.Account;
using DiabetesApp.Entities;
using DiabetesApp.Entities.Enum;
using DiabetesApp.Exceptions;
using DiabetesApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DiabetesApp.Services
{
    public class AccountService: IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly DiabetesDbContext _context;

        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager, DiabetesDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                throw new BadRequestException("Nieprawidłowe dane logowania");
            }

            return await GenerateJwt(user);
        }

        public async Task Register(RegisterDto dto)
        {
            if(dto.Password != dto.ConfirmPassword)
            {
                throw new BadRequestException("Hasla nie są takie same.");
            }

            var user = new User
            {
                Email = dto.Email,
                UserName = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                DiabetesType = dto.DiabetesType,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            
            if(!result.Succeeded)
            {
                throw new BadRequestException(result.Errors.ToString());
            }
            var userPreferences = InitializeUserPreferences(user);
            user.UserPreferences = userPreferences;
            _context.UserPreferences.Add(userPreferences);
            await _context.SaveChangesAsync();

        }

        public async Task<string> GenerateJwt(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("given_name", user.FirstName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKeyDon'tShareIt1234567890"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "diabetesapp.com",
                audience: "diabetesapp.com",
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private UserPreferences InitializeUserPreferences(User user)
        {
            return new UserPreferences
            {
                UserId = user.Id,
                PreferableSugarLevels = new List<PreferableSugarLevel>
                {
                    new PreferableSugarLevel
                    {
                        MealMarker = MealMarker.On_Empty_Stomach,
                        MinValue = 70,
                        MaxValue = 110
                    },
                    new PreferableSugarLevel
                    {
                        MealMarker = MealMarker.Before_Meal,
                        MinValue = 70,
                        MaxValue = 110
                    },
                    new PreferableSugarLevel
                    {
                        MealMarker = MealMarker.After_Meal,
                        MinValue = 70,
                        MaxValue = 180
                    }
                }
            };
        }
    }
}
