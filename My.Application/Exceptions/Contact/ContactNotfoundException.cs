using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Exceptions.Contact
{
    public class ContactNotfoundException : NotFoundException
    {
        public ContactNotfoundException(int id) : base($"The about with Id :{id} could not found.")
        {
        }
    }
}
