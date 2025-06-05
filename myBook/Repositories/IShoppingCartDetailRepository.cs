using myBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myBook.Repositories
{
    public interface IShoppingCartDetailRepository
    {
        Task<IEnumerable<ShoppingCartDetail>> GetAllAsync();
        Task<ShoppingCartDetail> GetByIdAsync(int id);
        Task AddAsync(ShoppingCartDetail detail);
        Task UpdateAsync(ShoppingCartDetail detail);
        Task DeleteAsync(int id);
        Task<IEnumerable<ShoppingCartDetail>> GetByCartIdAsync(int cartId);
    }
}
