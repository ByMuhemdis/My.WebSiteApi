using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using My.Application.IRepositories;
using My.Services.Iservices;
using My.Services.Iservices.IAbout;
using My.Services.Iservices.IContact;
using My.Services.Iservices.IProject;
using My.Services.Iservices.ISkill;
using My.Services.Iservices.IUser;
using My.Services.ServicesManager.About;
using My.Services.ServicesManager.Contact;
using My.Services.ServicesManager.Project;
using My.Services.ServicesManager.Skill;
using My.Services.ServicesManager.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.ServicesManager
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ILoggerService _loggerService;
 


        private readonly Lazy<IAboutService> _aboutService;
        private readonly Lazy<IContactService> _contactService;
        private readonly Lazy<IProjectService> _projectService;
        private readonly Lazy<ISkillService> _skillService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<ILoginService> _loginservice;


        public ServiceManager(IRepositoryManager repositoryManager, ILoggerService loggerService, IMapper mapper,UserManager<Entities.Models.User> manager,IConfiguration configuration)
        {
            _repositoryManager = repositoryManager;
            _loggerService = loggerService;


            _aboutService = new Lazy<IAboutService>(() => new AboutManager(_repositoryManager,_loggerService, mapper));
            _contactService = new Lazy<IContactService>(() => new ContactManager(_repositoryManager, _loggerService,mapper));
            _projectService = new Lazy<IProjectService>(() => new ProjectManager(_repositoryManager,_loggerService,mapper));
            _skillService = new Lazy<ISkillService>(() => new SkillManager(_repositoryManager,_loggerService));
            _userService = new Lazy<IUserService>(() => new UserManager(_loggerService, mapper, manager));
            _loginservice = new Lazy<ILoginService>(() => new LoginManager(mapper,_loggerService,manager,configuration));
           

        }

        public IAboutService AboutService => _aboutService.Value;

        public IContactService ContactService => _contactService.Value;

        public IProjectService ProjectService => _projectService.Value;

        public ISkillService SkillService => _skillService.Value;

        public IUserService userService => _userService.Value;
        public ILoginService loginService => _loginservice.Value;
    }
}
