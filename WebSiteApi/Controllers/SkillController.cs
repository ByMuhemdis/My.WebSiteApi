using Entities.Models;
using Entities.Search;
using Entities.Sort;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using My.Application.DTOs.Skill;
using My.Application.IRepositories;
using My.Application.Context;
using My.Services.Iservices;



namespace WebSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IServiceManager _service;

        public SkillController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> GetSills()
        {

            var skillsList = await _service.SkillService.GetSkillAllAsync(false);
            return Ok(skillsList);
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> GetSortSills(SkillSortParameters skillSortParameters)
        {

            var skillsList = await _service.SkillService.GetSortSkillAsync(skillSortParameters,false);
            return Ok(skillsList);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateSills(SkillDto skillDto)
        {
            var response = await _service.SkillService.AddSkillAsync(skillDto);
            return Ok(response);
        }

        //Arama yapılacak Api
        [HttpPost("[action]")]
        public async Task<IActionResult> SkillSearch(SkillSearchParameters searchParameters)
        {
            var response = await _service.SkillService.GetSkillSearcParameterAllAsync(searchParameters,false);
            return Ok(response);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateSills(int id, SkillDto skillDto)
        {
            var response = await _service.SkillService.UpdateSkillAsync(id, skillDto);
            return StatusCode(201, response);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteSills(int id)
        {
            var response = await _service.SkillService.DeleteSkillAsync(id, true);
            return Ok(response);
        }

    }
}
