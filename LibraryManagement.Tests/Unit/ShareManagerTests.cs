using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Core.CrossCuttingCorcerns.Logging;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ShareDetail;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Unit
{

    public class ShareManagerTests
    {
        private readonly ShareManager _shareManager;
        private readonly Mock<IShareDal> _mockShareDal;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<INoteDal> _mockNoteDal;
        private readonly Mock<IUserDal> _mockUserDal;
        private readonly Mock<IFriendShipService> _mockFriendShipService;
        private readonly Mock<IFriendShipDal> _mockFriendShipDal;
        private readonly Mock<ILoggerService> _mockLogger;

        public ShareManagerTests()
        {
            _mockShareDal = new Mock<IShareDal>();
            _mockMapper = new Mock<IMapper>();
            _mockNoteDal = new Mock<INoteDal>();
            _mockUserDal = new Mock<IUserDal>();
            _mockFriendShipService = new Mock<IFriendShipService>();
            _mockFriendShipDal = new Mock<IFriendShipDal>();
            _mockLogger = new Mock<ILoggerService>();
            _shareManager = new ShareManager(
                _mockShareDal.Object,
                _mockMapper.Object,
                _mockNoteDal.Object,
                _mockUserDal.Object,
                _mockFriendShipService.Object,
                _mockFriendShipDal.Object,
                _mockLogger.Object);
        }

        [Fact]
        public void Add_CreateShareDto_Method_Test()
        {
            // Arrange
            var createShareDto = new CreateShareDto { NoteId = 1, SharedWithUserId = 2, Privacy = SharePrivacy.Public };
            var note = new Note { Id = 1, IsShared = true };
            _mockNoteDal.Setup(m => m.Get(It.IsAny<System.Linq.Expressions.Expression<System.Func<Note, bool>>>())).Returns(note);
            _mockMapper.Setup(m => m.Map<Share>(createShareDto)).Returns(new Share());

            // Act
            var result = _shareManager.Add(createShareDto);

            // Assert
            _mockShareDal.Verify(m => m.Add(It.IsAny<Share>()), Times.Once);
            _mockLogger.Verify(m => m.LogInfo(It.IsAny<string>()), Times.Exactly(2));
            Assert.True(result.Success);
            Assert.Equal(Messages.ShareAdd, result.Message);
        }

        [Fact]
        public void Add_When_Note_Is_Not_Shared_Method_Test()
        {
            // Arrange
            var createShareDto = new CreateShareDto { NoteId = 1, SharedWithUserId = 2, Privacy = SharePrivacy.Public };
            var note = new Note { Id = 1, IsShared = false };
            _mockNoteDal.Setup(m => m.Get(It.IsAny<System.Linq.Expressions.Expression<System.Func<Note, bool>>>())).Returns(note);

            // Act
            var result = _shareManager.Add(createShareDto);

            // Assert
            _mockLogger.Verify(m => m.LogWarning(It.IsAny<string>()), Times.Once);
            Assert.False(result.Success);
            Assert.Equal(Messages.ShareFail, result.Message);
        }

        [Fact]
        public void Delete_Share_Method_Test()
        {
            // Arrange
            int shareId = 1;
            var share = new Share { Id = shareId, NoteId = 1, SharedWithUserId = 2 };
            _mockShareDal.Setup(m => m.Get(s => s.Id == shareId)).Returns(share);

            // Act
            var result = _shareManager.Delete(shareId);

            // Assert
            _mockShareDal.Verify(m => m.Delete(share), Times.Once);
            Assert.True(result.Success);
            Assert.Equal(Messages.ShareDelete, result.Message);
        }

       /* [Fact]
        public void GetAll_Should_Return_SuccessDataResult_With_Shares()
        {
            // Arrange
            var shares = new List<Share>
        {
            new Share { Id = 1, NoteId = 1, SharedWithUserId = 2 }
        };
            _mockShareDal.Setup(m => m.GetAll()).Returns(shares);

            // Act
            var result = _shareManager.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(shares, result.Data);
        }*/

        [Fact]
        public void GetById_ShareDto_Method_Test()
        {
            // Arrange
            int shareId = 1;
            var share = new Share { Id = shareId, NoteId = 1, SharedWithUserId = 2 };
            var shareDto = new ShareDto { Id = shareId, NoteId = 1, SharedWithUserId = 2 };
            _mockShareDal.Setup(m => m.Get(s => s.Id == shareId)).Returns(share);
            _mockMapper.Setup(m => m.Map<ShareDto>(share)).Returns(shareDto);

            // Act
            var result = _shareManager.GetById(shareId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(shareDto, result.Data);
            Assert.Equal(Messages.ShareDetail, result.Message);
        }

      /*  [Fact]
        public void Update_CreateShareDto_Method_Test()
        {
            // Arrange
            var createShareDto = new CreateShareDto { NoteId = 1, SharedWithUserId = 2, Privacy = SharePrivacy.Public };
            var share = new Share { Id = 1, NoteId = 1, SharedWithUserId = 2, Privacy = SharePrivacy.Public };

            _mockMapper.Setup(m => m.Map<Share>(createShareDto)).Returns(share);

            // Act
            var result = _shareManager.Update(createShareDto);

            // Assert
            _mockShareDal.Verify(m => m.Update(share), Times.Once);
            Assert.True(result.Success);
            Assert.Equal(Messages.ShareUpdate, result.Message);
        }*/
    }
}
