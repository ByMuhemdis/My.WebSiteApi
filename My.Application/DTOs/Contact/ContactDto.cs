using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.DTOs.Contact
{
    public class ContactDto
    {

        public string? Name { get; set; }
        public string? SurName { get; set; } 
        public string? Email { get; set; }  // İletişim e-posta adresi
        public string? Phone { get; set; }   // Telefon numarası
        public string? Message { get; set; }   // Adres
       
    }
}
