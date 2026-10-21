using JwtDemo.Models;
using JwtDemo.Services;
using Microsoft.EntityFrameworkCore;

namespace JwtDemo.Data;

public class AppDb : DbContext
{
    public AppDb(DbContextOptions<AppDb> options) : base(options) { }

    public DbSet<AppUser> Users => Set<AppUser>();

    // Tạo DB và thêm 2 tài khoản mẫu nếu chưa có
    public static void Seed(AppDb db)
    {
        db.Database.EnsureCreated();
        if (db.Users.Any()) return;

        db.Users.Add(new AppUser { Username = "admin", PasswordHash = PasswordHasher.Hash("123456"), Role = "Admin" });
        db.Users.Add(new AppUser { Username = "user", PasswordHash = PasswordHasher.Hash("123456"), Role = "User" });
        db.SaveChanges();
    }
}
