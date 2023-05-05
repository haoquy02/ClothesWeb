namespace ClothesWeb.Models
{
    public class OrderDB
    {
        public int id { get; set; }
        public int AccountId { get; set; }
        public int ClothesId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }  =string.Empty;
        public string Color { get; set; } = string.Empty;   
        public string Status { get; set; }
    }
}
