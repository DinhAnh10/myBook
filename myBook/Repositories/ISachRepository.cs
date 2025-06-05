using myBook.Models;

namespace myBook.Repositories
{
    public interface ISachRepository
    {
        Task<IEnumerable<Sach>> GetAllAsync();
        Task<Sach> GetByIdAsync(string maSach);
        Task AddAsync(Sach sach);
        Task UpdateAsync(Sach sach);
        Task DeleteAsync(string maSach);
        Task AddAnhAsync(Anh Image);
        Task<IEnumerable<Anh>> GetAnhsBySachIdAsync(string maSach);
    }
}
