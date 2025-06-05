using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myBook.Models
{
    public class TheLoai
    {
        [Key]
        [Required]
        public string MaTheLoai { get; set; }  // PK: phải trùng kiểu với Sach.MaTheLoai

        [Required]
        [StringLength(100)]
        public string TenTheLoai { get; set; }

        public string? MoTa { get; set; }

        // Navigation
        public List<Sach>? Sach { get; set; }
    }
}
