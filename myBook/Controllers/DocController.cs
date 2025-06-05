using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myBook.Models;

namespace myBook.Controllers
{
    public class DocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Chuong(int id)
        {
            var chuong = _context.Chuongs
                .Include(c => c.Sach)
                    .ThenInclude(s => s.TacGia)
                .Include(c => c.Sach)
                    .ThenInclude(s => s.Anhs)
                .FirstOrDefault(c => c.MaChuong == id);

            if (chuong == null) return NotFound();

            // Lấy danh sách chương của cùng quyển sách
            var danhSachChuong = _context.Chuongs
                .Where(c => c.MaSach == chuong.MaSach)
                .OrderBy(c => c.SoThuTu)
                .ToList();

            // Tìm vị trí chương hiện tại
            var index = danhSachChuong.FindIndex(c => c.MaChuong == id);
            Chuong? chuongTruoc = index > 0 ? danhSachChuong[index - 1] : null;
            Chuong? chuongSau = index < danhSachChuong.Count - 1 ? danhSachChuong[index + 1] : null;

            ViewBag.ChuongTruoc = chuongTruoc;
            ViewBag.ChuongSau = chuongSau;

            return View(chuong);
        }


        public IActionResult DanhSachChuong(string sachId)
        {
            var chuongs = _context.Chuongs
                .Where(c => c.MaSach == sachId)
                .OrderBy(c => c.SoThuTu)
                .ToList();

            ViewBag.Sach = _context.Sachs.FirstOrDefault(s => s.MaSach == sachId);
            return View(chuongs);
        }
    }
}
