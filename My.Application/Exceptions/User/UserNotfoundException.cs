using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Exceptions.User
{
    public class UserNotfoundException : NotFoundException
    {
        public UserNotfoundException(string id) : base($"The about with Id :{id} could not found.")
        {
        }
    }
}
