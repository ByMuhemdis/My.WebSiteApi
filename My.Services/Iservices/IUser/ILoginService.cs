using My.Application.DTOs.Token;
using My.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.Iservices.IUser
{
    public interface ILoginService
    {
        Task<bool> LoginUser(LoginDto loginDto);

        Task<TokenDto> CreateToken(bool populateEx);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
    }
}
