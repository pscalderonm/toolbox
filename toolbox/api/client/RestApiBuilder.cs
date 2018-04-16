using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace com.pscalderonm.toolbox.api.client
{
	public class RestApiBuilder
	{
		private string uri;
		private string method;
		private string body;
		private string contentType;
		private int? timeout;

		const string JsonContentType = "application/json";

		static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings {
			Culture = CultureInfo.GetCultureInfo("en-US"),
			Formatting = Formatting.None			
		};

		public static RestApiBuilder Create()
		{
			return new RestApiBuilder();
		}

		private RestApiBuilder()
		{
			method = "GET";
			contentType = null;
		}

		public RestApiBuilder SetUri(string uri)
		{
			this.uri = uri;
			return this;
		}

		public RestApiBuilder IsPost()
		{
			method = "POST";
			return this;
		}

		public RestApiBuilder SetApiRestMethod(RestApiMethodTypes restApiMethod) {
			method = restApiMethod.ToString();
			return this;
		}

		public RestApiBuilder SetBody(string body)
		{			
			this.body = body;
			return this;
		}

		public RestApiBuilder SetJsonBody(object body)
		{
			contentType = JsonContentType;
			SetBody(JsonConvert.SerializeObject(body));
			return this;
		}

		public RestApiBuilder SetTimeout(int timeout)
		{
			this.timeout = timeout;
			return this;
		}

		public T Call<T>()
		{
			var webRequest = WebRequest.Create(uri);
			webRequest.Method = method;
			webRequest.ContentType = contentType ?? webRequest.ContentType;
			webRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
			webRequest.Timeout = timeout.HasValue ? timeout.Value : webRequest.Timeout;
			if (IsBodyAllowed(webRequest))
			{
				using (var writer = new StreamWriter(webRequest.GetRequestStream()))
				{
					writer.WriteLine(body);
				}
			}

			using (var webResponse = webRequest.GetResponse())
			{
				using (var reader = new StreamReader(webResponse.GetResponseStream()))
				{
					string json = reader.ReadToEnd();
					if (typeof(string) == typeof(T)) return (T)Convert.ChangeType(json, typeof(T));
					else return JsonConvert.DeserializeObject<T>(json, JsonSettings);
				}
			}
		}

		public async Task<T> CallAsync<T>()
		{
			var webRequest = WebRequest.Create(uri);
			webRequest.Method = method;
			webRequest.ContentType = contentType ?? webRequest.ContentType;
			webRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore); webRequest.Timeout = timeout.HasValue ? timeout.Value : webRequest.Timeout;
			if (IsBodyAllowed(webRequest))
			{
				using (var writer = new StreamWriter(await webRequest.GetRequestStreamAsync()))
				{
					await writer.WriteLineAsync(body);
				}				
			}

			using (var webResponse = await webRequest.GetResponseAsync())
			{				
				using (var reader = new StreamReader(webResponse.GetResponseStream()))
				{
					string json = reader.ReadToEnd();
					if (typeof(string) == typeof(T)) return (T)Convert.ChangeType(json, typeof(T));
					else return JsonConvert.DeserializeObject<T>(json, JsonSettings);
				} 
			}
		}		

		public async Task<string> CallAsync()
		{
			return await CallAsync<string>();
		}

		private static bool IsBodyAllowed(WebRequest req) {
			return req.Method == "POST" || req.Method == "PUT";
		}
	}
}