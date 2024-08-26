using AgentsMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AgentsMVC.Controllers
{
    public class HomeController(IHttpClientFactory clientFactory, Authentication auth) : Controller
    {

        private readonly string loginUrl = "https://localhost:7270/api/Login/auth";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string name)
        {
            var httpClient = clientFactory.CreateClient();

            var httpContent = new StringContent(
                JsonSerializer.Serialize(new {name}),
                Encoding.UTF8,
                "application/json"
            );

            var request = new HttpRequestMessage(HttpMethod.Post, loginUrl);

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", auth.Token);

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode) 
            {
                var content = await response.Content.ReadAsStringAsync();
                auth.Token = content;
                return RedirectToAction("Index");
            }
            return View("AuthError");
        }
    }
}
