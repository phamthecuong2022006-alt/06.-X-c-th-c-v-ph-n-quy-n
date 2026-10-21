using JwtDemo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtDemo.Controllers;

[ApiController]
[Route("api/demo")]
public class DemoController : ControllerBase
{
    private readonly AppDb _db;
    public DemoController(AppDb db) => _db = db;

    // Ai cũng gọi được
    [HttpGet("public")]
    public IActionResult Public() => Ok("Ai cũng xem được");

    // Cần đăng nhập (User hoặc Admin)
    [Authorize]
    [HttpGet("user-area")]
    public IActionResult UserArea() => Ok($"Xin chào {User.Identity!.Name}");

    // Chỉ Admin: xem danh sách user trong DB
    [Authorize(Roles = "Admin")]
    [HttpGet("admin-area")]
    public IActionResult AdminArea() =>
        Ok(_db.Users.Select(u => new { u.Id, u.Username, u.Role }).ToList());
}
