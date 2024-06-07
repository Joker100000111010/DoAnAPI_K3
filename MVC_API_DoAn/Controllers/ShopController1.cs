using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;
using System.Text;
using MVC_API_DoAn.Models.DTO;
using System.Net.Http;
using MVC_API_DoAn.Models;
using System.Diagnostics;

namespace MVC_API_DoAn.Controllers
{
    public class ShopController1 : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ShopController1(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> TrangChu([FromQuery] string filteron = null, string filterQuery = null, string sortBy = null, bool isAscending = true)
        {
            List<ShopDTO> response = new List<ShopDTO>();
            try
            {
                // lấy dữ liệu books from API
                var client = httpClientFactory.CreateClient();
                var httpResponseMess = await client.GetAsync("https://localhost:7073/api/Shop/get-all-books? filterOn=" + filteron + "&filterQuery=" + filterQuery + "&sortBy=" + sortBy + "&isAscending=" + isAscending);
                httpResponseMess.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMess.Content.ReadFromJsonAsync<IEnumerable<ShopDTO>>());
            }
            catch (Exception ex)
            {
                return View("Error");
            }

            return View(response);
        }
        public async Task<IActionResult> MoTa(int id)
        {
            ShopDTO response = new ShopDTO();
            try
            {
                // lấy dữ liệu books from   
                var client = httpClientFactory.CreateClient();
                var httpResponseMess = await client.GetAsync("https://localhost:7073/api/Shop/get-book-by-id/" + id);
                httpResponseMess.EnsureSuccessStatusCode();
                var stringResponseBody = await httpResponseMess.Content.ReadAsStringAsync();
                response = await httpResponseMess.Content.ReadFromJsonAsync<ShopDTO>();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View(response);
        }
        [HttpGet]
        public IActionResult AddShop()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddShop(addshop addShop)
        {
            if (!ModelState.IsValid)
            {
                return View(addShop);
            }

            try
            {
                var client = httpClientFactory.CreateClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7073/api/Shop/add-book"),
                    Content = new StringContent(JsonSerializer.Serialize(addShop), Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var httpResponseMessage = await client.SendAsync(httpRequestMessage);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("TrangChu", "ShopController1");
                }
                else
                {
                    // Log thêm thông tin chi tiết cho việc gửi yêu cầu thất bại
                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to add product. Status code: {httpResponseMessage.StatusCode}, Reason: {httpResponseMessage.ReasonPhrase}, Response: {responseContent}");
                    ModelState.AddModelError(string.Empty, "Failed to add product. Please try again.");
                }
            }
            catch (Exception ex)
            {
                // Log thông tin chi tiết về ngoại lệ
                Console.WriteLine($"Exception occurred: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while adding the product.");
            }

            return View(addShop);
        }
        [HttpGet]
        public async Task<IActionResult> editShop(int id)
        {
            ShopDTO responseBook = null;
            var client = httpClientFactory.CreateClient();

            try
            {
                var httpResponseMess = await client.GetAsync($"https://localhost:7073/api/Shop/get-book-by-id/{id}");

                if (httpResponseMess.IsSuccessStatusCode)
                {
                    responseBook = await httpResponseMess.Content.ReadFromJsonAsync<ShopDTO>();
                }
                else
                {
                    ViewBag.Error = "Sản phẩm không tồn tại hoặc có lỗi trong quá trình lấy dữ liệu.";
                    return View("Error");
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                ViewBag.Error = $"Lỗi khi gửi yêu cầu: {httpRequestException.Message}";
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Đã xảy ra lỗi: {ex.Message}";
                return View("Error");
            }

            return View(responseBook);
        }

        [HttpPost]
        public async Task<IActionResult> editShop(int id, [FromForm] addshop shop)
        {
            try
            {
                // Kiểm tra dữ liệu trước khi gửi yêu cầu cập nhật
                if (ModelState.IsValid)
                {
                    var client = httpClientFactory.CreateClient();
                    var httpRequestMess = new HttpRequestMessage()
                    {
                        Method = HttpMethod.Put,
                        RequestUri = new Uri($"https://localhost:7073/api/Shop/update-book-by-id/{id}"),
                        Content = new StringContent(JsonSerializer.Serialize(shop), Encoding.UTF8, MediaTypeNames.Application.Json)
                    };
                    var httpResponseMess = await client.SendAsync(httpRequestMess);
                    httpResponseMess.EnsureSuccessStatusCode();

                    // Kiểm tra dữ liệu trả về từ yêu cầu cập nhật
                    var updatedShop = await httpResponseMess.Content.ReadFromJsonAsync<addshop>();
                    if (updatedShop != null)
                    {
                        return RedirectToAction("TrangChu", "ShopController1");
                    }
                    else
                    {
                        ViewBag.Error = "Không thể cập nhật sản phẩm.";
                        return View("Error");
                    }
                }   
                else
                {
                    // Dữ liệu không hợp lệ, quay lại biểu mẫu và hiển thị thông báo lỗi
                    return View(shop);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> delShop(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid product ID.");
            }
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMess = await client.DeleteAsync("https://localhost:7073/api/Shop/delete-book-by-id/" + id);
                httpResponseMess.EnsureSuccessStatusCode();
                return RedirectToAction("TrangChu", "ShopController1");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                ViewBag.Error = "An error occurred while deleting the product.";
            }
            return View("TrangChu");
        }
            public async Task<IActionResult> Search(string query)
            {
                List<ShopDTO> response = new List<ShopDTO>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMess = await client.GetAsync($"https://localhost:7073/api/Shop/search?query={query}");
                httpResponseMess.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMess.Content.ReadFromJsonAsync<IEnumerable<ShopDTO>>());
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


        }
    }
