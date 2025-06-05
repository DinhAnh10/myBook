using Microsoft.EntityFrameworkCore;
using myBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace myBook.Repositories
{
    public class EFPaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        public EFPaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.ShoppingCart)
                .ToListAsync();
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.ShoppingCart)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Payment>> GetByCartIdAsync(int cartId)
        {
            return await _context.Payments
                .Where(p => p.ShoppingCartId == cartId)
                .ToListAsync();
        }

        public async Task AddAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
