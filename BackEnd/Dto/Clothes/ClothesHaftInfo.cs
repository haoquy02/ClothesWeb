namespace ClothesWeb.Dto.Clothes
{
    public class ClothesHalfInfo
    {
        public int id { get; set; }
        public string ClothesName { get; set; } = string.Empty;
        public int Prices { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
