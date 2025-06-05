using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace myBook.Models
{
    public class Sach
    {
        [Key]
        public string MaSach { get; set; } = Guid.NewGuid().ToString("N")[..8].ToUpper();

        [Required(ErrorMessage = "Tên sách không được để trống")]
        public string TenSach { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        [Precision(18, 2)]
        public decimal GiaBan { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string MoTa { get; set; }

        [Required]
        public string MaTheLoai { get; set; }

        [ForeignKey("MaTheLoai")]
        public TheLoai? TheLoai { get; set; }

        [Required]
        public string MaTG { get; set; }

        [ForeignKey("MaTG")]
        public TacGia? TacGia { get; set; }
        public List<Chuong>? Chuongs { get; set; }
        public List<Anh>? Anhs { get; set; }
    }
}
