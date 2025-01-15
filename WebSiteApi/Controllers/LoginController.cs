using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using My.Application.DTOs.Token;
using My.Application.DTOs.User;
using My.Application.Context;
using My.Services.Iservices;
using My.Services.Iservices.IUser;
using System.Linq.Dynamic.Core.Tokenizer;
using WebSiteApi.ActionFilter;


namespace WebSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IServiceManager _service;

        public LoginController(IServiceManager service)
        {

            _service = service;
        }

        [HttpPost("[action]")]
        [ServiceFilter(typeof(ValidationFilterModelStateAttrubute))]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!await _service.loginService.LoginUser(loginDto))
            {
                return Unauthorized();
            }

            var tokenDto =await _service.loginService.CreateToken(true);

            return Ok(tokenDto); 

        }

        [HttpPost("[action]")]
        [ServiceFilter(typeof (ValidationFilterModelStateAttrubute))]
        public async Task<IActionResult> Refresh([FromBody]TokenDto tokenDto)
        {
            var tokenDtoReturn = await _service.loginService.RefreshToken(tokenDto);

            return Ok(tokenDtoReturn);



        }
    }
}
