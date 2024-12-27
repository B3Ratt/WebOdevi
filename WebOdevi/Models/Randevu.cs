namespace WebOdevi.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; } // Randevu tarihi
        public string Islem { get; set; } // Yapılacak işlem (saç kesimi, saç boyama vb.)
        public decimal Ucret { get; set; } // İşlem ücreti
        public int BerberId { get; set; } // Hangi çalışanla yapıldığı
        public Berber berber{ get; set; } // Çalışan bilgisi
    }

}
