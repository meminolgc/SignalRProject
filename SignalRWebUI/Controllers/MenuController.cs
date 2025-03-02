using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.BasketDtos;
using SignalRWebUI.Dtos.ProductDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class MenuController : Controller
	{

		private readonly IHttpClientFactory _httpClientFactory;

		public MenuController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index(int id)
		{
			ViewBag.v = id; // Burada MenuTableID değerini ayarlıyoruz.
			//TempData["x"] = id; // Eğer bunu kulllanıyorsanız.
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:44365/api/Product/ProductListWithCategory"); //Hata dönerse ProductListWithCategory kısmını sil 

			var jsonData = await responseMessage.Content.ReadAsStringAsync();
			var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
			return View(values);
		}

		[HttpPost]
		public async Task<IActionResult> AddBasket(int id, int menuTableId)
		{
			if (menuTableId == 0)
			{
				return BadRequest("MenuTableID 0 geliyor.");
			}

			CreateBasketDto createBasketDto = new CreateBasketDto
			{
				ProductID = id,
				MenuTableID = menuTableId // gelen MenuTableID burada kullanılıyor.
			};

			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createBasketDto);
			StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:44365/api/Baskets", stringContent);

			var client2 = _httpClientFactory.CreateClient();
			//var jsonData2 = JsonConvert.SerializeObject(createBasketDto);
			//StringContent stringContent2 = new StringContent(jsonData, Encoding.UTF8, "application/json");
			 await client2.GetAsync("https://localhost:44365/api/MenuTables/ChangeMenuTableStatusToFalse?id="+menuTableId);

			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return Json(createBasketDto);
		}
	}
}
