using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Exceptions.Project
{
    public class ProjectNotfoundException : NotFoundException
    {
        public ProjectNotfoundException(int id) : base($"The about with Id :{id} could not found.")
        {
        }
    }
}
