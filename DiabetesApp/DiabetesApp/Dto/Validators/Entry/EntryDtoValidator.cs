using DiabetesApp.Dto.EntryDtos;
using DiabetesApp.Entities.Enum;
using FluentValidation;

namespace DiabetesApp.Dto.Validators.Entry
{
    public class EntryDtoValidator<TDto> : AbstractValidator<TDto> where TDto : class
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

                
            });

            When(dto => dto is ModifyEntryDto, () =>
            {
                RuleFor(x => ((ModifyEntryDto)(object)x).SugarValue)
                .GreaterThan(0)
                .When(x => ((ModifyEntryDto)(object)x).SugarValue.HasValue)
                .WithMessage("Cukier powinien być większy niż 0");

                RuleFor(x => ((ModifyEntryDto)(object)x).MealTime)
                    .Must(date => date <= DateTime.Now)
                    .When(x => ((ModifyEntryDto)(object)x).MealTime.HasValue)
                    .WithMessage("Data i godzina posiłku nie może być z przyszłości");

                RuleFor(x => ((ModifyEntryDto)(object)x).MealMarker)
                    .IsEnumName(typeof(MealMarker), caseSensitive: false)
                    .When(x => !string.IsNullOrEmpty(((ModifyEntryDto)(object)x).MealMarker))
                    .WithMessage("Niepoprawna wartość pory pomiaru");
            });

        }
    }
}
