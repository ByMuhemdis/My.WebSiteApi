using AutoMapper;
using Entities.Models;
using My.Application.DTOs.About;
using My.Application.DTOs.Contact;
using My.Application.DTOs.Project;
using My.Application.DTOs.Skill;
using My.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.AutoMapping
{
    //sadece by class ı oluşturup bunun service kaydını yaptıktan sonra kullanmaya başladık 
    public class MappingProfile :Profile
    {
       
        public MappingProfile()
        {
            CreateMap<About, AboutDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<UserDto, User>();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
            });


        }
       
    }
}
