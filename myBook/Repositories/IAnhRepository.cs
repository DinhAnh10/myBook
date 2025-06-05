using myBook.Models;

namespace myBook.Repositories
{
    public interface IAnhRepository
    {
        Task<IEnumerable<Anh>> GetAllAsync();
        Task<IEnumerable<Anh>> GetByMaSachAsync(string maSach);
        Task<Anh?> GetByIdAsync(int id);
        Task<Anh?> GetByUrlAsync(string url);
        Task AddAsync(Anh anh);
        Task DeleteAsync(int id);
        Task DeleteByUrlAsync(string url);
    }
}
