﻿using AgentsMVC.Dto;
using AgentsMVC.Models;
using AgentsMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AgentsMVC.Controllers
{
	public class MissionsController(IHttpClientFactory clientFactory, Authentication auth) : Controller
	{
		private readonly string baseUrl = "https://localhost:7270/Missions";
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
				List<MissionDto>? missions = JsonSerializer.Deserialize<List<MissionDto>>(content,
					new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
				return View(missions);
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
				MissionVM? mission = JsonSerializer.Deserialize<MissionVM>(content,
					new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
				return View(mission);
			}
			return RedirectToAction("Index");
		}
	}
}
