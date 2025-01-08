namespace Entities.Models
{
    public class Skill:BaseEntity
    {
        public string? Name { get; set; }
        public string? Level { get; set; } // Örnek: "Başlangıç", "Orta", "İleri"
    }
}
