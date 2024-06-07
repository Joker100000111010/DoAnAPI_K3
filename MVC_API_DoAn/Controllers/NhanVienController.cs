using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;
using System.Text;
using MVC_API_DoAn.Models.DTO;
using System.Net.Http;
using MVC_API_DoAn.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;

namespace MVC_API_DoAn.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NhanVienController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        private void AddAuthorizationHeader(HttpClient client)
        {
            var token = HttpContext.Session.GetString("AccessToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<IActionResult> TrangChuNV()
        {
            List<NhanViendto> response = new List<NhanViendto>();

            try
            {
                var client = _httpClientFactory.CreateClient();
                AddAuthorizationHeader(client);

                var httpResponseMess = await client.GetAsync("https://localhost:7073/api/NhanVien/get-all-author");
                httpResponseMess.EnsureSuccessStatusCode();
                response = await httpResponseMess.Content.ReadFromJsonAsync<List<NhanViendto>>();
            }
            catch (Exception ex)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };
                return View("Error", errorModel);
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult AddNhanVien()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNhanVien(addNhanVien addNhanVien)
        {
            if (!ModelState.IsValid)
            {
                return View(addNhanVien);
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                AddAuthorizationHeader(client);

                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7073/api/NhanVien/add-author"),
                    Content = new StringContent(JsonSerializer.Serialize(addNhanVien), Encoding.UTF8, "application/json")
                };

                var httpResponseMessage = await client.SendAsync(httpRequestMessage);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("TrangChuNV", "NhanVien");
                }
                else
                {
                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to add employee. Status code: {httpResponseMessage.StatusCode}, Reason: {httpResponseMessage.ReasonPhrase}, Response: {responseContent}");
                    ModelState.AddModelError(string.Empty, "Failed to add employee. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while adding the employee.");
            }

            return View(addNhanVien);
        }

        [HttpGet]
        public async Task<IActionResult> EditNhanVien(int id)
        {
            NhanViendto responseNhanVien = null;
            var client = _httpClientFactory.CreateClient();
            AddAuthorizationHeader(client);

            try
            {
                var httpResponseMess = await client.GetAsync($"https://localhost:7073/api/NhanVien/get-author-by-id/{id}");

                if (httpResponseMess.IsSuccessStatusCode)
                {
                    responseNhanVien = await httpResponseMess.Content.ReadFromJsonAsync<NhanViendto>();
                }
                else
                {
                    ViewBag.Error = "Employee not found or an error occurred while fetching data.";
                    return View("Error");
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                ViewBag.Error = $"Request error: {httpRequestException.Message}";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"An error occurred: {ex.Message}";
                return View("Error");
            }

            return View(responseNhanVien);
        }

        [HttpPost]
        public async Task<IActionResult> EditNhanVien(int id, [FromForm] addNhanVien nhanVien)
        {
            if (!ModelState.IsValid)
            {
                return View(nhanVien);  // Trả về view với dữ liệu hiện tại và thông báo lỗi
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                AddAuthorizationHeader(client);

                var httpRequestMess = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"https://localhost:7073/api/NhanVien/update-author-by-id/{id}"),
                    Content = new StringContent(JsonSerializer.Serialize(nhanVien), Encoding.UTF8, MediaTypeNames.Application.Json)
                };
                var httpResponseMess = await client.SendAsync(httpRequestMess);

                if (httpResponseMess.IsSuccessStatusCode)
                {
                    return RedirectToAction("TrangChuNV", "NhanVien");
                }
                else
                {
                    ViewBag.Error = "Có lỗi xảy ra khi cập nhật nhân viên.";
                    return View("Error");
                }
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Request error: {ex.Message}";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNhanVien(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid employee ID.");
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                AddAuthorizationHeader(client);

                var httpResponseMess = await client.DeleteAsync($"https://localhost:7073/api/NhanVien/delete-author-by-id/{id}");
                if (httpResponseMess.IsSuccessStatusCode)
                {
                    return RedirectToAction("TrangChuNV", "NhanVien");
                }
                else
                {
                    var responseContent = await httpResponseMess.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to delete employee. Status code: {httpResponseMess.StatusCode}, Reason: {httpResponseMess.ReasonPhrase}, Response: {responseContent}");
                    ModelState.AddModelError(string.Empty, "Failed to delete employee. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the employee.");
            }

            return RedirectToAction("TrangChuNV", "NhanVien");
        }
    }
}
