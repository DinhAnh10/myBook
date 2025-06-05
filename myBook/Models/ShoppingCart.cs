using System.ComponentModel.DataAnnotations.Schema;

namespace myBook.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public double TotalPrice { get; set; }
    }
}
