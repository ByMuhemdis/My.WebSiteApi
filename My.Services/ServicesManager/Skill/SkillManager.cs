using Entities.Models;
using Entities.Search;
using Entities.Sort;
using My.Applicatio.Exceptions.Skill;
using My.Application.DTOs.Skill;
using My.Application.IRepositories;
using My.Services.Iservices;
using My.Services.Iservices.IProject;
using My.Services.Iservices.ISkill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.ServicesManager.Skill
{
    public class SkillManager : ISkillService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public SkillManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public async Task<bool> AddSkillAsync(SkillDto skillDto)
        {
            if (skillDto is null)
            {
                string msg = "An error occured in the list to bu added."; 
                _logger.LogWarning(msg);
                throw new KeyNotFoundException(msg);
            }
            var skill = new Entities.Models.Skill
            {
                Level = skillDto.Level,
                Name = skillDto.Name,
            };

            var result=await _manager.SkillWriteRepository.AddAsync(skill);
            await _manager.SkillWriteRepository.SaveAsync();
            return result;
        }

        public async Task<bool> DeleteSkillAsync(int id, bool tracking)
        {
            var deleteSkill= await _manager.SkillReadRepository.GetByIdAsync(id);
            if(deleteSkill is null)
            {
                throw new SkillNotfoundException(id);
            }
                
            await _manager.SkillWriteRepository.RemoveAsync(deleteSkill.Id);
            await _manager.SkillWriteRepository.SaveAsync();
            return true;
        }

        public async Task<List<Entities.Models.Skill>> GetSkillAllAsync(bool tracking)
        {
            var skill= await _manager.SkillReadRepository.GetAllAsync(false);
            if (skill is null)
                throw new KeyNotFoundException("An error occured while creating the Skill list");
            return skill.ToList();
        }

        public async Task<Entities.Models.Skill> GetSkillByIdAsync(int id)
        {
            var skill = await _manager.SkillReadRepository.GetByIdAsync(id, false);
            if (skill is null)
                throw new SkillNotfoundException(id);
            return skill;

        }

        public async Task<List<Entities.Models.Skill>> GetSkillSearcParameterAllAsync(SkillSearchParameters skillSearchParameters, bool tracking)
        {
            var skilSearch =await _manager.SkillReadRepository.GetSearchParameter(skillSearchParameters, false);
            return skilSearch.ToList();
        }

        public async Task<List<Entities.Models.Skill>> GetSortSkillAsync(SkillSortParameters skillSortParameters, bool tracking)
        {
            var skillSort =await _manager.SkillReadRepository.GetSortAllAsync(skillSortParameters, false);
            return skillSort.ToList();
        }

        public async Task<bool> UpdateSkillAsync(int id, SkillDto skillDto)
        {
            var updateSkill = await _manager.SkillReadRepository.GetByIdAsync(id,true);
            if (updateSkill is null)
                throw new SkillNotfoundException(id);
            //Mapping
            updateSkill.Level = skillDto.Level;
            updateSkill.Name = skillDto.Name;

            var result = _manager.SkillWriteRepository.Update(updateSkill);
            await _manager.SkillWriteRepository.SaveAsync();

            return result; 

        }
    }
}
