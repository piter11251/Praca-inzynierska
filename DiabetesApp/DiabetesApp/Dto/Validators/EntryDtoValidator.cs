using DiabetesApp.Entities.Enum;
using FluentValidation;

namespace DiabetesApp.Dto.Validators
{
    public class EntryDtoValidator<TDto>: AbstractValidator<TDto> where TDto: class
    {
        public EntryDtoValidator()
        {
            When(dto => dto is CreateEntryDto, () =>
            {
                RuleFor(x => ((CreateEntryDto)(object)x).SugarValue)
                .NotEmpty()
                .WithMessage("Wpisz wartość")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Wartość nie może być mniejsza niż 0");

                RuleFor(x => ((CreateEntryDto)(object)x).MealTime)
                    .NotEmpty()
                    .WithMessage("Musisz podać godzine pomiaru")
                    .Must(date => date <= DateTime.Now)
                    .WithMessage("Nie mozesz podac daty z przyszlosci");

                RuleFor(x => ((CreateEntryDto)(object)x).MealMarker)
                    .Must(value => Enum.TryParse<MealMarker>(value, true, out _))
                    .WithMessage("Poprawna wartość to: On_Empty_Stomach/Before_Meal/After_Meal");
            });

            When(dto => dto is ModifyEntryDto, () =>
            {
                RuleFor(x => ((ModifyEntryDto)(object)x).SugarValue)
                .GreaterThan(0)
                .When(x => ((ModifyEntryDto)(object)x).SugarValue.HasValue)
                .WithMessage("SugarValue must be greater than 0.");

                RuleFor(x => ((ModifyEntryDto)(object)x).MealTime)
                    .Must(date => date <= DateTime.Now)
                    .When(x => ((ModifyEntryDto)(object)x).MealTime.HasValue)
                    .WithMessage("MealTime cannot be in the future.");

                RuleFor(x => ((ModifyEntryDto)(object)x).MealMarker)
                    .IsEnumName(typeof(MealMarker), caseSensitive: false)
                    .When(x => !string.IsNullOrEmpty(((ModifyEntryDto)(object)x).MealMarker))
                    .WithMessage("Invalid MealMarker value.");
            });

        }
    }
}
