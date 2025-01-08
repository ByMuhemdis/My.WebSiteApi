using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.DTOs.User
{
    public class LoginDto
    {
        public string? UserNAmeOrEmail { get; set; }
        public string? Password { get; set; }
    }
}
