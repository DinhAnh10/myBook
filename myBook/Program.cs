using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using myBook.Models;
using myBook.Repositories;

namespace myBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add DB context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 2. Add Identity
            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // 3. Add Razor & MVC
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // 4. Add Repositories
            builder.Services.AddScoped<ISachRepository, EFSachRepository>();
            builder.Services.AddScoped<ITheLoaiRepository, EFTheLoaiRepository>();
            builder.Services.AddScoped<IChuongRepository, EFChuongRepository>();
            builder.Services.AddScoped<ITacGiaRepository, EFTacGiaRepository>();
            builder.Services.AddScoped<IShoppingCartRepository, EFShoppingCartRepository>();
            builder.Services.AddScoped<IShoppingCartDetailRepository, EFShoppingCartDetailRepository>();
            builder.Services.AddScoped<IBookshelfRepository, EFBookshelfRepository>();
            builder.Services.AddScoped<IPaymentRepository, EFPaymentRepository>();
            builder.Services.AddScoped<IAnhRepository, EFAnhRepository>();

            var app = builder.Build();

            // 5. Middlewares
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); // ➕ BẮT BUỘC khi dùng Identity
            app.UseAuthorization();

            // 6. Routes
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages(); // dùng cho Identity UI

            app.Run();
        }
    }
}
