using Microsoft.EntityFrameworkCore;
using myBook.Models;


namespace myBook.Repositories
{
    public class EFSachRepository : ISachRepository
    {
        private readonly ApplicationDbContext _context;
        public EFSachRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Sach>> GetAllAsync()
        {
            return await _context.Sachs
                .Include(s => s.TheLoai)

                .Include(s => s.TacGia)
                .Include(s => s.Anhs)
                .ToListAsync();
        }

        public async Task<Sach> GetByIdAsync(string maSach)
        {
            return await _context.Sachs
                .Include(s => s.TheLoai)
                .Include(s => s.TacGia)
                .Include(s => s.Anhs)
                .FirstOrDefaultAsync(s => s.MaSach == maSach);
        }

        public async Task AddAsync(Sach sach)
        {
            _context.Sachs.Add(sach);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sach sach)
        {
            _context.Sachs.Update(sach);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string maSach)
        {
            var sach = await _context.Sachs.FindAsync(maSach);
            if (sach != null)
            {
                _context.Sachs.Remove(sach);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddAnhAsync(Anh Image)
        {
            _context.Anhs.Add(Image);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Anh>> GetAnhsBySachIdAsync(string maSach)
        {
            return await _context.Anhs
                .Where(a => a.MaSach == maSach)
                .ToListAsync();
        }

    }

}
