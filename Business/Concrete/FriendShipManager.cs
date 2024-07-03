using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FriendShipManager : IFriendShipService
    {
        IFriendShipDal _friendShipDal;
        private readonly IUserDal _userDal;

        public FriendShipManager(IFriendShipDal friendShipDal, IUserDal userDal)
        {
            _friendShipDal = friendShipDal;
            _userDal = userDal;
        }

        public IDataResult<FriendShip> AddFriend(int userId, int friendId)
        {
            var existingFriendship = _friendShipDal.Get(f => (f.UserId == userId && f.FriendId == friendId) || (f.UserId == friendId && f.FriendId == userId));
            // .FirstOrDefaultAsync(f => (f.UserId == userId && f.FriendId == friendId) || (f.UserId == friendId && f.FriendId == userId));
            if (existingFriendship != null)
            {
                throw new InvalidOperationException("Friendship zaten var.");
            }
            var friendship = new FriendShip
            {
                UserId = userId,
                FriendId = friendId
            };
            _friendShipDal.Add(friendship);
            return new SuccessDataResult<FriendShip>(friendship, "Friend added successfully.");
        }

        public IDataResult<List<User>> GetFriend(int userId)
        {
            var friendships = _friendShipDal.GetAll(f => f.UserId == userId || f.FriendId == userId);

            if (friendships == null || !friendships.Any())
            {
                return new ErrorDataResult<List<User>>("No friends found.");
            }

            var friendIds = friendships.Select(f => f.UserId == userId ? f.FriendId : f.UserId).ToList();
            var friends = _userDal.GetAll(u => friendIds.Contains(u.Id)).ToList();
            return new SuccessDataResult<List<User>>(friends, "Friends retrieved successfully.");
        }




    }
}
