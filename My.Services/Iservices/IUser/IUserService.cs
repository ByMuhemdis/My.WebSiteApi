using Entities.Models;
using Microsoft.AspNetCore.Identity;
using My.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.Iservices.IUser
{
    public interface IUserService
    {
        Task<List<User>> GetUserAllAsync(bool tracking);//tüm kullanıcıları getir
        Task<IdentityResult> AddUserAsync(UserDto userDto); // Yeni kullanıcı ekle
        Task<bool> UpdateUserAsync(string id, UserDto userDto);//kullanıcı bilgilerini güncelle
        Task<bool> DeleteUserAsync(string id, bool tracking);
        Task<string> GeneratePasswordResetTokenAsync(UserDto userDto); // Şifre sıfırlama token'ı oluştur
        Task<IdentityResult> ResetPasswordAsync(UserDto userDto, string token, string newPassword); // Şifreyi sıfırlama
        Task<IdentityResult> ChangePasswordAsync(UserDto userDto, string currentPassword, string newPassword); // Şifre değiştir
        Task<bool> ConfirmEmailAsync(UserDto userDto, string token); // E-posta onayı
        Task<bool> ValidateUserCredentialsAsync(string email, string password); // Kullanıcı kimlik bilgilerini doğrula
        
    }
}
