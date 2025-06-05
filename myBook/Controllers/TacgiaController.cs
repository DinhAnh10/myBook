using Microsoft.AspNetCore.Mvc;
using myBook.Models;
using myBook.Repositories;

namespace myBook.Controllers
{
    public class TacGiaController : Controller
    {
        private readonly ITacGiaRepository _tacGiaRepository;

        public TacGiaController(ITacGiaRepository tacGiaRepository)
        {
            _tacGiaRepository = tacGiaRepository;
        }

        // Hiển thị danh sách tác giả
        public async Task<IActionResult> Index()
        {
            var list = await _tacGiaRepository.GetAllAsync();
            return View(list);
        }

        // Form tạo mới
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TacGia tg)
        {
            if (ModelState.IsValid)
            {
                tg.MaTG = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
                await _tacGiaRepository.AddAsync(tg);
                return RedirectToAction(nameof(Index));
            }

            return View(tg);
        }

        // Form sửa
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var tg = await _tacGiaRepository.GetByIdAsync(id);
            if (tg == null) return NotFound();

            return View(tg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, TacGia model)
        {
            if (id != model.MaTG) return NotFound();

            if (ModelState.IsValid)
            {
                await _tacGiaRepository.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // Xác nhận xóa
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var tg = await _tacGiaRepository.GetByIdAsync(id);
            if (tg == null) return NotFound();

            return View(tg);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _tacGiaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
