namespace Spg._4BHifShop.Models
{
    public class SchoppingCartItemDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Tax { get; set; }
        public int Pieces { get; set; }
    }
}
