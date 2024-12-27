namespace WebOdevi.Models
{
    public class Berber
    {
        public int Id { get; set; }
        public string Ad { get; set; } // Çalışanın adı ve soyadı
        public string Soyad { get; set; } // Çalışanın adı ve soyadı
        public string Uzmanlik { get; set; } // Çalışanın uzmanlık alanları (saç kesimi, saç boyama, vb.)
        public bool Musaitlik { get; set; } // Çalışanın müsaitlik durumu
    }

}