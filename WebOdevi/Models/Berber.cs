namespace WebOdevi.Models
{
    public class Berber
    {
        public int Id { get; set; }
        public string Ad { get; set; } // Berberin adı
        public string Adres { get; set; } // Berberin adresi
        public string CalismaSaatleri { get; set; } // Berberin çalışma saatleri
        public List<Calisan> Calisanlar { get; set; } = new List<Calisan>(); // Berberin çalışanları
    }

}
