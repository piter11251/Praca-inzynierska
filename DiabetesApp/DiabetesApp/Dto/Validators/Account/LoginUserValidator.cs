using DiabetesApp.Dto.Account;
using FluentValidation;

namespace DiabetesApp.Dto.Validators.Account
{
    public class LoginUserValidator: AbstractValidator<LoginDto>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
