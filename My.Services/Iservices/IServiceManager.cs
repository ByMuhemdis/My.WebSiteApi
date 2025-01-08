using My.Services.Iservices.IAbout;
using My.Services.Iservices.IContact;
using My.Services.Iservices.IProject;
using My.Services.Iservices.ISkill;
using My.Services.Iservices.IUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.Iservices
{
    public interface IServiceManager
    {
        IAboutService AboutService { get; }
        IContactService ContactService { get; }
        IProjectService ProjectService { get; }
        ISkillService SkillService { get; }
        IUserService userService { get; }

        ILoginService loginService { get; }
    }
}
