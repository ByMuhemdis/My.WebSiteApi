using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime {  get; set; } //resfresh Token suresini belirleyecegiz
        public DateTime? CreateDate { get; set; } =DateTime.UtcNow.AddHours(3);
        public DateTime? UpdateDate { get; set; }=DateTime.UtcNow.AddHours(3);

    }
}
