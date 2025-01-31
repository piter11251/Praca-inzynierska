using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.ViewModels
{
    public partial class RegisterViewModel: ObservableObject
    {
        private readonly DemoApiClientService _apiService;

        [ObservableProperty] private string email;
        [ObservableProperty] private string password;
        [ObservableProperty] private string confirmPassword;
        [ObservableProperty] private string firstName;
        [ObservableProperty] private string lastName;
        [ObservableProperty] private DateTime birthDate = DateTime.Now;
        [ObservableProperty] private int gender;
        [ObservableProperty] private int diabetesType;
        [ObservableProperty] private string errorMessage;

        public RegisterViewModel(DemoApiClientService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        private async Task RegisterAsync()
        {
            var emailTrimmed = Email?.Trim() ?? "";
            var passwordTrimmed = Password?.Trim() ?? "";
            var confirmTrimmed = ConfirmPassword?.Trim() ?? "";

            ErrorMessage = string.Empty;
            if(string.IsNullOrEmpty(emailTrimmed) || !Regex.IsMatch(emailTrimmed, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ErrorMessage = "Podaj prawidłowy email";
                return;
            }
            if(string.IsNullOrEmpty(passwordTrimmed) || passwordTrimmed.Length < 6)
            {
                ErrorMessage = "Hasło za krótkie(min. 6 znaków)";
                return;
            }
            if(confirmTrimmed != passwordTrimmed)
            {
                ErrorMessage = $"Hasła nie są takie same";
                return;
            }
            if(string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
            {
                ErrorMessage = "Podaj imie i nazwisko";
                return;
            }

            var dto = new RegisterDto
            {
                Email = emailTrimmed,
                Password = passwordTrimmed,
                ConfirmPassword = confirmTrimmed,
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate,
                Gender = gender,
                DiabetesType = diabetesType
            };

            try
            {
                await _apiService.RegisterAsync(dto);
                await Shell.Current.GoToAsync("LoginPage");
            }
            catch(Exception ex)
            {
                ErrorMessage = $"Błąd rejestracji: {ex.Message}";
            }
        }
    }
}
