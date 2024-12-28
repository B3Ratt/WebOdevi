using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebOdevi.Controllers
{
    public class HairstyleController : Controller
    {
        // GET metodu: Saç tavsiyesi almak için sayfayı yükler
        [HttpGet]
        public IActionResult ChangeHairstyle()
        {
            return View();
        }

        // POST metodu: Kullanıcının gönderdiği görseli API'ye gönderir
        [HttpPost]
        public async Task<IActionResult> ChangeHairstyle(IFormFile image)
        {
            if (image == null)
            {
                ModelState.AddModelError("", "Lütfen bir görsel seçin.");
                return View();
            }

            // API'ye istek gönderme
            var result = await ApplyHairstyleTransformation(image);
            if (result != null)
            {
                // İşlem başarılı ise sonucu görüntüle
                var resultData = ProcessApiResponse(result);
                return View("Success", resultData); // Success view'ini sonucu parametre olarak gönderiyoruz
            }
            else
            {
                ModelState.AddModelError("", "Saç değişikliği işleminde bir hata oluştu.");
                return View();
            }
        }

        // API'ye istek gönderen yardımcı metod
        private async Task<string> ApplyHairstyleTransformation(IFormFile image)
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://api.magicapi.dev/api/v1/magicapi/hair/hair"),
                Headers =
                {
                    { "x-magicapi-key", "cm58c7ary0001mm03tjeaqsiz" }, // API anahtarınızı kullanın
                },
                Content = new MultipartFormDataContent
                {
                    { new StringContent("both"), "editing_type" },
                    { new StringContent("blonde"), "color_description" },
                    { new StringContent("bob cut hairstyle"), "hairstyle_description" },
                    { new StreamContent(image.OpenReadStream()), "image", image.FileName }
                }
            };

            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody; // API'den dönen sonucu geri gönder
                }
                else
                {
                    return null; // API isteği başarısızsa null döndür
                }
            }
        }

        // API yanıtını işleyen metod (örneğin, gerekli bilgiyi ayıklama)
        private string ProcessApiResponse(string responseBody)
        {
            // API yanıtında dönen JSON formatındaki veriyi işleyin
            // Örnek olarak, JSON'u işleyip 'result' alanını alabilirsiniz.
            // Gerçek projede, JSON parsing işlemi yapılacak

            // Basit bir örnek:
            var resultStartIndex = responseBody.IndexOf("https://");
            var resultEndIndex = responseBody.IndexOf("\"", resultStartIndex);
            if (resultStartIndex != -1 && resultEndIndex != -1)
            {
                var imageUrl = responseBody.Substring(resultStartIndex, resultEndIndex - resultStartIndex);
                return imageUrl; // Görselin URL'sini döndür
            }

            return null; // Görsel URL'si bulunmazsa null döndür
        }
    }
}
