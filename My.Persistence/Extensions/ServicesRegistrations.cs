﻿ using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using My.Application.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Application.IRepositories;
using My.Application.Repositories;
using My.Application.IRepositories.Abouts;
using My.Application.Repositories.Abouts;
using My.Application.IRepositories.Contacts;
using My.Application.Repositories.Contacts;

namespace My.Application.Extensions
{
    public static class ServicesRegistrations
    {
        public static void ConFigureSqlContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
           );
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
           //istersek butun oluşturulan repositoryleride ayrı ayrı kaydedip istenilen tyerde istenilen repo yu çagırarak işlemlere devam edebiliriz istersek de
           //manager ile tek kayıtta butun repolara ualaşa biliriz.
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            //About Repository IOC kaydı
            services.AddScoped<IAboutReadRepository,AboutReadRepository>();
            services.AddScoped<IAboutWriteRepository,AboutWriteRepository>();
            //Contact Repositoris Ioc kaydı.
            services.AddScoped<IContactReadRepository, ContactReadRepository>();
            services.AddScoped<IContactWriteRepository, ContactWriteRepository>();
            ///.....Şeklinde tamamen istege baglı 
        }

       

      
    }
}
