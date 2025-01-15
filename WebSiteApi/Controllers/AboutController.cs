   using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My.Application.DTOs.About;
using My.Application.IRepositories;
using My.Application.Context;
using My.Application.Repositories.Abouts;
using My.Services.Iservices;
using My.Services.Iservices.IAbout;
using System.Diagnostics.Eventing.Reader;
using WebSiteApi.ActionFilter;



namespace WebSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(LogFilterAttrubute))]//,Order = 1 yazarsak pantezeönce og al medek order
    [ResponseCache(CacheProfileName = "5mins")]//2 caching yontemi bunu buraya eklemeden önce program.cs e gidip orada add.controller da  confing.CacheProfiles.Add("5mins", new CacheProfile() { Duration = 300 }); bunu ekliyoruz 
    public class AboutController : ControllerBase
    {
          
        private readonly IServiceManager _services;

        public AboutController(IServiceManager services)
        {
            _services = services;
        }

       
        [HttpGet("[action]")]
        [ResponseCache (Duration = 60)]//önbellege alındı ve her aynı istegi attıgında 60 sn gecmediyse önbellege alınan bilgiler geri dönecektir. bunun service kaydını My.Persistence daki service ragistration da yaptık.
        public async Task<IActionResult> GetAllAbout()
        {
            var about = await _services.AboutService.GetAboutAllAsync(false);
           
            return Ok(about.ToList());


        }
        [HttpGet("[action]")]
        [ServiceFilter(typeof(ValidationFilterModelStateAttrubute))]//if( !Modelstate.Isvalid ) oldugunda artık controllerda degil burada çalıştırıp kontrol saglayarak bir actionFilter kuralı yazdık.
        public async Task<IActionResult> GetOneAbout( int id)
        {
            var about = await _services.AboutService.GetAboutByIdAsync(id);
            return Ok(about);
        }
       
        [ServiceFilter(typeof(ValidationFilterModelStateAttrubute))]//if( !Modelstate.Isvalid ) oldugunda artık controllerda degil burada çalıştırıp kontrol saglayarak bir actionFilter kuralı yazdık.
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateAbout(AboutDto aboutsDto)
        {
           
            var aboutAdd = await _services.AboutService.AddAboutAsync(aboutsDto);
            return StatusCode(201, aboutAdd);

        }

        [HttpDelete("[action]")]
        [ServiceFilter(typeof(ValidationFilterModelStateAttrubute))]//if( !Modelstate.Isvalid ) oldugunda artık controllerda degil burada çalıştırıp kontrol saglayarak bir actionFilter kuralı yazdık.
        public async Task<IActionResult> DeleteOneAbout(int id)
        {
            var deleteAbout = await _services.AboutService.DeleteAboutAsync(id, true);
            return Ok(deleteAbout);
        }

        [HttpPut("[action]")]
        [ServiceFilter(typeof(ValidationFilterModelStateAttrubute))]//if( !Modelstate.Isvalid ) oldugunda artık controllerda degil burada çalıştırıp kontrol saglayarak bir actionFilter kuralı yazdık.
        public async Task<IActionResult> UpdateAbout(int id, AboutDto aboutDto)
        {
            //check about
            /// modelstateizvalid ve buna benzer kod tekrarlarından kurtulmak için ActonFilter yazıp bu kontrolu orada saglayacagız.
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var aboutUpdate = await _services.AboutService.UpdateAboutAsync(id, aboutDto);
            return Ok(aboutUpdate);
        }


    }


}
