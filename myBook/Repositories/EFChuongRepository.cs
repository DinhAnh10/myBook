using Microsoft.EntityFrameworkCore;

using myBook.Models;

namespace myBook.Repositories
{
    public class EFChuongRepository : IChuongRepository
    {
        private readonly ApplicationDbContext _context;

        public EFChuongRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Chuong>> GetAllAsync()
        {
            return await _context.Chuongs
                .Include(s => s.Sach) // Nếu muốn lấy thông tin sách liên quan
                .ToListAsync();
        }

        public async Task<Chuong> GetByIdAsync(int id)
        {
            return await _context.Chuongs
                .Include(c => c.Sach)
                .FirstOrDefaultAsync(c => c.MaChuong == id);
        }

        public async Task AddAsync(Chuong chuong)
        {
            _context.Chuongs.Add(chuong);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chuong chuong)
        {
            _context.Chuongs.Update(chuong);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var chuong = await _context.Chuongs.FindAsync(id);
            if (chuong != null)
            {
                _context.Chuongs.Remove(chuong);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Chuong>> GetByMaSachAsync(string maSach)
        {
            return await _context.Chuongs
                .Where(c => c.MaSach == maSach)
                .OrderBy(c => c.SoThuTu)
                .ToListAsync();
        }
        
    }
    
}
