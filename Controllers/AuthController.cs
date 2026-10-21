using JwtDemo.Data;
using JwtDemo.Models;
using JwtDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDb _db;
    private readonly JwtService _jwt;

    public AuthController(AppDb db, JwtService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    [HttpPost("register")]
    public IActionResult Register(LoginRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest(new { message = "Thiếu username/password" });
        if (_db.Users.Any(u => u.Username == req.Username))
            return Conflict(new { message = "Username đã tồn tại" });

        _db.Users.Add(new AppUser
        {
            Username = req.Username,
            PasswordHash = PasswordHasher.Hash(req.Password),
            Role = "User"
        });
        _db.SaveChanges();
        return Ok(new { message = "Đăng ký thành công" });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest req)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == req.Username);
        if (user == null || !PasswordHasher.Verify(req.Password, user.PasswordHash))
            return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu" });

        return Ok(new { token = _jwt.CreateToken(user), role = user.Role });
    }
}
