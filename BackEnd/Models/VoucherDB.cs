namespace ClothesWeb.Models
{
    public class VoucherDB
    {
        public int id { get; set; } = 0;
        public string Code { get; set;} = string.Empty;
        public int Amount { get; set;}
        public string Type { get; set; } = string.Empty;
        public double Discount { get; set; }
        public bool HasUsed {get; set;}
    }
}
