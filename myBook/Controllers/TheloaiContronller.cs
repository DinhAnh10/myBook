using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using myBook.Models;

namespace myBook.Controllers
{
    public class TheLoaiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TheLoaiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TheLoai
        public async Task<IActionResult> Index()
        {
            return View(await _context.TheLoai.ToListAsync());
        }

        // GET: TheLoai/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TheLoai/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TheLoai theLoai)
        {
            // Tự sinh mã nếu chưa có
            theLoai.MaTheLoai = "TL" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

            // Clear model validation nếu cần
            ModelState.Clear();
            TryValidateModel(theLoai);

            if (ModelState.IsValid)
            {
                _context.Add(theLoai);
                TempData["success"] = "Thể loại  created successfully";
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(theLoai);
        }




        // GET: TheLoai/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var theLoai = await _context.TheLoai.FindAsync(id);
            if (theLoai == null)
                return NotFound();

            return View(theLoai);
        }

        // POST: TheLoai/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaTheLoai,TenTheLoai,MoTa")] TheLoai theLoai)
        {
            if (id != theLoai.MaTheLoai)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theLoai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.TheLoai.Any(e => e.MaTheLoai == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(theLoai);
        }

        // GET: TheLoai/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var theLoai = await _context.TheLoai
                .FirstOrDefaultAsync(m => m.MaTheLoai == id);

            if (theLoai == null)
                return NotFound();

            return View(theLoai);
        }

        // POST: TheLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var theLoai = await _context.TheLoai.FindAsync(id);
            if (theLoai != null)
            {
                _context.TheLoai.Remove(theLoai);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
