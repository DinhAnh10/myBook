using System.ComponentModel.DataAnnotations;

namespace myBook.Models
{
    public class TacGia
    {
        [Key]
        public string MaTG { get; set; }

        [Required(ErrorMessage = "Tên tác giả không được bỏ trống")]
        public string TenTG { get; set; }

        public List<Sach>? Sach { get; set; }
    }
}
