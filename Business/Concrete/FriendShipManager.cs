using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.CrossCuttingCorcerns.Logging;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.UsersDetail;
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
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;


        public FriendShipManager(IFriendShipDal friendShipDal, IUserDal userDal, ILoggerService loggerService, IMapper mapper)
        {
            _friendShipDal = friendShipDal;
            _userDal = userDal;
            _logger = loggerService;
            _mapper = mapper;
        }

        public IDataResult<FriendShip> AddFriend(int userId, int friendId)
        {
            _logger.LogInfo("Add Friend method called");
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
            _logger.LogInfo("Add Friend method completed");
            return new SuccessDataResult<FriendShip>(friendship, Messages.FriendAdd);
        }

        public IDataResult<List<UserDto>> GetFriend(int userId)
        {
            var friendships = _friendShipDal.GetAll(f => f.UserId == userId || f.FriendId == userId);

            if (friendships == null || !friendships.Any())
            {
                return new ErrorDataResult<List<UserDto>>(Messages.FriendNotFound);
            }

            var friendIds = friendships.Select(f => f.UserId == userId ? f.FriendId : f.UserId).ToList();
            var friends = _userDal.GetAll(u => friendIds.Contains(u.Id)).ToList();
            var userDtos = _mapper.Map<List<UserDto>>(friends);
            return new SuccessDataResult<List<UserDto>>(userDtos, Messages.UserFriendList);
        }




    }
}
