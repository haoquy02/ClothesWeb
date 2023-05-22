namespace ClothesWeb.Dto.Order
{
    public class PayObject
    {
        public List<int> ListOrder { get; set; } = new List<int>();
        public List<string> ListClothesName { get; set; } = new List<string>();
        public string sum { get; set; } = string.Empty;
    }
}
