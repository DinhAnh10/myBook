using System.ComponentModel.DataAnnotations.Schema;

namespace myBook.Models
{
    public class ShoppingCartDetail
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }

        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }
        public string MaSach { get; set; }

        [ForeignKey("MaSach")]
        public Sach Sach { get; set; }
        public double Price { get; set; } 
    }
}
