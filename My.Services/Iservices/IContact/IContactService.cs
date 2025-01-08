using Entities.Models;
using My.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.Iservices.IContact
{
    public interface IContactService
    {
        Task<Contact> GetContactByIdAsync(int id);
        Task<List<Contact>> GetContactAllAsync(bool tracking);
        Task<bool> AddContactAsync(ContactDto contactDto);
        Task<bool> UpdateContactAsync(int id, ContactDto contactDto);
        Task<bool> DeleteContactAsync(int id,bool tracking);
    }
}
