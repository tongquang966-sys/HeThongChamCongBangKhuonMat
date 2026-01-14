using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApp.Services
{
    public class AiFaceService
    {
        private readonly HttpClient _http;

        public AiFaceService(HttpClient http)
        {
            _http = http;
        }

        public async Task<float[]> EncodeFaceAsync(IFormFile image)
        {
            using var content = new MultipartFormDataContent();

            var streamContent = new StreamContent(image.OpenReadStream());
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);

            content.Add(streamContent, "file", image.FileName);

            var response = await _http.PostAsync("/encode", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<float[]>(json)!;
        }
    }
}
