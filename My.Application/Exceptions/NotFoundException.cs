using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        protected NotFoundException(string massage):base(massage) 
        {
            
        }


    }
   
}
