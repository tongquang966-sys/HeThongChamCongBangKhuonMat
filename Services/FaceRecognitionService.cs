using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApp.Services
{
    public class FaceRecognitionService
    {
        private readonly HttpClient _http;

        public FaceRecognitionService(HttpClient http)
        {
            _http = http;

            // Timeout đủ cho AI xử lý ảnh
            _http.Timeout = TimeSpan.FromSeconds(60);

            // Đảm bảo BaseAddress đã được cấu hình
            // Ví dụ trong Program.cs:
            // builder.Services.AddHttpClient<FaceRecognitionService>(c =>
            // {
            //     c.BaseAddress = new Uri("http://127.0.0.1:8000");
            // });
        }

        /// <summary>
        /// Gửi ảnh lên AI Server để nhận diện khuôn mặt
        /// </summary>
        public async Task<AIResult?> RecognizeAsync(byte[] imageBytes)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                var fileContent = new ByteArrayContent(imageBytes);
                fileContent.Headers.ContentType =
                    new MediaTypeHeaderValue("image/jpeg");

                content.Add(fileContent, "file", "capture.jpg");

                // Gọi API AI
                var response = await _http.PostAsync("/recognize", content);

                // Không kết nối được AI
                if (!response.IsSuccessStatusCode)
                {
                    return new AIResult
                    {
                        success = false,
                        message = "❌ Không kết nối được AI server"
                    };
                }

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<AIResult>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return result;
            }
            catch (TaskCanceledException)
            {
                return new AIResult
                {
                    success = false,
                    message = "❌ AI xử lý quá lâu (timeout)"
                };
            }
            catch (Exception ex)
            {
                return new AIResult
                {
                    success = false,
                    message = $"❌ Lỗi AI: {ex.Message}"
                };
            }
        }
    }

    /// <summary>
    /// DTO kết quả trả về từ AI Server
    /// </summary>
    public class AIResult
    {
        public bool success { get; set; }

        // employeeId là STRING (AI trả về string số)
        public string? employeeId { get; set; }

        // Độ tin cậy cosine similarity
        public double confidence { get; set; }

        // Thông báo từ AI
        public string? message { get; set; }
    }
}
