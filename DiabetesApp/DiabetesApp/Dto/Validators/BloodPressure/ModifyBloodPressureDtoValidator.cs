using DiabetesApp.Dto.BloodPressureDtos;
using FluentValidation;

namespace DiabetesApp.Dto.Validators.BloodPressure
{
    public class ModifyBloodPressureDtoValidator : AbstractValidator<ModifyBloodPressureDto>
    {
        public ModifyBloodPressureDtoValidator()
        {
            RuleFor(x => x.StolicPressure)
                .GreaterThan(x => x.DiastolicPressure)
                .When(x => x.StolicPressure.HasValue && x.DiastolicPressure.HasValue)
                .WithMessage("Ciśnienie skurczowe musi być większe niż ciśnienie rozkurczowe");

            RuleFor(x => x.DiastolicPressure)
                .LessThan(x => x.StolicPressure)
                .When(x => x.DiastolicPressure.HasValue && x.StolicPressure.HasValue)
                .WithMessage("Ciśnienie rozkurczowe musi być niższe niż ciśnienie skurczowe");

            RuleFor(x => x.StolicPressure)
                .GreaterThan(0)
                .When(x => x.StolicPressure.HasValue)
                .WithMessage("StolicPressure musi być większe niż 0");

            RuleFor(x => x.DiastolicPressure)
                .GreaterThan(0)
                .When(x => x.DiastolicPressure.HasValue)
                .WithMessage("Ciśnienie rozkurczowe musi być większe niż 0");

            RuleFor(x => x.Pulse)
                .GreaterThan(0)
                .When(x => x.Pulse.HasValue)
                .WithMessage("Puls musi być większy niż 0");

            RuleFor(x => x.MeasurementDate)
                .Must(date => !date.HasValue || date.Value <= DateTime.Now)
                .WithMessage("Nie mozesz podać daty z przyszłości");
        }
    }
}
