namespace ClothesWeb.Models
{
    public class VoucherDB
    {
        public int id { get; set; }
        public string Code { get; set;} = string.Empty;
        public int Amount { get; set;}
        public string Type { get; set; } = string.Empty;
    }
}
