using AspNetCoreRateLimit;
using Entities.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using My.Application.Context;
using NLog.LayoutRenderers.Wrappers;
using System.Text;

namespace WebSiteApi.Extensions
{
    public static class ServiceRegistrations
    {
        //burası caching yaparken lazım 
        public static void ConfigureresponseCaching(this IServiceCollection services)
        {
            services.AddResponseCaching();
        }
        //3. yol caching de oacket yukledik marvin cache heardes adında  bu 3. caching yolu burada    indirdigimiz paketten confugure olusturacagız 1. adım olarak
        public static void ConfigureHttpCacheHeaders(this IServiceCollection services)
        {
            services.AddHttpCacheHeaders(expirationOpt =>
            {
                expirationOpt.MaxAge = 70;
                expirationOpt.CacheLocation = CacheLocation.Private;
                //burada defaolut olarak gelen özellikleri burada degiştirebiliyoruz.
            });
            //daha sonra program cs dee cagıralım.
            //daha sonra programcs de var olan app kısmına app.usehttpcaheheaders ile bir işlem yazcaz. 
        }

        //1 olarak program.cs de 1. adım olarak  builder.Services.AddMemoryCache(); ekliyoruz daha sonra 2. olarak burada bir configure metodu yazacagız ve bunu progra.cs de cagıracagız
        public static void ConfigurureRateLimitingOptions(this IServiceCollection services)
        {
            var reteLimitRoles = new List<RateLimitRule>()
            {
                new RateLimitRule()//burası 1 dakika içinde yanlızca apiye 3 istek alabilir diyoruz 
                {
                    Endpoint ="*",// Tüm API uç noktaları için geçerli (* ile belirtiliyor).
                    Limit =3,     // 3 istek ile sınırlandırılıyor.
                    Period ="1m"  // Sınırlama periyodu 1 dakika olarak belirleniyor.

                }
            };


            //IpRateLimitOptions: IP adresi tabanlı hız limiti ayarlarını yapılandırır.
            //GeneralRules: Yukarıdaki hız limiti kurallarını uygular.
            services.Configure<IpRateLimitOptions>(opt =>
             {
                 opt.GeneralRules = reteLimitRoles;// Tanımlanan hız limiti kuralları uygulanıyor.
             });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();//Hız limiti sayacı (isteklerin sayısını hafızada saklar).
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();                //IP politikalarını yönetir.
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();          //Genel hız limiti yapılandırmasını yönetir.
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();  // İşlem sırasını ve kilitlenmeleri yönetir (asenkron kilitleme stratejisi kullanılır).

        }

        ///Identity ve Identity role ısmını burada yarlamalarını yapıp program.cs de bunu cagıracagız
        ///
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 6;//6 Karekter olacaktır.

                opt.User.RequireUniqueEmail = true;// Bir Email bir kere kullanıla bilir.
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection("JwtSettings");
            var secretkey = jwtSetting["secretKey"];

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSetting["validIssuer"],
                ValidAudience = jwtSetting["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey))
            });

        }

        public static void ConfugureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title ="RestFull Api Çalışması",
                    Version = "v1",
                    Description ="Bir web sitesi için backend çalışması yapamayan arkadaşlar için hazır olarak taşarlanmış backend RestFull Api",
                    Contact = new OpenApiContact
                    {
                        Name="ÖMER BAYRAK",
                        Email ="by.omer.bayrak@outlook.com",
                        Url =new Uri("https://www.linkedin.com/in/%C3%B6mer-b-689b76250/")
                       

                    }
                });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Beearer"
                        },

                        new List<string>()
                    }
                });

            });


        }

    }
}
