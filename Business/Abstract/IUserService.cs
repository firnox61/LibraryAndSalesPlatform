using Azure.Core;
using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs.UsersDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
       // List<OperationClaim> GetClaims(User user);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(int  id);
        User GetByMail(string email);
        IResult EditProfil(UserDto userDto, string password);
    }
}
