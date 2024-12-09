using DiabetesApp.DiabetesAppDbContext;
using DiabetesApp.Dto.Account;
using FluentValidation;

namespace DiabetesApp.Dto.Validators.Account
{
    public class RegisterUserValidator: AbstractValidator<RegisterDto>
    {
        public RegisterUserValidator(DiabetesDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords must match");
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.BirthDate)
                .NotEmpty()
                .LessThan(DateTime.Now);

            RuleFor(x => x.Gender)
                .IsInEnum();

            RuleFor(x => x.DiabetesType)
                .IsInEnum();

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailInUse)
                    {
                        context.AddFailure("Email", "Ten email jest już w użyciu");
                    }
                });
        }
    }
}
