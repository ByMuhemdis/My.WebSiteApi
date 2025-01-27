using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My.Application.DTOs.Contact;
using My.Application.IRepositories;
using My.Services.Iservices;



namespace WebSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
       
        private readonly IServiceManager _service;

        public ContactController(IServiceManager service)
        {
           
            _service = service;
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllContact()
        {
            var contact = await _service.ContactService.GetContactAllAsync(false);
            var response = contact.ToList();
            return Ok(response);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateContact(ContactDto contactDto)
        {
            var response = await _service.ContactService.AddContactAsync(contactDto);
            return StatusCode(201, response);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateContact(int id, ContactDto contactDto)
        {
            var contactUpdate = await _service.ContactService.UpdateContactAsync(id, contactDto);
            return Ok(contactUpdate);
        }

    }
}
