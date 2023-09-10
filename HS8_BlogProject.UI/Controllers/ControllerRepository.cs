using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using NuGet.Common;
using System.Net.Http.Headers;

namespace HS8_BlogProject.UI.Controllers
{
	public static class ControllerRepository
	{
		static string baseUrl = "https://localhost:7148/api/";

		public static HttpResponseMessage ApiHttpGet<T>(string apiAction, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				client.DefaultRequestHeaders.Authorization = token is not null ? new AuthenticationHeaderValue("Bearer", token) : null;

				return client.GetAsync(apiAction).Result;
			}
		}

		public static HttpResponseMessage ApiHttpPost<T>(string apiAction, T model, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = token is not null ? new AuthenticationHeaderValue("Bearer", token) : null;

                return client.PostAsJsonAsync<T>(apiAction, model).Result;
			}
		}

		public static HttpResponseMessage ApiHttpPut<T>(string apiAction, T model, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = token is not null ? new AuthenticationHeaderValue("Bearer", token) : null;

                return client.PutAsJsonAsync<T>(apiAction, model).Result;
            } 
		}

		public static HttpResponseMessage ApiHttpDelete(string apiAction, string token)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(baseUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = token is not null ? new AuthenticationHeaderValue("Bearer", token) : null;

                return client.DeleteAsync(apiAction).Result;
            }
		}
	}
}
