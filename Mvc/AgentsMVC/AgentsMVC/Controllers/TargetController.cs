﻿using AgentsMVC.Dto;
using AgentsMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AgentsMVC.Controllers
{
    public class TargetController(IHttpClientFactory clientFactory, Authentication auth) : Controller
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
    }
}
