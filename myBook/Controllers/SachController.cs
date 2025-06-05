using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myBook.Models;
using myBook.Repositories;

namespace myBook.Controllers
{
    public class SachController : Controller
    {
        private readonly ISachRepository _sanphamRepository;
        private readonly IChuongRepository _chuongRepository;
        private readonly IAnhRepository _anhRepository;
        private readonly ITheLoaiRepository _loaiRepository;
        private readonly ITacGiaRepository _nhacungcapRepository;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SachController(ISachRepository sanphamRepository, ITacGiaRepository tacGiaRepo, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ITheLoaiRepository loaiRepository, IChuongRepository chuongRepository, IAnhRepository anhRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _chuongRepository = chuongRepository;
            _sanphamRepository = sanphamRepository;
            _nhacungcapRepository = tacGiaRepo;
            _context = context;
            _anhRepository = anhRepository;
            _loaiRepository = loaiRepository;
        }

        public async Task<IActionResult> Index(int? minPrice, int? maxPrice, string? theloai, string? brand)
        {
            var sachs = await _sanphamRepository.GetAllAsync() ?? new List<Sach>();

            if (minPrice.HasValue && maxPrice.HasValue)
                sachs = sachs.Where(sp => sp.GiaBan >= minPrice && sp.GiaBan <= maxPrice).ToList();

            if (!string.IsNullOrEmpty(theloai))
                sachs = sachs.Where(sp => sp.MaTheLoai == theloai).ToList();

            if (!string.IsNullOrEmpty(brand))
                sachs = sachs.Where(sp => sp.MaTG == brand).ToList();

            ViewBag.DanhMuc = await _loaiRepository.GetAllAsync();
            ViewBag.Brands = await _nhacungcapRepository.GetAllAsync();

            return View(sachs);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var sach = await _sanphamRepository.GetByIdAsync(id);
            if (sach == null) return NotFound();

            return View(sach);
        }
        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_context.TheLoai.ToList(), "MaTheLoai", "TenTheLoai");
            ViewBag.MaTG = new SelectList(_context.TacGias.ToList(), "MaTG", "TenTG");
            ViewBag.MaChuong = new SelectList(_context.Chuongs.ToList(), "MaChuong", "TenChuong");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Sach sach, List<IFormFile> Images)
        {
            if (ModelState.IsValid)
            {
                // Lưu danh sách chương tạm thời, rồi xóa để tránh EF tự thêm chương
                var chuongs = sach.Chuongs?.ToList();
                sach.Chuongs = null;

                // Sinh mã sách mới
                sach.MaSach = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();

                // Lưu sach trước để có ID (hoặc MaSach nếu là key)
                await _sanphamRepository.AddAsync(sach);

                // Lưu chương thủ công với MaSach
                if (chuongs != null && chuongs.Any())
                {
                    foreach (var chuong in chuongs)
                    {
                        await _chuongRepository.AddAsync(new Chuong
                        {
                            MaSach = sach.MaSach,
                            TenChuong = chuong.TenChuong,
                            NoiDung = chuong.NoiDung,
                            SoThuTu = chuong.SoThuTu
                        });
                    }
                }

                // Xử lý ảnh như Upsert: lưu file vào wwwroot, tạo record ảnh
                if (Images != null && Images.Count > 0)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string bookPath = Path.Combine("images", "books", $"book-{sach.MaSach}");

                    string fullPath = Path.Combine(wwwRootPath, bookPath);
                    if (!Directory.Exists(fullPath))
                    {
                        Directory.CreateDirectory(fullPath);
                    }

                    foreach (var file in Images)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string filePath = Path.Combine(fullPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Lưu đường dẫn vào DB (dùng dấu \ hoặc / tùy theo chuẩn bạn dùng)
                        await _sanphamRepository.AddAnhAsync(new Anh
                        {
                            MaSach = sach.MaSach,
                            Url = Path.Combine(bookPath, fileName).Replace("\\", "/")
                        });
                    }
                }
                TempData["success"] = "successfully";
                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();
            return View(sach);
        }



        private async Task<List<string>> SaveImages(List<IFormFile> images)
        {
            var savedPaths = new List<string>();

            foreach (var image in images)
            {
                if (image != null && image.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    savedPaths.Add("/images/" + fileName);
                }
            }

            return savedPaths;
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var sach = await _sanphamRepository.GetByIdAsync(id);
            if (sach == null) return NotFound();

            // Lấy ảnh liên quan đến sách
            var anhs = await _sanphamRepository.GetAnhsBySachIdAsync(id); 

            sach.Anhs = anhs?.ToList();

            LoadDropdowns();
            return View(sach);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, Sach sach, List<IFormFile> Images)
        {
            if (id != sach.MaSach) return NotFound();

            // Xóa trường Chuongs để tránh EF tự động map và sửa
            sach.Chuongs = null;

            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật sách
                    await _sanphamRepository.UpdateAsync(sach);

                    // Xử lý ảnh mới (nếu có)
                    if (Images != null && Images.Count > 0)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string bookPath = Path.Combine("images", "books", $"book-{sach.MaSach}");
                        string fullPath = Path.Combine(wwwRootPath, bookPath);

                        if (!Directory.Exists(fullPath))
                        {
                            Directory.CreateDirectory(fullPath);
                        }

                        foreach (var file in Images)
                        {
                            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                            string filePath = Path.Combine(fullPath, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            await _sanphamRepository.AddAnhAsync(new Anh
                            {
                                MaSach = sach.MaSach,
                                Url = Path.Combine(bookPath, fileName).Replace("\\", "/")
                            });
                        }
                    }

                    TempData["success"] = "Cập nhật sách thành công!";
                    return RedirectToAction(nameof(Edit), new { id = sach.MaSach });
                }
                catch
                {
                    TempData["error"] = "Có lỗi xảy ra khi cập nhật sách.";
                    return RedirectToAction(nameof(Edit), new { id = sach.MaSach });
                }
            }

            LoadDropdowns();
            return View(sach);
        }

        public async Task<IActionResult> UpdateChuong(string id) // id = MaSach
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var sach = await _sanphamRepository.GetByIdAsync(id);
            if (sach == null) return NotFound();

            var chuongs = await _chuongRepository.GetByMaSachAsync(id);

            ViewBag.Sach = sach;
            return View(chuongs);
        }

        // Thêm chương mới
        [HttpPost]
        public async Task<IActionResult> AddChuong(Chuong chuong)
        {
            if (ModelState.IsValid)
            {
                
                var chuongs = await _chuongRepository.GetByMaSachAsync(chuong.MaSach);

                
                int maxSoThuTu = chuongs.Any() ? chuongs.Max(c => c.SoThuTu) : 0;
                chuong.SoThuTu = maxSoThuTu + 1;

                await _chuongRepository.AddAsync(chuong);
                TempData["success"] = "Thêm chương thành công";
                return RedirectToAction(nameof(UpdateChuong), new { id = chuong.MaSach });
            }

            
            var sach = await _sanphamRepository.GetByIdAsync(chuong.MaSach);
            var chuongsReload = await _chuongRepository.GetByMaSachAsync(chuong.MaSach);
            ViewBag.Sach = sach;
            return View("UpdateChuong", chuongsReload);
        }


        // Sửa chương inline
        [HttpPost]
        public async Task<IActionResult> EditChuong(Chuong chuong)
        {
            if (ModelState.IsValid)
            {
                await _chuongRepository.UpdateAsync(chuong);
                TempData["success"] = "Thể loại  created successfully";
                return RedirectToAction(nameof(UpdateChuong), new { id = chuong.MaSach });
            }

            // Nếu lỗi, tải lại trang
            var sach = await _sanphamRepository.GetByIdAsync(chuong.MaSach);
            var chuongs = await _chuongRepository.GetByMaSachAsync(chuong.MaSach);
            ViewBag.Sach = sach;
            return View("UpdateChuong", chuongs);
        }

        // Xoá chương
        [HttpPost]
        public async Task<IActionResult> DeleteChuong(int id) // id = MaChuong
        {
            if (id == null) return NotFound();

            var chuong = await _chuongRepository.GetByIdAsync(id);
            if (chuong == null) return NotFound();

            await _chuongRepository.DeleteAsync(id);
            TempData["success"] = "Thể loại  created successfully";
            return RedirectToAction(nameof(UpdateChuong), new { id = chuong.MaSach });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteImage(string url)
        {
            if (string.IsNullOrEmpty(url)) return BadRequest();
            var anh = await _anhRepository.GetByUrlAsync(url);
            try
            {
                // Xoá bản ghi DB
                await _anhRepository.DeleteByUrlAsync(url);

                // Xoá file vật lý
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, url.TrimStart('/'));
                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);

                TempData["success"] = "Đã xoá ảnh.";
            }
            catch
            {
                TempData["error"] = "Không thể xoá ảnh.";
            }

            // Điều hướng về lại Edit sách
            return RedirectToAction("Edit", new { id = anh.MaSach });
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var sach = await _sanphamRepository.GetByIdAsync(id);
            if (sach == null) return NotFound();

            return View(sach);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var anhs = await _sanphamRepository.GetAnhsBySachIdAsync(id);
            if (anhs != null && anhs.Any())
            {
                foreach (var anh in anhs)
                {
                    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, anh.Url.TrimStart('/'));
                    try
                    {
                        if (System.IO.File.Exists(fullPath))
                            System.IO.File.Delete(fullPath);
                    }
                    catch (Exception ex)
                    {
                        // TODO: Log lỗi
                    }
                }
            }

            var chuongs = await _chuongRepository.GetByMaSachAsync(id);
            if (chuongs != null && chuongs.Any())
            {
                foreach (var chuong in chuongs)
                {
                    await _chuongRepository.DeleteAsync(chuong.MaChuong);
                }
            }

            await _sanphamRepository.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


        private void LoadDropdowns()
        {
            ViewBag.Categories = new SelectList(_context.TheLoai.ToList(), "MaTheLoai", "TenTheLoai");
            ViewBag.MaTG = new SelectList(_context.TacGias.ToList(), "MaTG", "TenTG");
            ViewBag.MaChuong = new SelectList(_context.Chuongs.ToList(), "MaChuong", "TenChuong");
        }

    }
}
