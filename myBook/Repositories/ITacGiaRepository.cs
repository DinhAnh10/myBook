using myBook.Models;

namespace myBook.Repositories
{
    public interface ITacGiaRepository
    {
        Task<IEnumerable<TacGia>> GetAllAsync();
        Task<TacGia> GetByIdAsync(string maTG);
        Task AddAsync(TacGia tacgia);
        Task UpdateAsync(TacGia tacgia);
        Task DeleteAsync(string maTG);
    }
}
