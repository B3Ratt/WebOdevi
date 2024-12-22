namespace WebOdevi.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class Kullanici : IdentityUser
    {

        [Required]
        public string AdSoyad { get; set; } // Kullanıcının adı ve soyadı
        public string Rol { get; set; } // Kullanıcının rolü (Admin veya Müşteri)
    }

}
