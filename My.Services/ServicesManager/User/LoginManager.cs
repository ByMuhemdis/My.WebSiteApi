using AutoMapper;
using Entities.ErrorModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using My.Application.DTOs.Token;
using My.Application.DTOs.User;
using My.Application.Exceptions;
using My.Services.Iservices;
using My.Services.Iservices.IUser;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.ServicesManager.User
{
    public class LoginManager : ILoginService
    {
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly UserManager<Entities.Models.User> _userManager;
        private readonly IConfiguration _configuration;

        private Entities.Models.User? _user;

        public LoginManager(IMapper mapper, ILoggerService logger, UserManager<Entities.Models.User> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signinCredentials = GetSiginCredentials();//kimlik bilgilerini alıyoruz
            var claims = await GetClaims();//Claims ları aldık 
            var TokenOptions = GenerateTokenOptions(signinCredentials, claims);//token oluşturma optionsları generat ettik

            var refreshtoken = GenerateRefreshToken();
            _user.RefreshToken = refreshtoken;
            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userManager.UpdateAsync(_user);// Kullanıcıyı güncelle

            var accesstoken = new JwtSecurityTokenHandler().WriteToken(TokenOptions);//ilgili tken oluşmasını saglamak üzere parametre geçtik

            return new TokenDto
            {
                AccessToken = accesstoken,
                RefreshToken = refreshtoken
            };
        }


        public async Task<bool> LoginUser(LoginDto loginDto)
        {
            _user = await _userManager.FindByNameAsync(loginDto.UserNAmeOrEmail);
            if (_user == null)
            {
                _user = await _userManager.FindByEmailAsync(loginDto.UserNAmeOrEmail);
                if (_user is null)
                {
                    _logger.LogError("Username or Email is not found");
                    return false;
                }
            }

            var result = await _userManager.CheckPasswordAsync(_user, loginDto.Password!);
            if (result == false)
            {
                _logger.LogError("The user entered the password incorrectly. ");
            }
            else
                _logger.LogDebug($"user :  {_user} logged in today at time :{DateTime.Now} ");

            return result;


        }


        private SigningCredentials GetSiginCredentials()
        {
            var jwtSetting = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSetting["secretKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName )
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;

        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials, List<Claim> claims)
        {
            var jwtSetting = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                    issuer: jwtSetting["validIssuer"],
                    audience: jwtSetting["validAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSetting["expires"])),
                    signingCredentials: signinCredentials
                );

            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {

            var jwtSetting = _configuration.GetSection("JwtSettings");//tokenı alıyoruz 
            var secretkey = jwtSetting["secretKey"];

            var tokenvalidationParameters = new TokenValidationParameters //token dogrulamakısnı parametrelerini yazdık 
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSetting["validIssuer"],
                ValidAudience = jwtSetting["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey))
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenhandler.ValidateToken(token, tokenvalidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("token invalid.");
            }

            return principal;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);//ilgili kullanıcının olup olmadıgının teyidi i.in yapıldı.

            if (user == null || user.RefreshToken != tokenDto.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new RefreshTokenBadRequestException();//bu ifade My.Application da EXceptions klasöründe hata classı olarak oluşturuldu.
            }


            _user = user;

            return await CreateToken(populateExp: false);
        }
    }
}
