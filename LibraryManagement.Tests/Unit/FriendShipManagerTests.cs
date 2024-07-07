using AutoMapper;
using Business.Concrete;
using Business.Constants;
using Core.CrossCuttingCorcerns.Logging;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.UsersDetail;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Unit
{
    public class FriendShipManagerTests
    {
        private readonly FriendShipManager _friendShipManager;
        private readonly Mock<IFriendShipDal> _mockFriendShipDal;
        private readonly Mock<IUserDal> _mockUserDal;
        private readonly Mock<ILoggerService> _mockLogger;
        private readonly Mock<IMapper> _mockMapper;

        public FriendShipManagerTests()
        {
            _mockFriendShipDal = new Mock<IFriendShipDal>();
            _mockUserDal = new Mock<IUserDal>();
            _mockLogger = new Mock<ILoggerService>();
            _mockMapper = new Mock<IMapper>();
            _friendShipManager = new FriendShipManager(_mockFriendShipDal.Object, _mockUserDal.Object, _mockLogger.Object, _mockMapper.Object);
        }

        [Fact]
        public void Friend_Add_Method_Test()
        {
            // Arrange
            int userId = 1;
            int friendId = 2;
            _mockFriendShipDal.Setup(m => m.Get(It.IsAny<System.Linq.Expressions.Expression<Func<FriendShip, bool>>>())).Returns((FriendShip)null);

            // Act
            var result = _friendShipManager.AddFriend(userId, friendId);

            // Assert
            _mockFriendShipDal.Verify(m => m.Add(It.IsAny<FriendShip>()), Times.Once);
            _mockLogger.Verify(m => m.LogInfo(It.IsAny<string>()), Times.Exactly(2));
            Assert.True(result.Success);
            Assert.Equal(Messages.FriendAdd, result.Message);
            Assert.Equal(userId, result.Data.UserId);
            Assert.Equal(friendId, result.Data.FriendId);
        }

        [Fact]
        public void Shelf_Friendship_Already_Exists_Method_Test()
        {
            // Arrange
            int userId = 1;
            int friendId = 2;
            var existingFriendship = new FriendShip { UserId = userId, FriendId = friendId };
            _mockFriendShipDal.Setup(m => m.Get(It.IsAny<System.Linq.Expressions.Expression<Func<FriendShip, bool>>>())).Returns(existingFriendship);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _friendShipManager.AddFriend(userId, friendId));
        }

        [Fact]
        public void GetFriend_Friendship_Method_Test()
        {
            // Arrange
            int userId = 1;
            var friendships = new List<FriendShip>
        {
            new FriendShip { UserId = userId, FriendId = 2 },
            new FriendShip { UserId = 3, FriendId = userId }
        };
            var friends = new List<User>
        {
            new User { Id = 2, FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new User { Id = 3, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
        };
            var userDtos = new List<UserDto>
        {
            new UserDto { Id = 2, FirstName = "John", LastName = "Doe", Email = "john@example.com" },
            new UserDto { Id = 3, FirstName = "Jane", LastName = "Smith", Email = "jane@example.com" }
        };

            _mockFriendShipDal.Setup(m => m.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<FriendShip, bool>>>())).Returns(friendships);
            _mockUserDal.Setup(m => m.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<User, bool>>>())).Returns(friends);
            _mockMapper.Setup(m => m.Map<List<UserDto>>(friends)).Returns(userDtos);

            // Act
            var result = _friendShipManager.GetFriend(userId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(userDtos, result.Data);
            Assert.Equal(Messages.UserFriendList, result.Message);
        }

        [Fact]
        public void GetFriend_Friend_Method_Test()
        {
            // Arrange
            int userId = 1;
            _mockFriendShipDal.Setup(m => m.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<FriendShip, bool>>>())).Returns(new List<FriendShip>());

            // Act
            var result = _friendShipManager.GetFriend(userId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(Messages.FriendNotFound, result.Message);
            Assert.Null(result.Data);
        }
    }
}
