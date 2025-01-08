using AutoMapper;
using My.Application.DTOs.Contact;
using My.Application.Exceptions.about;
using My.Application.Exceptions.Contact;
using My.Application.IRepositories;
using My.Services.Iservices;
using My.Services.Iservices.IContact;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.ServicesManager.Contact
{
    public class ContactManager : IContactService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public ContactManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> AddContactAsync(ContactDto contactDto)
        {
            if (contactDto == null)
            {
                string msg = "An error Oncurred in the list yo be added";
                _logger.LogWarning(msg);// ExceptionMiddlewareExtensions clas da yaotıgımız middleware de log alındıından burada artık ihtiyacımız yok ama örnek olarak log yazımı için burada bırakıyorum
                throw new KeyNotFoundException(msg);

            }
            //Manuel AMpping
            //var addContact = new Entities.Models.Contact
            //{
            //    Name = contactDto.Name,
            //    SurName = contactDto.SurName,
            //    Email = contactDto.Email,
            //    Message = contactDto.Message,
            //    Phone = contactDto.Phone,
            //    CreatedAt = contactDto.CreatedAt,
            //};

            //auto mapper
            var a =_mapper.Map<Entities.Models.Contact>(contactDto);


            bool result = await _manager.ContactWriteRepository.AddAsync(a);
            await _manager.ContactWriteRepository.SaveAsync();

            return result;
        }

        public async Task<bool> DeleteContactAsync(int id, bool tracking)
        {
            var contactDelete = await _manager.ContactReadRepository.GetByIdAsync(id,true);
            if (contactDelete == null)
            {
                throw new ContactNotfoundException(id);// id alıp entities de ki exception / aboutNotFoundException a göndererek ExceptionMiddleWareException daki loger da hatayı yazdırmak üzere gönderiyoruz
            }
               
            await _manager.ContactWriteRepository.RemoveAsync(contactDelete.Id);
            await _manager.ContactWriteRepository.SaveAsync();

            return true;
        }

        public async Task<List<Entities.Models.Contact>> GetContactAllAsync(bool tracking)
        {
            var concatc=await  _manager.ContactReadRepository.GetAllAsync(false);
            if (concatc == null)
            {
                string msg = "An error occurred while creating the about list";
                _logger.LogWarning($"{msg}");
                throw new KeyNotFoundException(msg);
            }
            return concatc.ToList(); 
        }

        public async Task<Entities.Models.Contact> GetContactByIdAsync(int id)
        {
            //belirtilen id ile ilgili 'Contact' nesnesini almak için repository'den Çagrı yapıyoruz
            var Contact = await _manager.ContactReadRepository.GetByIdAsync(id,false);
            //Eger ''Contact ' nesnesi bulunamassa , uygun bir özel durum fırlatıyoruz. 
            if (Contact == null)
            {
                throw new ContactNotfoundException(id);

            }
                //Bulunan 'Contact' nesnesini geri dönderiyoruz.
            return Contact;
        }

        public async Task<bool> UpdateContactAsync(int id, ContactDto contactDto)
        {
            //Contact is check
           var contactUpdate =await _manager.ContactReadRepository.GetByIdAsync(id,true);

            if (id != contactUpdate.Id)
                throw new ContactNotfoundException(id);
           
            if (contactUpdate == null)
                throw new ContactNotfoundException(id);

            //Mapping 

            //contactUpdate!.Name = contactDto.Name;
            //contactUpdate.Email = contactDto.Email;
            //contactUpdate.CreatedAt = contactDto.CreatedAt;
            //contactUpdate.Phone = contactDto.Phone;
            //contactUpdate.SurName = contactDto.SurName;
            //contactUpdate.Message = contactDto.Message;

            //outo Mapping (Mevcut nesne üzerinde güncelleme yapılıyor)
            _mapper.Map(contactDto,contactUpdate);
            // Güncelleme
            var result =  _manager.ContactWriteRepository.Update(contactUpdate);
            await _manager.ContactWriteRepository.SaveAsync();
            return result;
        }
    }
}
