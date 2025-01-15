
using AspNetCoreRateLimit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My.Application.Context;
using My.Application.Extensions;
using My.Services.Extensions;
using My.Services.Iservices;
using NLog;
using System.Runtime.InteropServices;
using WebSiteApi.ActionFilter;
using WebSiteApi.Extensions;


namespace WebSiteApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //

            LogManager.Setup().LoadConfigurationFromFile($"{Directory.GetCurrentDirectory()}/nlog.config"); //***NLog'un yapýlandýrma ayarlarýný nlog.config dosyasýndan yükler.
            var loggers = LogManager.GetCurrentClassLogger();
            loggers.Info("Application started.");  // Uygulamanýn baþlangýç logu.

            // Add services to the container.
            builder.Services.AddControllers(confing =>
            {
                //içerik pazarlýgýyla isteklere göre dönüþ yapabiliyoruz örn text/xml --aplicationjson/xml -- text/csv--- aplicationjson/json þeklinde
                //istekler gelebilir biz burada bu isteklere izin verdigimizi söylüyoruz. yazi pazarlýga acýgýz ama istedgin formatta bir cýktý veremedgi iöin hata verecektir 
                //burada default olarak application/json olarak çýktý alýabiliriz ama bunlarý yazdýgýmýz için önceen hangi sekilde çagýrýrsan çagýralým
                // aplication/json formatýnda dönderiyordu ama artýk bu format haricinde diger formatlarda hata verdirdik  orada hata veriyor.   
                confing.RespectBrowserAcceptHeader = true;
                confing.ReturnHttpNotAcceptable = true;
                confing.CacheProfiles.Add("5mins", new CacheProfile() { Duration = 300 });//2. yol kýsmý burada artýk onbellek (caching) profiline sahip olduk daha sonrada controller In en basýna bu profili tanýmlýyor ve uyguluyoruz

            }).AddXmlDataContractSerializerFormatters();//***ÝÇERÝK PAZARLIGI bu satýrla beraber xml formatýnda da çýkýþ vermemize olanak saglar.
                                                        //bununla beraber bir dto serilaz edilmesi gerekir. ki hata ile karþýlaþýlmasýn.

            //model state invalid iþlemi = Dogrulamaiþlemi veri tabanýna gidecek kýsmý kontolu saglanýyor.
            builder.Services.Configure<ApiBehaviorOptions>(confing =>
            {
                confing.SuppressModelStateInvalidFilter = true;
            });

            //**logFilterAttrubute için bir IOc kaydý yapýldý.
            builder.Services.AddSingleton<LogFilterAttrubute>();
            builder.Services.AddScoped<ValidationFilterModelStateAttrubute>();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfugureSwaggerGen();
            //Sql service kaydý
            builder.Services.ConFigureSqlContext(builder.Configuration);
            //Repository Kaydý
            builder.Services.ConfigureRepositories();//tek parametreli oldugundan buraya parametre vermeye gerek yok.
            //Service kaydý
            builder.Services.ConfigureServices();
            //Mappingprifile Ioc kaydý
            builder.Services.AutoMapperDTOService();
            //Loglama iþlemi 
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            //Caching iþlemi (*** ÖN BELLEGE ALMA ÝÞLEMÝ IOC KAYDI. (RESPONSE CACHÝNG ))
            
            builder.Services.ConfigureresponseCaching();// ** bunu yaptýktan sonra asagýda userResponsecaching idadesini app üzerinden cagýrmalýyýz

            // ön bellege alma 3. adýmda indirip serviceRegistrationda metoty yazdýktan sonra burada kayýt ypýp asagýdaki app kýsmýndan sonrada bu kaydý app.usehttpcacheHeaders ile configurasyonu bitirelim
            builder.Services.ConfigureHttpCacheHeaders();

            //hýz lýmiti limit kýsmý için bir memorycache ekleyelim. 1 adým 
            builder.Services.AddMemoryCache();
            // 2. adým olarak burada Extensions klasöründeki (WEp API projesindeki) serviceRegistrations da oluþturdugumuz kurallar ve iþlem metodunu burada IoC kaydýný yapýyoruz
            builder.Services.ConfigurureRateLimitingOptions();
            // bundan sonra son olarak aþagýdaki ifadeti de tanýmlayýp çözüyoruz
            builder.Services.AddHttpContextAccessor(); //bu iþlemden sonra son olarak hýzlimiti için yapýlmasý gereken app kýsmýna inip app.useIprateliting ekliyip tamamlýyoruz

            builder.Services.ConfigureIdentity();
            ///******Identity Ioc kaydý
            // 1. olarak Authentication eklenecek
            // builder.Services.AddAuthentication(); //bunu artýk kulanmayacagýz bunun yerine içersinde JWT token için da yaýlandýrm yaptýgmýz metodu kullanacagýz
            builder.Services.ConfigureJWT(builder.Configuration);
            //daha sonra Extencaionda tanýmladýgýmýz kýsmý buraya cagýrýyoruz.
           

            var app = builder.Build();
            //Global hata yönetimini yapýlandýrdýgýmýz kýsmý burada tanýmlýyoruz 

            var logger = app.Services.GetRequiredService<ILoggerService>();
            app.ConfigureExceptionHandler(logger);

            // Configure the HTTP request pipeline.


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            if (app.Environment.IsProduction())
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseIpRateLimiting();//hýz limi için eklendi
            app.UseResponseCaching();//ÖN BELLEK (CACHÝNG) ÝÞLEMÝ  bu iþlem yulkarýdaki builder.Services.ConfigureresponseCaching(); iþlemle berabaer çagrýldý.
            app.UseHttpCacheHeaders();//ön bellege alma 3. yolu son iþlem 
            
            app.UseAuthentication();//Giriþ iþlemi kýsmý
            app.UseAuthorization();//burasý yetkilendirme kýsmý biz Identity yukarýya ekledikten hemen sonra burada önce giriþ sonra yetkilendirme olmasý için     app.UseAuthentication(); ekleyecegiz


            app.MapControllers();
            
            app.Run();
        }
    }
}
