﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.IRepositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table {  get; }
    }
}
