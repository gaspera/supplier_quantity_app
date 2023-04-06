using System.ComponentModel.DataAnnotations;

namespace SupplierQuantityServerApp.Models
{
    public class Customer
    {
        [Key]
        [Display(Name = "Supplier")]
        public string? Name { get; set; }
        public string? Password { get; set; }
        public decimal? Quantity { get; set; }
    }
}
