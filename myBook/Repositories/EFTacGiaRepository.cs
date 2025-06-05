using Microsoft.EntityFrameworkCore;
using myBook.Models;
namespace myBook.Repositories
{
    public class EFTacGiaRepository : ITacGiaRepository
    {
        private readonly ApplicationDbContext _context;

        public EFTacGiaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TacGia>> GetAllAsync()
        {
            return await _context.TacGias.ToListAsync();
        }

        public async Task<TacGia> GetByIdAsync(string maTG)
        {
            return await _context.TacGias.FirstOrDefaultAsync(n => n.MaTG == maTG);
        }

        public async Task AddAsync(TacGia nhacungcap)
        {
            _context.TacGias.Add(nhacungcap);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TacGia nhacungcap)
        {
            _context.TacGias.Update(nhacungcap);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string maNCC)
        {
            var ncc = await _context.TacGias.FindAsync(maNCC);
            if (ncc != null)
            {
                _context.TacGias.Remove(ncc);
                await _context.SaveChangesAsync();
            }
        }
    }

}
