using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using myBook.Models;
using myBook.Repositories;

namespace myBook.Controllers;

public class HomeController : Controller
{
    private readonly ISachRepository _sachRepository;
    private readonly ILogger<HomeController> _logger;
    private readonly ITheLoaiRepository _loaiRepository;
    private readonly ApplicationDbContext _context;
    public HomeController(ILogger<HomeController> logger, ISachRepository sachRepository, ITheLoaiRepository loaiRepository, ApplicationDbContext context)
    {
        _logger = logger;
        _sachRepository = sachRepository;
        _loaiRepository = loaiRepository;
        _context = context;
    }

    public async Task<IActionResult> Index(string? danhMucId, string? search)
    {
        var sanphams = await _sachRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(danhMucId))
        {
            sanphams = sanphams.Where(sp => sp.MaTheLoai == danhMucId).ToList();
        }

        if (!string.IsNullOrEmpty(search))
        {
            sanphams = sanphams.Where(sp =>
                sp.TenSach.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                sp.MaSach.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (sp.TheLoai != null && sp.TheLoai.TenTheLoai.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                (sp.TacGia != null && sp.TacGia.TenTG.Contains(search, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }

        ViewBag.DanhMuc = await _loaiRepository.GetAllAsync();

        // Thêm: lấy danh sách poster từ tintuc
        //var posters = await _context.tintuc
        //    .Where(t => t.tieude.ToLower() == "poster" && t.hinhanh != null)
        //    .ToListAsync();
        //ViewBag.Posters = posters;

        return View(sanphams);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return NotFound();
        }

        var sach = await _sachRepository.GetByIdAsync(id);
        if (sach == null)
        {
            return NotFound();
        }

        return View(sach);
    }

}
