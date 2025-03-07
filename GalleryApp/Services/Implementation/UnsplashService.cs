using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace GalleryApp
{
    public class UnsplashService : IUnsplashService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.unsplash.com/";
        private const string AccessKey = "p-WbVTyzyJi4K56bvygXJxGlT3A04vFTEL-xqvKORKI";

        public UnsplashService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Client-ID", AccessKey);
        }

        public async Task<JArray> SearchPhotosAsync(string query)
        {
            var response = await _httpClient.GetAsync($"search/photos?query={query}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);
            return (JArray)json["results"];
        }

        public async Task<JArray> GetRandomPhotosAsync(int page, int per_page)
        {
            var response = await _httpClient.GetAsync($"/photos?page={page}&per_page={per_page}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JArray.Parse(content);
        }
    }
}
