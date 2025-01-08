using My.Application.IRepositories;
using My.Application.IRepositories.Abouts;
using My.Application.IRepositories.Contacts;
using My.Application.IRepositories.Projects;
using My.Application.IRepositories.Skills;
using My.Persistence.Context;
using My.Persistence.Repositories.Abouts;
using My.Persistence.Repositories.Contacts;
using My.Persistence.Repositories.Projects;
using My.Persistence.Repositories.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Persistence.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly AppDbContext _context;
        //About
        private readonly Lazy<IAboutReadRepository> _aboutReadRepository;
        private readonly Lazy<IAboutWriteRepository> _aboutWriteRepository;
        //Contact
        private readonly Lazy<IContactReadRepository> _contactReadRepository;
        private readonly Lazy<IContactWriteRepository> _contactWriteRepository;
        //Project
        private readonly Lazy<IProjectReadRepository> _projectReadRepository;
        private readonly Lazy<IProjectWriteRepository> _projectWriteRepository;
        //Skill
        private readonly Lazy<ISkillReadRepository> _skillReadRepository;
        private readonly Lazy<ISkillWriteRepository> _skillWriteRepository;
       

        //Lazy nesne ancak ve ancak çagrıldıgında newlenir.

        public RepositoryManager(AppDbContext context)
        {
            _context = context;
            _aboutReadRepository = new Lazy<IAboutReadRepository>(() => new AboutReadRepository(_context));
            _aboutWriteRepository = new Lazy<IAboutWriteRepository>(() => new AboutWriteRepository(_context));
            //Contact
            _contactReadRepository = new Lazy<IContactReadRepository>(() => new ContactReadRepository(_context));
            _contactWriteRepository = new Lazy<IContactWriteRepository>(() => new ContactWriteRepository(_context));
            //Project
            _projectReadRepository = new Lazy<IProjectReadRepository>(() => new ProjectReadRepository(_context));
            _projectWriteRepository = new Lazy<IProjectWriteRepository>(() => new ProjectWriteRepository(_context));
            //Skill
            _skillReadRepository = new Lazy<ISkillReadRepository>(() => new SkillReadRepository(_context));
            _skillWriteRepository = new Lazy<ISkillWriteRepository>(() => new SkillWriteRepository(_context));
            
           
        }
        //About
        public IAboutReadRepository AboutReadRepository => _aboutReadRepository.Value;
        public IAboutWriteRepository AboutWriteRepository => _aboutWriteRepository.Value;
        //Contract
        public IContactReadRepository ContactReadRepository => _contactReadRepository.Value;
        public IContactWriteRepository ContactWriteRepository => _contactWriteRepository.Value;
        //Project
        public IProjectReadRepository ProjectReadRepository => _projectReadRepository.Value;
        public IProjectWriteRepository ProjectWriteRepository => _projectWriteRepository.Value;
        //Skill
        public ISkillReadRepository SkillReadRepository => _skillReadRepository.Value;
        public ISkillWriteRepository SkillWriteRepository => _skillWriteRepository.Value;
      
       

        //İşlemlerin kaydedilmesi aynı işlem RepositoryWrite de oldugundan gerek yoktur 
        //public void Save()
        //{
        //    _context.SaveChanges();
        //}
    }
}
