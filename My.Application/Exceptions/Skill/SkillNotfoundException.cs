using My.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Applicatio.Exceptions.Skill
{
    public class SkillNotfoundException : NotFoundException
    {
        public SkillNotfoundException(int id) : base($"The about with Id :{id} could not found.")
        {
        }
    }
}
