using Microsoft.EntityFrameworkCore;
using myBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myBook.Repositories
{
    public class EFBookshelfRepository : IBookshelfRepository
    {
        private readonly ApplicationDbContext _context;
        public EFBookshelfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bookshelf>> GetAllAsync()
        {
            return await _context.Bookshelves
                .Include(b => b.ApplicationUser)
                .Include(b => b.Sach)
                .ToListAsync();
        }

        public async Task<Bookshelf> GetByIdAsync(int id)
        {
            return await _context.Bookshelves
                .Include(b => b.ApplicationUser)
                .Include(b => b.Sach)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task AddAsync(Bookshelf bookshelf)
        {
            _context.Bookshelves.Add(bookshelf);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Bookshelf bookshelf)
        {
            _context.Bookshelves.Update(bookshelf);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bookshelf = await _context.Bookshelves.FindAsync(id);
            if (bookshelf != null)
            {
                _context.Bookshelves.Remove(bookshelf);
                await _context.SaveChangesAsync();
            }
        }
    }
}
