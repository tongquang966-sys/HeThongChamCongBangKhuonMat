using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        // ===== ADMIN CỨNG (CHỈ CÓ 1 – KHÔNG ĐĂNG KÝ NGOÀI) =====
        private const string ADMIN_USER = "admin";
        private const string ADMIN_PASS = "admin123";

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // ===================== LOGIN =====================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // ===== ADMIN LOGIN =====
            if (username == ADMIN_USER && password == ADMIN_PASS)
            {
                await SignIn(username, "Admin");

                return RedirectToAction(
                    "Index",
                    "Dashboard",
                    new { area = "Admin" }
                );
            }

            // ===== NHÂN VIÊN LOGIN =====
            var nv = await _context.NhanViens
                .FirstOrDefaultAsync(x => x.Email == username);

            if (nv == null)
            {
                ViewBag.Error = "Tài khoản không tồn tại";
                return View();
            }

            // 👉 ĐỒ ÁN: mật khẩu = mã nhân viên
            if (password != nv.Id.ToString())
            {
                ViewBag.Error = "Sai mật khẩu";
                return View();
            }

            await SignIn(nv.Email, "User");

            return RedirectToAction(
                "QuetMat",
                "ChamCong",
                new { area = "User" }
            );
        }

        // ===================== REGISTER =====================
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string HoTen, string Email, string Password)
        {
            // ❌ Không cho đăng ký admin
            if (Email == ADMIN_USER)
            {
                ViewBag.Error = "Không thể đăng ký tài khoản Admin";
                return View();
            }

            var exists = await _context.NhanViens
                .AnyAsync(x => x.Email == Email);

            if (exists)
            {
                ViewBag.Error = "Email đã tồn tại";
                return View();
            }

            var nv = new NhanVien
            {
                HoTen = HoTen,
                Email = Email
                // ⚠️ Không lưu password – dùng ID làm mật khẩu
            };

            _context.NhanViens.Add(nv);
            await _context.SaveChangesAsync();

            ViewBag.Success = $"Đăng ký thành công! Mật khẩu của bạn là: {nv.Id}";
            return View();
        }

        // ===================== LOGOUT =====================
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction("Login");
        }

        // ===================== SIGN IN HELPER =====================
        private async Task SignIn(string username, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );
        }
    }
}
