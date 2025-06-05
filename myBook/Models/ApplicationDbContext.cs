using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace myBook.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Sach>? Sachs { get; set; }
        public DbSet<TheLoai>? TheLoai { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<TacGia>? TacGias { get; set; }
        public DbSet<Anh>? Anhs { get; set; }
        public DbSet<Chuong>? Chuongs { get; set; }
        public DbSet<Bookshelf>? Bookshelves { get; set; }
        public DbSet<ShoppingCart>? ShoppingCarts { get; set; }
        public DbSet<ShoppingCartDetail>? ShoppingCartDetails { get; set; }
        public DbSet<Payment>? Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        }
    
}
