Cần cài .NET 8 SDK. Mở JwtDemo.csproj bằng Visual Studio, chọn profile JwtDemo, bấm Ctrl+F5.
Hoặc terminal: dotnet run  (trình duyệt tự mở http://localhost:5000)

Cấu trúc:
  Models/       AppUser, LoginRequest
  Data/         AppDb (DbContext + seed dữ liệu mẫu)
  Services/     JwtService (tạo JWT), PasswordHasher (hash mật khẩu)
  Controllers/  AuthController (đăng ký/đăng nhập), DemoController (API public/user/admin)
  wwwroot/      index.html (giao diện)
  appsettings.json  (cấu hình JWT + chuỗi kết nối DB)

Database SQLite: file app.db tự tạo lần chạy đầu (xóa file để reset).
Tài khoản mẫu: admin / 123456 và user / 123456
