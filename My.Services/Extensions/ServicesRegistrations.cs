
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Application.IRepositories;

using My.Services.Iservices.IAbout;
using My.Services.ServicesManager.About;
using My.Services.Iservices;
using My.Services.ServicesManager;
using My.Services.Iservices.IContact;
using My.Services.ServicesManager.Contact;

namespace My.Services.Extensions
{

    public static class ServicesRegistrations
    {


        public static void ConfigureServices(this IServiceCollection services)
        {
            //istersek butun oluşturulan serviceleri ayrı ayrı kaydedip istenilen yerde istenilen service yu çagırarak işlemlere devam edebiliriz istersek de
            //manager ile tek kayıtta butun servislerin tamamına  ualaşa biliriz.
            services.AddScoped<IServiceManager,ServiceManager>();
            //About Service Ioc Kaydı.
            services.AddScoped<IAboutService, AboutManager>();
            //Contact Service IOC kaydı.
            services.AddScoped<IContactService,ContactManager>();

            /// istege göre digerleride kaydedilebilir biz burada kısa yoldan service manager uzerinden servistelirn tamamına ulaşacagız.
            /// 
            
            //Log service IOc kaydı

            services.AddSingleton<ILoggerService,LoggerManager>();

        }
    }
}
