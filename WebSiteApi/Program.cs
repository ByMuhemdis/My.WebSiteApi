
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

            LogManager.Setup().LoadConfigurationFromFile($"{Directory.GetCurrentDirectory()}/nlog.config"); //***NLog'un yap�land�rma ayarlar�n� nlog.config dosyas�ndan y�kler.
            var loggers = LogManager.GetCurrentClassLogger();
            loggers.Info("Application started.");  // Uygulaman�n ba�lang�� logu.

            // Add services to the container.
            builder.Services.AddControllers(confing =>
            {
                //i�erik pazarl�g�yla isteklere g�re d�n�� yapabiliyoruz �rn text/xml --aplicationjson/xml -- text/csv--- aplicationjson/json �eklinde
                //istekler gelebilir biz burada bu isteklere izin verdigimizi s�yl�yoruz. yazi pazarl�ga ac�g�z ama istedgin formatta bir c�kt� veremedgi i�in hata verecektir 
                //burada default olarak application/json olarak ��kt� al�abiliriz ama bunlar� yazd�g�m�z i�in �nceen hangi sekilde �ag�r�rsan �ag�ral�m
                // aplication/json format�nda d�nderiyordu ama art�k bu format haricinde diger formatlarda hata verdirdik  orada hata veriyor.   
                confing.RespectBrowserAcceptHeader = true;
                confing.ReturnHttpNotAcceptable = true;
                confing.CacheProfiles.Add("5mins", new CacheProfile() { Duration = 300 });//2. yol k�sm� burada art�k onbellek (caching) profiline sahip olduk daha sonrada controller In en bas�na bu profili tan�ml�yor ve uyguluyoruz

            }).AddXmlDataContractSerializerFormatters();//***��ER�K PAZARLIGI bu sat�rla beraber xml format�nda da ��k�� vermemize olanak saglar.
                                                        //bununla beraber bir dto serilaz edilmesi gerekir. ki hata ile kar��la��lmas�n.

            //model state invalid i�lemi = Dogrulamai�lemi veri taban�na gidecek k�sm� kontolu saglan�yor.
            builder.Services.Configure<ApiBehaviorOptions>(confing =>
            {
                confing.SuppressModelStateInvalidFilter = true;
            });

            //**logFilterAttrubute i�in bir IOc kayd� yap�ld�.
            builder.Services.AddSingleton<LogFilterAttrubute>();
            builder.Services.AddScoped<ValidationFilterModelStateAttrubute>();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.ConfugureSwaggerGen();
            //Sql service kayd�
            builder.Services.ConFigureSqlContext(builder.Configuration);
            //Repository Kayd�
            builder.Services.ConfigureRepositories();//tek parametreli oldugundan buraya parametre vermeye gerek yok.
            //Service kayd�
            builder.Services.ConfigureServices();
            //Mappingprifile Ioc kayd�
            builder.Services.AutoMapperDTOService();
            //Loglama i�lemi 
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            //Caching i�lemi (*** �N BELLEGE ALMA ��LEM� IOC KAYDI. (RESPONSE CACH�NG ))
            
            builder.Services.ConfigureresponseCaching();// ** bunu yapt�ktan sonra asag�da userResponsecaching idadesini app �zerinden cag�rmal�y�z

            // �n bellege alma 3. ad�mda indirip serviceRegistrationda metoty yazd�ktan sonra burada kay�t yp�p asag�daki app k�sm�ndan sonrada bu kayd� app.usehttpcacheHeaders ile configurasyonu bitirelim
            builder.Services.ConfigureHttpCacheHeaders();

            //h�z l�miti limit k�sm� i�in bir memorycache ekleyelim. 1 ad�m 
            builder.Services.AddMemoryCache();
            // 2. ad�m olarak burada Extensions klas�r�ndeki (WEp API projesindeki) serviceRegistrations da olu�turdugumuz kurallar ve i�lem metodunu burada IoC kayd�n� yap�yoruz
            builder.Services.ConfigurureRateLimitingOptions();
            // bundan sonra son olarak a�ag�daki ifadeti de tan�mlay�p ��z�yoruz
            builder.Services.AddHttpContextAccessor(); //bu i�lemden sonra son olarak h�zlimiti i�in yap�lmas� gereken app k�sm�na inip app.useIprateliting ekliyip tamaml�yoruz

            builder.Services.ConfigureIdentity();
            ///******Identity Ioc kayd�
            // 1. olarak Authentication eklenecek
            // builder.Services.AddAuthentication(); //bunu art�k kulanmayacag�z bunun yerine i�ersinde JWT token i�in da ya�land�rm yapt�gm�z metodu kullanacag�z
            builder.Services.ConfigureJWT(builder.Configuration);
            //daha sonra Extencaionda tan�mlad�g�m�z k�sm� buraya cag�r�yoruz.
           

            var app = builder.Build();
            //Global hata y�netimini yap�land�rd�g�m�z k�sm� burada tan�ml�yoruz 

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
            app.UseIpRateLimiting();//h�z limi i�in eklendi
            app.UseResponseCaching();//�N BELLEK (CACH�NG) ��LEM�  bu i�lem yulkar�daki builder.Services.ConfigureresponseCaching(); i�lemle berabaer �agr�ld�.
            app.UseHttpCacheHeaders();//�n bellege alma 3. yolu son i�lem 
            
            app.UseAuthentication();//Giri� i�lemi k�sm�
            app.UseAuthorization();//buras� yetkilendirme k�sm� biz Identity yukar�ya ekledikten hemen sonra burada �nce giri� sonra yetkilendirme olmas� i�in     app.UseAuthentication(); ekleyecegiz


            app.MapControllers();
            
            app.Run();
        }
    }
}
