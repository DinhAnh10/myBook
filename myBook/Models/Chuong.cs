using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myBook.Models
{
    public class Chuong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChuong { get; set; }
        public string TenChuong { get; set; }
        public int SoThuTu { get; set; }
        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }
        public string? MaSach { get; set; }
        [ForeignKey("MaSach")]
        public Sach? Sach { get; set; }
    }
}
