namespace WebOdevi.Models
{
    using Microsoft.AspNetCore.Identity;
    public class Kullanici : IdentityUser
    {
        public string AdSoyad { get; set; } // Kullanıcının adı ve soyadı
        public string Rol { get; set; } // Kullanıcının rolü (Admin veya Müşteri)
    }

}
