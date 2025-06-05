using Microsoft.EntityFrameworkCore;
using myBook.Models;
namespace myBook.Repositories
{
    public class EFTheLoaiRepository : ITheLoaiRepository
    {
        private readonly ApplicationDbContext _context;

        public EFTheLoaiRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TheLoai>> GetAllAsync()
        {
            return await _context.TheLoai.ToListAsync();


        }

        public async Task<TheLoai> GetByIdAsync(string id)
        {
            return await _context.TheLoai.FindAsync(id);
            // lấy thông tin kèm theo category 

        }

        public async Task AddAsync(TheLoai product)
        {

            _context.TheLoai.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TheLoai product)
        {
            _context.TheLoai.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var product = await _context.TheLoai.FindAsync(id);
            _context.TheLoai.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
    
}
