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
    public interface IFriendShipService
    {
        IDataResult<FriendShip> AddFriend(int userId, int friendId);
        IDataResult<List<UserDto>> GetFriend(int userId);
    }
}
