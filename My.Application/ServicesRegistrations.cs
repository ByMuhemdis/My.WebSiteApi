
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Application.IRepositories;

using AutoMapper;
using System.Reflection;

namespace My.Application.Extensions
{
    public static class ServicesRegistrations
    {
        public static void AutoMapperDTOService(this IServiceCollection services)
        {
            ///AutoMapping IOC kaydı program.cs de yapılmıştır.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
           


        }
    }
}
