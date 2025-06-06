using System.ComponentModel.DataAnnotations;

namespace myBook.Models
{
    public class TacGia
    {
        [Key]
        public string MaTG { get; set; } = Guid.NewGuid().ToString("N")[..8].ToUpper();

        [Required(ErrorMessage = "Tên tác giả không được bỏ trống")]
        public string TenTG { get; set; }

        public List<Sach>? Sach { get; set; }
    }
}
