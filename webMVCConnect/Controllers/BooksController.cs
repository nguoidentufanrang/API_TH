using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using webMVCConnect.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Mime;
using System.Text.Json;
using System.Text;

namespace webMVCConnect.Controllers
{
    public class BooksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string filterOn = null, string filterQuery = null, string sortBy = null, bool isAscending = true)
        {
            List<BookDTO> response = new List<BookDTO>();
            try
            {
                // Tạo HttpClient từ IHttpClientFactory
                var client = _httpClientFactory.CreateClient();
                // Xây dựng URL với các tham số
                var url = $"https://localhost:7245/api/Books/get-all-books?filterOn={filterOn}&filterQuery={filterQuery}&sortBy={sortBy}&isAscending={isAscending}";
                // Gửi yêu cầu GET tới API
                var httpResponse = await client.GetAsync(url);
                // Đảm bảo yêu cầu thành công
                httpResponse.EnsureSuccessStatusCode();
                // Đọc và chuyển đổi nội dung từ phản hồi thành danh sách BookDTO
                response.AddRange(await httpResponse.Content.ReadFromJsonAsync<IEnumerable<BookDTO>>());
            }
            catch (Exception ex)
            {
                // Trả về trang lỗi nếu có lỗi xảy ra
                return View("Error");
            }
            // Trả về view với dữ liệu sách
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> addBook(AddBookDTO addBookDTO)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpRequestMess = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7245/api/Books/add-book"),
                    Content = new StringContent(JsonSerializer.Serialize(addBookDTO), Encoding.UTF8, MediaTypeNames.Application.Json)
                };

                var httpResponseMess = await client.SendAsync(httpRequestMess);
                httpResponseMess.EnsureSuccessStatusCode();
                var response = await httpResponseMess.Content.ReadFromJsonAsync<AddBookDTO>();

                if (response != null)
                {
                    return RedirectToAction("Index", "Books");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View();
        }
        public async Task<IActionResult> listBook(int id)
        {
            BookDTO response = new BookDTO();
            try
            {
                // Tạo client từ factory
                var client = _httpClientFactory.CreateClient();

                // Gửi request đến API
                var httpResponseMess = await client.GetAsync($"https://localhost:7245/api/Books/get-book-by-id/{id}");

                // Kiểm tra trạng thái phản hồi
                httpResponseMess.EnsureSuccessStatusCode();

                // Đọc phản hồi từ nội dung JSON
                response = await httpResponseMess.Content.ReadFromJsonAsync<BookDTO>();
            }
            catch (Exception ex)
            {
                // Lưu lỗi vào ViewBag
                ViewBag.Error = ex.Message;
            }

            // Trả về view với dữ liệu sách
            return View(response);
        }
        public async Task<IActionResult> editBook(int id)
        {
            BookDTO responseBook = new BookDTO();
            var client = _httpClientFactory.CreateClient();
            var httpResponseMess = await client.GetAsync("https://localhost:7245/api/Books/get-book-by-id/" + id);
            httpResponseMess.EnsureSuccessStatusCode();
            responseBook = await httpResponseMess.Content.ReadFromJsonAsync<BookDTO>();
            ViewBag.Book = responseBook;
            List<authorDTO> responseAu = new List<authorDTO>();
            var httpResponseAu = await client.GetAsync("https://localhost:7245/api/Authors/get-all-author");
            httpResponseAu.EnsureSuccessStatusCode();
            responseAu.AddRange(await httpResponseAu.Content.ReadFromJsonAsync<IEnumerable<authorDTO>>());
            ViewBag.listAuthor = responseAu;
            List<publisherDTO> responsePu = new List<publisherDTO>();
            var httpResponsePu = await client.GetAsync("https://localhost:7245/api/Publisher/get-all-publisher");
            httpResponsePu.EnsureSuccessStatusCode();
            responsePu.AddRange(await httpResponsePu.Content.ReadFromJsonAsync<IEnumerable<publisherDTO>>());
            ViewBag.listPublisher = responsePu;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> editBook([FromRoute] int id, editDTO bookDTO)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpRequestMess = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("https://localhost:7245/api/Books/update-book-by-id/" + id),
                    Content = new StringContent(JsonSerializer.Serialize(bookDTO), Encoding.UTF8, MediaTypeNames.Application.Json)
                };
                var httpResponseMess = await client.SendAsync(httpRequestMess);
                httpResponseMess.EnsureSuccessStatusCode();
                var response = await httpResponseMess.Content.ReadFromJsonAsync<AddBookDTO>();
                if (response != null)
                {
                }
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> delBook([FromRoute] int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMess = await client.DeleteAsync("http://localhost:7245/api/Books/delete-book-by-id/" + id);
                httpResponseMess.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "Books");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View("Index");
        }


    }
}
