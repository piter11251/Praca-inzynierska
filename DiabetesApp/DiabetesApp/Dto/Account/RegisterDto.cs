using DiabetesApp.Entities.Enum;

namespace DiabetesApp.Dto.Account
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public DiabetesType DiabetesType { get; set; }

    }
}
