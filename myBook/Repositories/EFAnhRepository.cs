using Microsoft.EntityFrameworkCore;
using myBook.Models;

namespace myBook.Repositories
{
    public class EFAnhRepository : IAnhRepository
    {
        private readonly ApplicationDbContext _context;

        public EFAnhRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Anh>> GetAllAsync()
        {
            return await _context.Anhs.ToListAsync();
        }

        public async Task<IEnumerable<Anh>> GetByMaSachAsync(string maSach)
        {
            return await _context.Anhs
                .Where(a => a.MaSach == maSach)
                .ToListAsync();
        }

        public async Task<Anh?> GetByIdAsync(int id)
        {
            return await _context.Anhs.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Anh?> GetByUrlAsync(string url)
        {
            return await _context.Anhs.FirstOrDefaultAsync(a => a.Url == url);
        }

        public async Task AddAsync(Anh anh)
        {
            _context.Anhs.Add(anh);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var anh = await _context.Anhs.FindAsync(id);
            if (anh != null)
            {
                _context.Anhs.Remove(anh);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteByUrlAsync(string url)
        {
            var anh = await GetByUrlAsync(url);
            if (anh != null)
            {
                _context.Anhs.Remove(anh);
                await _context.SaveChangesAsync();
            }
        }
    }
}
