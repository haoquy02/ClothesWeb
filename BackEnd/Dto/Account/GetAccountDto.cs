namespace ClothesWeb.Dto.Account
{
    public class GetAccountDto
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

    }
}
