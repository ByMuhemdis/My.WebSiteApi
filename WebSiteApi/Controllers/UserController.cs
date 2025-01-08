using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My.Application.DTOs.User;
using My.Application.IRepositories;
using My.Persistence.Context;
using My.Services.Iservices;
using WebSiteApi.ActionFilter;


namespace WebSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IServiceManager _service;

        public UserController(IServiceManager service)
        {
            
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userList = await _service.userService.GetUserAllAsync(false);
            return Ok(userList);
        }
        [HttpPost("[action]")]
        [ServiceFilter(typeof(ValidationFilterModelStateAttrubute))]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            
            var response = await _service.userService.AddUserAsync(userDto);
            if(!response.Succeeded)
            {
                foreach (var item in response.Errors)
                {
                    ModelState.TryAddModelError(item.Code, item.Description);
                }
                  
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUser(string id, UserDto userDto)
        {
            var response = await _service.userService.UpdateUserAsync(id, userDto);
            return StatusCode(201, response);

        }
       
    }
}
