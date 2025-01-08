﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.DTOs.User
{
    public class UserDto
    {

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

       
        public string? UserName { get; set; }

        public string? Password { get; set; }
        public string? Email { get; set; }     
        public string? PhoneNumber { get; set; }

        public ICollection<string>? Roles { get; set; }



    }
}
