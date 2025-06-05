using myBook.Models;

namespace myBook.Repositories
{
    public interface ITheLoaiRepository
    {
        Task<IEnumerable<TheLoai>> GetAllAsync();
        Task<TheLoai> GetByIdAsync(string id);
        Task AddAsync(TheLoai theloai);
        Task UpdateAsync(TheLoai theloai);
        Task DeleteAsync(string id);
    }
}
