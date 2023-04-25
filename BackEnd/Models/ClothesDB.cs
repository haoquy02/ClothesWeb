namespace ClothesWeb.Models
{
    public class ClothesDB
    {
        public int id { get; set; }
        public string ClothesName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int Prices { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Material { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
