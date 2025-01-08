using My.Application.IRepositories.Abouts;
using My.Application.IRepositories.Contacts;
using My.Application.IRepositories.Projects;
using My.Application.IRepositories.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.IRepositories
{
    public interface IRepositoryManager
    {
        //About
        IAboutReadRepository AboutReadRepository { get; }
        IAboutWriteRepository AboutWriteRepository { get; }
        
        //Contact
        IContactReadRepository ContactReadRepository { get; }
        IContactWriteRepository ContactWriteRepository { get; }

        //Project
        IProjectReadRepository ProjectReadRepository { get; }
        IProjectWriteRepository ProjectWriteRepository { get; }

        //Skill
        ISkillReadRepository SkillReadRepository { get; }
        ISkillWriteRepository SkillWriteRepository { get; }

       

        // void Save();


    }
}
