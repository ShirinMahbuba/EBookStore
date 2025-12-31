using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShoppingartMvcUI.Models
{
    [Table ("OrderDetails")]
    public class OrderDetails
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Required]
        public int BookId { get; set; }

        public Book Book { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double  UnitPrice { get; set; }
    }
}
