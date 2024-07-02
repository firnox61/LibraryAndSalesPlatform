//using Azure.Core;
using Core.Entities.Concrete;
using Core.Utilities.Result;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.DTOs.UsersDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(RegisterUserDto registerUserDto, string password);
        IDataResult<User> Login(LoginUserDto loginUserDto);
        IResult UserExists(string email);//kullanıcı varmı ve maille yapıcaz
        IDataResult<AccessToken> CreateAccessToken(User user);//acces token üretmek  istiyoruz
    }
}
