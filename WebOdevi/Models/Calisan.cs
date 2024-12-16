namespace WebOdevi.Models
{
    public class Calisan
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; } // Çalışanın adı ve soyadı
        public string Uzmanlik { get; set; } // Çalışanın uzmanlık alanları (saç kesimi, saç boyama, vb.)
        public bool Musaitlik { get; set; } // Çalışanın müsaitlik durumu
        public int BerberId { get; set; } // Çalışanın hangi berbere ait olduğunu belirtir
        public Berber Berber { get; set; } // Çalışanın ait olduğu berber
    }

}
