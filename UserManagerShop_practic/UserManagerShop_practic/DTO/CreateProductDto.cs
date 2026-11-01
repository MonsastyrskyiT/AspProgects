using System.ComponentModel.DataAnnotations;

namespace UserManagerShop_practic.DTO
{
    public class CreateProductDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(12)]
        public string Sku { get; set; } = string.Empty;
        [Range(typeof(decimal), "0", "9999999999999999.99")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
    }
}
