namespace ClothesWeb.Dto.Account
{
    public class CreateAccountDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string VerificationCodeFromUser { get; set; }= string.Empty;
        public string VerificationCodeFromProgram { get; set; } = string.Empty;
    }
}
