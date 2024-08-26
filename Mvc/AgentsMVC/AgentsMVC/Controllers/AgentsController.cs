using AgentsMVC.Dto;
using AgentsMVC.Models;
using AgentsMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AgentsMVC.Controllers
{
    public class AgentsController(IHttpClientFactory clientFactory, Authentication auth) : Controller
    {
        private readonly string baseUrl = "https://localhost:7270/Agents";

        [HttpGet]
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
                List<AgentDto>? agents = JsonSerializer.Deserialize<List<AgentDto>>(content,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
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
				AgentVM? agent = JsonSerializer.Deserialize<AgentVM>(content,
					new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
				return View(agent);
			}
			return RedirectToAction("Index");
		}
	}
}
