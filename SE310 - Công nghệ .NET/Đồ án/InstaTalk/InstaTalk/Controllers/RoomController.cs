using InstaTalk.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace InstaTalk.Controllers
{
    public class RoomController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public RoomController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index(int roomId)
        {
            var content = HttpContext.Session.GetString("sessionRoom");
            if (string.IsNullOrEmpty(content))
                return RedirectToAction("Index", "Home");

            var model = JsonConvert.DeserializeObject<RoomInfo>(content);

            ViewBag.API = _configuration.GetValue<string>("APIUrl");

            return View(model);
        }

        public IActionResult Meeting(Guid? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            var content = HttpContext.Session.GetString("sessionRoom");
            if (string.IsNullOrEmpty(content))
                return RedirectToAction("FriendHub", "Home", new { roomId = id});

            var model = JsonConvert.DeserializeObject<RoomInfo>(content);

            ViewBag.API = new { API = _configuration.GetValue<string>("APIUrl") };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRoomSercurityCode([FromBody] EditRoomModel editRoomModel)
        {
            using (HttpClient client = _httpClientFactory.CreateClient("API"))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var response = await client.PutAsJsonAsync("api/Room", editRoomModel);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { message = "OK" });
                }
                else
                {
                    Console.WriteLine("Error change sercurity of room");
                    return Json(new { message = "Error" });
                }
            }
        }
    }
}
