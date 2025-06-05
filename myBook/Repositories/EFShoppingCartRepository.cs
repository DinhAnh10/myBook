using Microsoft.EntityFrameworkCore;
using myBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myBook.Repositories
{
    public class EFShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext _context;
        public EFShoppingCartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync()
        {
            return await _context.ShoppingCarts
                .Include(c => c.ApplicationUser) // nếu cần
                .ToListAsync();
        }

        public async Task<ShoppingCart> GetByIdAsync(int id)
        {
            return await _context.ShoppingCarts
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(ShoppingCart cart)
        {
            _context.ShoppingCarts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShoppingCart cart)
        {
            _context.ShoppingCarts.Update(cart);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cart = await _context.ShoppingCarts.FindAsync(id);
            if (cart != null)
            {
                _context.ShoppingCarts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }
    }
}
