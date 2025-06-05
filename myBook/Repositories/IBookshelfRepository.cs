using myBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace myBook.Repositories
{
    public interface IBookshelfRepository
    {
        Task<IEnumerable<Bookshelf>> GetAllAsync();
        Task<Bookshelf> GetByIdAsync(int id);
        Task AddAsync(Bookshelf bookshelf);
        Task UpdateAsync(Bookshelf bookshelf);
        Task DeleteAsync(int id);
    }
}
