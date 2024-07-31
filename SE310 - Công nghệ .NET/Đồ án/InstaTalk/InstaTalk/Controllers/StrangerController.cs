using InstaTalk.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InstaTalk.Controllers
{
    public class StrangerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public StrangerController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public IActionResult Index()
        { 
            return View();
        }

        public IActionResult FindOut(StrangerModel obj)
        {
            if (obj.DisplayName == null)
            {
                return RedirectToAction("Index");
            }
            HttpContext.Session.SetString("ShareStranger", JsonConvert.SerializeObject(obj));
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> CallAddStranger(StrangerModel obj)
        {
            var shareModel = HttpContext.Session.GetString("ShareStranger");
            var json = JsonConvert.DeserializeObject<StrangerModel>(shareModel);
            if (shareModel == null)
            {

                return RedirectToAction("Index");
            }
            obj.DisplayName = json.DisplayName;
            obj.Gender = json.Gender;
            obj.Nationality = json.Nationality;
            obj.Age = json.Age;
            switch (obj.StrangerFilter?.MinAge)
            {
                case 1:
                    obj.StrangerFilter.MinAge = 15;
                    obj.StrangerFilter.MaxAge = 18;
                    break;
                case 2:
                    obj.StrangerFilter.MinAge = 18;
                    obj.StrangerFilter.MaxAge = 24;
                    break;
                case 3:
                    obj.StrangerFilter.MinAge = 24;
                    obj.StrangerFilter.MaxAge = 30;
                    break;
                case 4:
                    obj.StrangerFilter.MinAge = 30;
                    obj.StrangerFilter.MaxAge = 100;
                    break;
            }

            if (obj.StrangerFilter.FindGender.First() == "All")
            {
                obj.StrangerFilter.FindGender.Remove("All");
                obj.StrangerFilter.FindGender.Add("Male");
                obj.StrangerFilter.FindGender.Add("Female");
                obj.StrangerFilter.FindGender.Add("Gay");
                obj.StrangerFilter.FindGender.Add("Lesbian");
                obj.StrangerFilter.FindGender.Add("Binary");
            }

            using (var client = _httpClientFactory.CreateClient("API"))
            {
                var model = obj;
                model.RoomName = "test123";
                var response = await client.PostAsJsonAsync("/api/Stranger/add-stranger", model);
                if (response.IsSuccessStatusCode)
                {
                    using (var content = response.Content)
                    {
                        var responseContent = await content.ReadFromJsonAsync<RoomInfo>();
                        HttpContext.Session.SetString("token", responseContent?.User?.Token ?? string.Empty);
                        HttpContext.Session.SetString("sessionRoom", JsonConvert.SerializeObject(responseContent));
                        if (responseContent?.Room?.RoomId != null)
                            return RedirectToAction("Waiting");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Waiting()
        {
            var content = HttpContext.Session.GetString("sessionRoom");
            if (string.IsNullOrEmpty(content))
                return RedirectToAction("Index");

            var model = JsonConvert.DeserializeObject<RoomInfo>(content);

            ViewBag.API = new { API = _configuration.GetValue<string>("APIUrl") };

            return View(model);
        }

        public async Task<IActionResult> Matching(JoinStrangerModel obj)
        {
            if (obj.RoomId == Guid.Empty)
                return RedirectToAction("Index");
            using (var client = _httpClientFactory.CreateClient("API"))
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                var response = await client.PostAsJsonAsync("/api/Stranger/join-stranger", obj);
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
            ViewBag.Error = "Room not found";
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Meeting2Peer()
        {
            return View();
        }
    }
}
