using DiabetesApp.Dto.BloodPressureDtos;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DiabetesApp.Dto.Validators.BloodPressure
{
    public class CreateBloodPressureDtoValidator : AbstractValidator<CreateBloodPressureEntryDto>
    {
        public CreateBloodPressureDtoValidator()
        {
            RuleFor(x => x.StolicPressure)
                .NotEmpty().WithMessage("Wpisz wartość")
                .GreaterThan(0).WithMessage("StolicPressure musi być większe niż 0")
                .Must((dto, stolicPressure) =>
                {
                    return stolicPressure > dto.DiastolicPressure;
                }).WithMessage("Ciśnienie skurczowe musi być większe niż ciśnienie rozkurczowe");

            RuleFor(x => x.DiastolicPressure)
                .NotEmpty().WithMessage("Wpisz wartość")
                .GreaterThan(0).WithMessage("Ciśnienie rozkurczowe musi być większe niż 0")
                .Must((dto, diastolicPressure) =>
                {
                    return diastolicPressure < dto.StolicPressure;
                }).WithMessage("Ciśnienie rozkurczowe musi być niższe niż skurczowe");

            RuleFor(x => x.Pulse)
                .NotEmpty().WithMessage("Wpisz wartość")
                .GreaterThan(0).WithMessage("Puls musi być większy od zera");

            RuleFor(x => x.MeasurementDate)
                .NotEmpty().WithMessage("Wpisz datę i godzinę badania")
                .Must(date => date <= DateTime.Now).WithMessage("Nie mozesz podać daty z przyszłości");
        }
    }
}
