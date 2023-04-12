using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace HS8_BlogProject.UI.Controllers
{
	public static class ControllerRepository
	{
		static string baseUrl = "https://localhost:7148/api/";

		public static T ApiHttpGet<T>(string apiAction)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage getData = client.GetAsync(apiAction).Result;

				if (getData.IsSuccessStatusCode)
				{
					string results = getData.Content.ReadAsStringAsync().Result;
					var resultsConverted = JsonConvert.DeserializeObject<T>(results);

					return resultsConverted;
				}
			}
			return default(T);
		}

		public static void ApiHttpPost<T>(string apiAction, T model)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage getData = client.PostAsJsonAsync<T>(apiAction, model).Result;
			}
		}

		public static void ApiHttpPut<T>(string apiAction, T model)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage getData = client.PutAsJsonAsync<T>(apiAction, model).Result;
			} 
		}

		public static void ApiHttpDelete(string apiAction)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage getData = client.DeleteAsync(apiAction).Result;
			}
		}
	}
}
