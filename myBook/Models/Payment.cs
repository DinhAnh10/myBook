using System.ComponentModel.DataAnnotations.Schema;

namespace myBook.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double TotalPrice { get; set; }
        public string? StripePaymentIntentID { get; set; }
        public string? PaymentStatus { get; set; }
        public int ShoppingCartId { get; set; }
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }
    }
}
