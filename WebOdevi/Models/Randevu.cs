namespace WebOdevi.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; } // Randevu tarihi
        public string Islem { get; set; } // Yapılacak işlem (saç kesimi, saç boyama vb.)
        public decimal Ucret { get; set; } // İşlem ücreti
        public int CalisanId { get; set; } // Hangi çalışanla yapıldığı
        public Calisan Calisan { get; set; } // Çalışan bilgisi
    }

}
