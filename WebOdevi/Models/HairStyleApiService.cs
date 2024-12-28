using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class HairstyleApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "cm58c7ary0001mm03tjeaqsiz"; // Buraya API anahtarınızı girin.

    public HairstyleApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Saç tipi, renk veya her ikisini de değiştiren metot.
    public async Task<string> ChangeHairstyleAsync(string imageUrl, string editingType = "hairstyle", string colorDescription = null, string hairstyleDescription = null)
    {
        var requestBody = new
        {
            image = imageUrl,
            editing_type = editingType,
            color_description = colorDescription,
            hairstyle_description = hairstyleDescription
        };

        var json = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("https://api.magicapi.dev/api/v1/magicapi/hair"),
            Headers =
            {
                { "x-magicapi-key", _apiKey },
                { "accept", "application/json" }
            },
            Content = content
        };

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody; // responseBody içinde request_id var.
    }

    // İşlem sonucunu almak için
    public async Task<string> GetTransformationResultAsync(string requestId)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.magicapi.dev/api/v1/magicapi/hair/predictions/{requestId}"),
            Headers =
            {
                { "x-magicapi-key", _apiKey },
                { "accept", "application/json" }
            }
        };

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        return responseBody; // responseBody içinde status ve result var.
    }
}
