using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents an individual product for sale in the eCommerce application.
    /// </summary>
    public class Product
    {
        [Key]
        //The unique identifier for the product
        public int ProductId { get; set; }

        [StringLength(50, ErrorMessage = "The product name cannot exceed 50 characters.")]
        //The name of the product
        public required string Title { get; set; }

        [Range (0, 10_000)]
        //the price of the product with an upper limit of 10,000 $
        public decimal Price { get; set; }
    }
}
