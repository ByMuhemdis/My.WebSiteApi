

using AutoMapper;
using Entities.Models;
using My.Application.DTOs.About;
using My.Application.Exceptions.about;
using My.Application.IRepositories;
using My.Services.Iservices;
using My.Services.Iservices.IAbout;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.ServicesManager.About
{
    public class AboutManager : IAboutService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public AboutManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> AddAboutAsync(AboutDto aboutDto)
        {
           
            if (aboutDto is null)
            {
                string msg = $"An error occurred in the list to be added.";
                _logger.LogError(msg);//ExceptionMiddlewareExtensions(csadece hatalı logların alındıgı kısım ) class da yaotıgımız middleware de log alındıından burada artık ihtiyacımız yok) ama burası hata logundan hariç butun kısmı kaydettiginden ve örnek olarak log yazımı için burada bırakıyorum
                throw new KeyNotFoundException(msg);
            }
            //Manule Mapping
            var aboutEntity = new Entities.Models.About
            {
                Bio = aboutDto.Bio,
                ProfilePictureUrl = aboutDto.ProfilePictureUrl,
                LinkeinUrl = aboutDto.LinkeinUrl,
                Phone = aboutDto.Phone
            };
            //  bool result = await _manager.AboutWriteRepository.AddAsync(aboutEntity);
            //*****
            //Auto MApping 

            var autoMapper= _mapper.Map<Entities.Models.About>(aboutDto);


            bool result = await _manager.AboutWriteRepository.AddAsync(autoMapper);
            await _manager.AboutWriteRepository.SaveAsync();
            _logger.LogInfo("About successfully added");
            return result;
        }

        public async Task<bool> DeleteAboutAsync(int id, bool tracking)
        {
            var aboutDelete = await _manager.AboutReadRepository.GetByIdAsync(id);
            if (aboutDelete == null)
            {
                string msg = $"No value found matching {id} .";
                _logger.LogInfo(msg);//ExceptionMiddlewareExtensions clas da yaotıgımız middleware de log alındıından burada artık ihtiyacımız yok ama örnek olarak log yazımı için burada bırakıyorum
                throw new AboutNotfoundException(id);// id alıp entities de ki exception / aboutNotFoundException a göndererek ExceptionMiddleWareException daki loger da hatayı yazdırmak üzere gönderiyoruz
            }


            await _manager.AboutWriteRepository.RemoveAsync(aboutDelete.Id);
            await _manager.AboutWriteRepository.SaveAsync();

            return true;

        }

        public async Task<List<AboutDto>> GetAboutAllAsync(bool tracking)
        {
            var abouts = await _manager.AboutReadRepository.GetAllAsync(false);
            if (abouts == null)
            {
                string msg = $"An error occurred while creating the about list";
                _logger.LogWarning(msg);//ExceptionMiddlewareExtensions class da yaotıgımız middleware de log alındıından burada artık ihtiyacımız yok ama örnek olarak log yazımı için burada bırakıyorum 
                throw new KeyNotFoundException(msg);

            }
            var a =_mapper.Map<List<AboutDto>>(abouts);//auto mapper ile aboutdaki verileri dto uzerine alarak listrliyoruz 
            return a;
        }

        public async Task<Entities.Models.About> GetAboutByIdAsync(int id)
        {
            // Belirtilen id ile ilgili 'About' nesnesini almak için repository'den çağrı yapıyoruz.
            var about = await _manager.AboutReadRepository.GetByIdAsync(id);

            // Eğer 'about' nesnesi bulunamazsa, uygun bir özel durum fırlatıyoruz.
            if (about == null)
            {
                string msg = $"The searched 'About' with ID {id} was not found.";
                _logger.LogWarning(msg);//ExceptionMiddlewareExtensions class da yaotıgımız middleware de log alındıından burada artık ihtiyacımız yok ama örnek olarak log yazımı için burada bırakıyorum 
                throw new AboutNotfoundException(id);//id alıp entities de ki exception / aboutNotFoundException a göndererek ExceptionMiddleWareException daki loger da hatayı yazdırmak üzere gönderiyoruz
            }
            _logger.LogInfo("about was successfully restored");
            // Bulunan 'about' nesnesini geri döndürüyoruz.
            return about;

        }

        public async Task<bool> UpdateAboutAsync(int id, AboutDto aboutDto)
        {
            //Check about
            var aboutUpdate = await _manager.AboutReadRepository.GetByIdAsync(id,true);
            if (id != aboutUpdate.Id)
            {
                string msg = "ID mismatch";
                _logger.LogInfo(msg);//ExceptionMiddlewareExtensions clas da yaotıgımız middleware de log alındıından burada artık ihtiyacımız yok ama örnek olarak log yazımı için burada bırakıyorum
                throw new AboutNotfoundException(id);// id alıp entities de ki exception / aboutNotFoundException a göndererek ExceptionMiddleWareException daki loger da hatayı yazdırmak üzere gönderiyoruz
            }


            if (aboutUpdate == null)
            {
                string msg = $"The searched 'About' with ID: {id} was not found.";
                _logger.LogInfo(msg);
                throw new AboutNotfoundException(id);// id alıp entities de ki exception / aboutNotFoundException a göndererek ExceptionMiddleWareException daki loger da hatayı yazdırmak üzere gönderiyoruz
            }
            //Manuel Mapping
            //aboutUpdate.Bio = aboutDto.Bio;
            //aboutUpdate.ProfilePictureUrl = aboutDto.ProfilePictureUrl;
            //aboutUpdate.LinkeinUrl = aboutDto.LinkedInUrl;
            //aboutUpdate.Phone = aboutDto.Phone;

            //AutoMapper

            _mapper.Map(aboutDto, aboutUpdate);

            var response = _manager.AboutWriteRepository.Update(aboutUpdate);
            await _manager.AboutWriteRepository.SaveAsync();

            return response;


        }
    }
}
