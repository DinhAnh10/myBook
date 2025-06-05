using System.ComponentModel.DataAnnotations.Schema;

namespace myBook.Models
{
    public class Anh
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string MaSach { get; set; }
        [ForeignKey("MaSach")]
        public Sach? Sach { get; set; }
    }
}
