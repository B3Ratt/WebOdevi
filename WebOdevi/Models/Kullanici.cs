namespace WebOdevi.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class Kullanici : IdentityUser
    {

        [Required]
        public string AdSoyad { get; set; } // Kullanıcının adı ve soyadı
    }

}
