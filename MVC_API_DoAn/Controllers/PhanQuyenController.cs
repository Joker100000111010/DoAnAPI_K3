using Microsoft.AspNetCore.Mvc;
using MVC_API_DoAn.Models.DTO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC_API_DoAn.Controllers
{
    public class PhanQuyenController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PhanQuyenController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:7073/api/User/Login", model);

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
                var token = tokenResponse.JwtToken;
                Console.WriteLine($"Received access token: {token}"); // Ghi log để kiểm tra chuỗi token nhận được
                HttpContext.Session.SetString("AccessToken", token);
                return RedirectToAction("TrangChuNV", "NhanVien");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Đăng nhập không thành công. Vui lòng thử lại.");
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("https://localhost:7073/api/User/Register", model);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Đăng ký không thành công. Vui lòng thử lại.");
                return View(model);
            }
        }
    }
    public class TokenResponse
    {
        public string JwtToken { get; set; }
    }
}
