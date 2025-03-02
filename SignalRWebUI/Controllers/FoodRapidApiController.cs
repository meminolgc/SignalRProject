using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.RapidApiDtos;

namespace SignalRWebUI.Controllers
{
	public class FoodRapidApiController : Controller
	{
		public async Task<IActionResult> Index()
		{
			var client = new HttpClient();
			var request = new HttpRequestMessage
			{
				Method = HttpMethod.Get,
				RequestUri = new Uri("https://tasty.p.rapidapi.com/recipes/list?from=0&size=20&tags=under_30_minutes"),
				Headers =
	{
		{ "x-rapidapi-key", "3283d06647mshd151ddd45740f93p1f96dfjsn293e6be9b8e0" },
		{ "x-rapidapi-host", "tasty.p.rapidapi.com" },
	},
			};
			using (var response = await client.SendAsync(request))
			{
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				//var values = JsonConvert.DeserializeObject<List<ResultTastyApi>>(body);

				var root = JsonConvert.DeserializeObject<RootTastyApi>(body);
				var values = root.Results;
				return View(values.ToList());
			}
		}
	}
}
