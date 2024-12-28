namespace WebOdevi.Models
{
    public class Randevu
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; } // Randevu tarihi
        public string Islem { get; set; } // Yapılacak işlem (saç kesimi, saç boyama vb.)
        public decimal Ucret { get; set; } // İşlem ücreti
        public int BerberId { get; set; } // Hangi çalışanla yapıldığı
        public Berber Berber { get; set; } // Çalışan bilgisi
        public int KullaniciId { get; set; } // Randevuyu alan kullanıcı
        public Kullanici Kullanici { get; set; } // Kullanıcı bilgisi
        public TimeSpan BaslangicSaati { get; set; } // Randevu başlangıç saati
        public TimeSpan BitisSaati { get; set; } // Randevu bitiş saati
        public string Durum { get; set; } // Randevu durumu (Onay Bekliyor, Onaylandı, İptal Edildi)
    }
}