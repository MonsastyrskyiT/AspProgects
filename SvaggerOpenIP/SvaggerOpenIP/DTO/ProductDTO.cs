using System.ComponentModel.DataAnnotations;

namespace SvaggerOpenIP.DTO
{

    namespace SwaggerOpenApi.DTO
    {
        /// <summary>
        /// DTO for create products
        /// </summary>
        public class ProductDTO
        {

            public Guid Id { get; set; }
            ///<summary>Product Name</summary>
            ///<example>Laptop Lenovo</example>
            [Required]
            public string Name { get; set; } = string.Empty;
            ///<summary>Price in dollars</summary>
            ///<example>200$</example>
            [Range(0.01, 1000000)]
            public decimal Price { get; set; }
        }
    }
}
