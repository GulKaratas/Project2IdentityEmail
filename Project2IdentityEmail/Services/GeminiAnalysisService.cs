using System.Text;
using System.Text.Json;
using Project2IdentityEmail.Entities;
using Project2IdentityEmail.ViewModels;

namespace Project2IdentityEmail.Services
{
    public class GeminiAnalysisService : IGeminiAnalysisService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GeminiAnalysisService> _logger;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public GeminiAnalysisService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<GeminiAnalysisService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<MessageAnalysisViewModel> AnalyzeMessagesAsync(
            List<Message> messages)
        {
            var apiKey = _configuration["Gemini:ApiKey"];
            var model = _configuration["Gemini:Model"] ?? "gemini-3.6-flash";

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return CreateFallback("Gemini API anahtarı bulunamadı.");
            }

            var messageText = string.Join("\n\n",
                messages.Select(x =>
                    $"Konu: {x.Subject}\n" +
                    $"Mesaj: {x.MessageDetail}"));

            var prompt = $"""
                Aşağıdaki e-posta mesajlarını analiz et.

                Sadece verilen mesajlara dayan.
                Kullanıcı hakkında mesajlarda bulunmayan bilgiler uydurma.

                Şunları belirle:
                - Mesajların kısa genel özeti
                - En çok öne çıkan konu
                - Mesaj trafiğinin yoğunluğu
                - Genel iletişim tonu

                Mesajlar:

                {messageText}
                """;

            var request = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = prompt
                            }
                        }
                    }
                },

                generationConfig = new
                {
                    responseMimeType = "application/json",

                    responseSchema = new
                    {
                        type = "object",

                        properties = new
                        {
                            summary = new
                            {
                                type = "string"
                            },

                            mainTopic = new
                            {
                                type = "string"
                            },

                            activity = new
                            {
                                type = "string"
                            },

                            tone = new
                            {
                                type = "string"
                            }
                        },

                        required = new[]
                        {
                            "summary",
                            "mainTopic",
                            "activity",
                            "tone"
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(request);

            using var httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent");

            httpRequest.Headers.Add("x-goog-api-key", apiKey);

            httpRequest.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            try
            {
                var response = await _httpClient.SendAsync(httpRequest);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning(
                        "Gemini API failed with {StatusCode}: {Body}",
                        (int)response.StatusCode,
                        responseJson);

                    return CreateFallback(
                        "Mesaj analizi şu anda alınamadı. Lütfen daha sonra tekrar deneyin.");
                }

                using var document = JsonDocument.Parse(responseJson);

                var resultText =
                    document.RootElement
                        .GetProperty("candidates")[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                var result =
                    JsonSerializer.Deserialize<MessageAnalysisViewModel>(
                        resultText ?? "{}",
                        JsonOptions);

                return result ?? CreateFallback("Analiz sonucu okunamadı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gemini analysis request failed.");
                return CreateFallback(
                    "Mesaj analizi şu anda alınamadı. Lütfen daha sonra tekrar deneyin.");
            }
        }

        private static MessageAnalysisViewModel CreateFallback(string summary)
        {
            return new MessageAnalysisViewModel
            {
                Summary = summary,
                MainTopic = "Analiz bekleniyor",
                Activity = "Belirsiz",
                Tone = "Belirsiz"
            };
        }
    }
}
