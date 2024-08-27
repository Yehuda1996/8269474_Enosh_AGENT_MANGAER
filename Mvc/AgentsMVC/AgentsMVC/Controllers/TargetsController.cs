using AgentsMVC.Dto;
using AgentsMVC.Models;
using AgentsMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AgentsMVC.Controllers
{
    public class TargetsController(IHttpClientFactory clientFactory, Authentication auth) : Controller
    {
        private readonly string baseUrl = "https://localhost:7270/Targets";
        public async Task<IActionResult> Index()
        {
            var httpClient = clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", auth.Token);

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<TargetDto>? agents = JsonSerializer.Deserialize<List<TargetDto>>(content,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return View(agents);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var httpClient = clientFactory.CreateClient();

			var httpContent = new StringContent(
	            JsonSerializer.Serialize(id),
	            Encoding.UTF8,
	            "application/json"
            );

            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/{id}");

            request.Content = httpContent;

            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                TargetVM? target = JsonSerializer.Deserialize<TargetVM>(content,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return View(target);
            }
            return RedirectToAction("Index");
		}
    }
}
