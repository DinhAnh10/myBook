using myBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myBook.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<IEnumerable<ShoppingCart>> GetAllAsync();
        Task<ShoppingCart> GetByIdAsync(int id);
        Task AddAsync(ShoppingCart cart);
        Task UpdateAsync(ShoppingCart cart);
        Task DeleteAsync(int id);
    }
}
