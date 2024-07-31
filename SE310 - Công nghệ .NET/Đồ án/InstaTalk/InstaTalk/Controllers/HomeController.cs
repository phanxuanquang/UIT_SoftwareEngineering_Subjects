using InstaTalk.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace InstaTalk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FriendHub(Guid? roomId)
        {
            var obj = new RoomViewModel();
            obj.JoinRoom = new JoinRoomModel();
            obj.CreateRoom = new CreateRoomModel();
            ViewBag.RoomId = roomId;
            return View(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CreateRoom(RoomViewModel obj)
        {
            /*if (ModelState.IsValid)
            {
                */
            using (var client = _httpClientFactory.CreateClient("API"))
            {
                var model = obj.CreateRoom;
                model.RoomName = "test123";
                var response = await client.PostAsJsonAsync("/api/Room/add-room", model);
                if (response.IsSuccessStatusCode)
                {
                    using (var content = response.Content)
                    {
                        var responseContent = await content.ReadFromJsonAsync<RoomInfo>();
                        HttpContext.Session.SetString("token", responseContent?.User?.Token ?? string.Empty);
                        HttpContext.Session.SetString("sessionRoom", JsonConvert.SerializeObject(responseContent));
                        if (responseContent?.Room?.RoomId != null)
                            return RedirectToAction("Meeting", "Room", new { id = responseContent?.Room?.RoomId });
                    }
                }
            }
            /*}*/
            return RedirectToAction("FriendHub");
        }

        public async Task<IActionResult> JoinRoom(RoomViewModel obj)
        {
            using (var client = _httpClientFactory.CreateClient("API"))
            {
                var model = obj.JoinRoom;
                var response = await client.PostAsJsonAsync("/api/Room/join-room", model);
                if (response.IsSuccessStatusCode)
                {
                    using (var content = response.Content)
                    {
                        var responseContent = await content.ReadFromJsonAsync<RoomInfo>();
                        HttpContext.Session.SetString("token", responseContent?.User?.Token ?? string.Empty);
                        HttpContext.Session.SetString("sessionRoom", JsonConvert.SerializeObject(responseContent));
                        return RedirectToAction("Meeting", "Room", new { id = responseContent?.Room?.RoomId });
                    }
                }
            }

            return RedirectToAction("FriendHub");
        }
    }
}