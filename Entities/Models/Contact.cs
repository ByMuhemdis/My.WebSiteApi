using System.Diagnostics;

namespace Entities.Models
{
    public class Contact:BaseEntity //burası müşterinin mesaj bırakacagı yada iletişime gececegi bölüm 
    {
        public string? Name {  get; set; } 
        public string? SurName { get; set; } 
        public string? Email { get; set; }  // İletişim e-posta adresi
        public string? Phone { get; set; }  // Telefon numarası
        public string? Message { get; set; } // Adres
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow.AddHours(3);

    }
}
