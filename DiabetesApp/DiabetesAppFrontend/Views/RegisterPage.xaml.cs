using Demo.ApiClient;
//using DiabetesAppFrontend.Models;
using System.Text.RegularExpressions;
using Demo.ApiClient.Models.ApiModels;

namespace DiabetesAppFrontend.Views;

public partial class RegisterPage : ContentPage
{
    private readonly DemoApiClientService _apiService;
    public RegisterPage(DemoApiClientService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
    }

    private async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        ResetErrorMessages();
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;
        var confirmPassword = ConfirmPasswordEntry.Text;
        var firstName = FirstNameEntry.Text?.Trim();
        var lastName = LastNameEntry.Text?.Trim();
        var birthDate = BirthDatePicker.Date;
        var gender = GenderPicker.SelectedIndex;
        var diabetesType = DiabetesTypePicker.SelectedIndex;

        var hasErrors = false;

        if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
        {
            EmailErrorLabel.Text = "Podaj prawid?owy email.";
            EmailErrorLabel.IsVisible = true;
            hasErrors = true;
        }

        if (string.IsNullOrEmpty(password) || password.Length < 6)
        {
            PasswordErrorLabel.Text = "Has?o musi mie? co najmniej 6 znaków";
            PasswordErrorLabel.IsVisible = true;
            hasErrors = true;
        }

        if (confirmPassword != password)
        {
            ConfirmPasswordErrorLabel.Text = "Has?a nie s? takie same";
            ConfirmPasswordErrorLabel.IsVisible = true;
            hasErrors = true;
        }

        if (string.IsNullOrEmpty(firstName))
        {
            FirstNameErrorLabel.Text = "Podaj imi?.";
            FirstNameErrorLabel.IsVisible = true;
            hasErrors = true;
        }

        if (string.IsNullOrEmpty(lastName))
        {
            LastNameErrorLabel.Text = "Podaj nazwisko.";
            LastNameErrorLabel.IsVisible = true;
            hasErrors = true;
        }

        if (gender == null)
        {
            GenderErrorLabel.Text = "Wybierz p?e?";
            GenderErrorLabel.IsVisible = true;
            hasErrors = true;
        }

        if (diabetesType == null)
        {
            DiabetesTypeErrorLabel.Text = "Wybierz typ cukrzycy.";
            DiabetesTypeErrorLabel.IsVisible = true;
            hasErrors = true;
        }

        if (hasErrors) return;

        var registerDto = new RegisterDto
        {
            Email = email,
            Password = password,
            ConfirmPassword = confirmPassword,
            FirstName = firstName,
            LastName = lastName,
            BirthDate = birthDate,
            Gender = (int)gender,
            DiabetesType = (int)diabetesType,
        };

        try
        {
            await _apiService.RegisterAsync(registerDto);
            await DisplayAlert("Sukces", "Rejestracja zakonczona pomyslnie", "Ok");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            GeneralErrorLabel.Text = $"B??d rejestracji {ex.Message}";
            GeneralErrorLabel.IsVisible = true;
        }
    }

    private void ResetErrorMessages()
    {
        EmailErrorLabel.IsVisible = false;
        PasswordErrorLabel.IsVisible = false;
        ConfirmPasswordErrorLabel.IsVisible = false;
        FirstNameErrorLabel.IsVisible = false;
        LastNameErrorLabel.IsVisible = false;
        BirthDateErrorLabel.IsVisible = false;
        GenderErrorLabel.IsVisible = false;
        DiabetesTypeErrorLabel.IsVisible = false;
        GeneralErrorLabel.IsVisible = false;
    }

    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

}