using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My.Applicatio.Exceptions.Skill;
using My.Application.DTOs.User;
using My.Application.Exceptions.User;
using My.Application.IRepositories;
using My.Services.Iservices;
using My.Services.Iservices.IUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.ServicesManager.User
{
    public class UserManager : IUserService
    {
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;
        private readonly UserManager<Entities.Models.User> _manager;
    

        public UserManager(ILoggerService loggerService,
            IMapper mapper,
            UserManager<Entities.Models.User> manager)
        {
            _loggerService = loggerService;
            _mapper = mapper;
            _manager = manager;
        }

        public async Task<IdentityResult> AddUserAsync(UserDto userDto)
        {
           
            var user = _mapper.Map<Entities.Models.User>(userDto);
            var result = await _manager.CreateAsync(user,userDto.Password);//Kullanıcıyı ve parolayı aynı anda ekler. Genellikle parolayla kullanıcı oluşturmak için kullanılır.
            
            if(result.Succeeded)
            {
                await _manager.AddToRolesAsync(user, userDto.Roles);
            }

            _loggerService.LogInfo("user is added");
          
            return result;

        }

        public async Task<IdentityResult> ChangePasswordAsync(UserDto userDto, string currentPassword, string newPassword)//şifre degiştirme
        {
            var user = await _manager.FindByEmailAsync(userDto.FirstName);
            if(user == null)
    {
                throw new ArgumentException("User not found.");
            }
            return await _manager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
       
        public async Task<bool> ConfirmEmailAsync(UserDto userDto, string token)//E-posta onayını gerçekleştirir.
        {
            var user = await _manager.FindByEmailAsync(userDto?.FirstName);
            if(user == null)
            {
                string msg = $"User is Not found .";
                _loggerService.LogError(msg);
                throw new ArgumentException("User not found.");
            }
            var result =await _manager.ConfirmEmailAsync(user, token);   
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string id, bool tracking)
        {
            var userDelete = await _manager.FindByIdAsync(id);
            if (userDelete == null)
            {
                string msg = $"No value found matching {id} .";
                _loggerService.LogError(msg);
                throw new UserNotfoundException(id);
            }
            var result = await _manager.DeleteAsync(userDelete);
            return result.Succeeded;

        }

        public async Task<string> GeneratePasswordResetTokenAsync(UserDto userDto)//Şifre sıfırlama işlemleri için bir token oluşturur.
        {
            // Kullanıcıyı email veya kullanıcı adına göre bul
            var user = await _manager.FindByEmailAsync(userDto.FirstName);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            // Şifre sıfırlama tokeni oluştur
            return await _manager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<List<Entities.Models.User>> GetUserAllAsync(bool tracking)
        {
            var userList =  tracking ? _manager.Users : _manager.Users.AsNoTracking();

            return await userList.ToListAsync();
        }


        public async Task<Entities.Models.User> GetUserByIdAsync(string id)
        {
            var userId= await _manager.FindByIdAsync(id);
            return userId;
        }

        public async Task<IdentityResult> ResetPasswordAsync(UserDto userDto, string token, string newPassword)//GeneratePasswordResetTokenAsync ile uretilen token sayesinde şifre sıfırlanır
        {
            var user = await _manager.FindByEmailAsync(userDto.FirstName);
            var userReset = await _manager.ResetPasswordAsync(user, token, newPassword);
            return userReset;
        }

        public async Task<bool> UpdateUserAsync(string id, UserDto userDto)
        {
            var user = await _manager.FindByIdAsync(id);
            if (user == null)
            {
                string msg = $"No value found matching {id} .";
                _loggerService.LogError(msg);
                throw new UserNotfoundException(id);
            }
            var userUpdate = _mapper.Map(userDto, user);
            var result =await _manager.UpdateAsync(userUpdate);
            return result.Succeeded;
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)//E-posta ve şifre ile kullanıcı kimlik bilgilerini doğrular.
        {
            var user = await _manager.FindByEmailAsync(email);

            return user != null && await _manager.CheckPasswordAsync(user, password);//verilen şifre kullanıcıya ait şifreyle uysuyormu ona bakıyor
        }
    }
}
