using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.DTOs.About
{
   
    public class AboutDto
    {
        [Required]
        [MaxLength(650)]
        public string? Bio { get; set; }
        
        public string? ProfilePictureUrl { get; set; }
        [Required]
        public string? Phone { get; set; }
        public string? LinkeinUrl { get; set; }
    }
}
