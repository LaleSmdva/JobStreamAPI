using JobStream.Test.Models;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace JobStream.Test.Controllers
{
	public class CompaniesController : Controller
	{
		public async Task<IActionResult> Index()
		{
			HttpResponseMessage responseMessage = null;

			string endpoint = "https://localhost:7101/api/companies";

			using (HttpClient client = new HttpClient())
			{
				responseMessage = await client.GetAsync(endpoint);
			}
			if (responseMessage!= null && responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
			{
				string content = await responseMessage.Content.ReadAsStringAsync();
				List<CompaniesDto> companies = JsonConvert.DeserializeObject<List<CompaniesDto>>(content);
				return View(companies);
			}
			return RedirectToAction("error", "home");
		}
	}
}
