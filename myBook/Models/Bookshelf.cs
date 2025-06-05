using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace myBook.Models
{
    public class Bookshelf
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string MaSach { get; set; }

        [ForeignKey("MaSach")]
        public Sach Sach { get; set; }
    }
}