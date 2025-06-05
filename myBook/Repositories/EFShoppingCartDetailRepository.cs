using Microsoft.EntityFrameworkCore;
using myBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace myBook.Repositories
{
    public class EFShoppingCartDetailRepository : IShoppingCartDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public EFShoppingCartDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCartDetail>> GetAllAsync()
        {
            return await _context.ShoppingCartDetails
                .Include(d => d.ShoppingCart)
                .Include(d => d.Sach)
                .ToListAsync();
        }

        public async Task<ShoppingCartDetail> GetByIdAsync(int id)
        {
            return await _context.ShoppingCartDetails
                .Include(d => d.ShoppingCart)
                .Include(d => d.Sach)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IEnumerable<ShoppingCartDetail>> GetByCartIdAsync(int cartId)
        {
            return await _context.ShoppingCartDetails
                .Where(d => d.ShoppingCartId == cartId)
                .Include(d => d.Sach)
                .ToListAsync();
        }

        public async Task AddAsync(ShoppingCartDetail detail)
        {
            _context.ShoppingCartDetails.Add(detail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShoppingCartDetail detail)
        {
            _context.ShoppingCartDetails.Update(detail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var detail = await _context.ShoppingCartDetails.FindAsync(id);
            if (detail != null)
            {
                _context.ShoppingCartDetails.Remove(detail);
                await _context.SaveChangesAsync();
            }
        }
    }
}
