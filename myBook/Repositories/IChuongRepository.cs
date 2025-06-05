using myBook.Models;

namespace myBook.Repositories
{
    public interface IChuongRepository
    {
        Task<IEnumerable<Chuong>> GetAllAsync();

        Task<Chuong> GetByIdAsync(int id);
        Task AddAsync(Chuong chuong);
        Task UpdateAsync(Chuong chuong);
        Task DeleteAsync(int id);
        Task<IEnumerable<Chuong>> GetByMaSachAsync(string maSach);
    }
}
