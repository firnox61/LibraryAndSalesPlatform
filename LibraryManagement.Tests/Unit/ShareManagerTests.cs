using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Core.CrossCuttingCorcerns.Logging;
using Core.Utilities;
using Core.Utilities.Result;
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
        private readonly Mock<IShareDal> _mockShareDal;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<INoteDal> _mockNoteDal;
        private readonly Mock<IUserDal> _mockUserDal;
        private readonly Mock<IFriendShipService> _mockFriendShipService;
        private readonly Mock<IFriendShipDal> _mockFriendShipDal;
        private readonly ShareManager _shareManager;

        public ShareManagerTests()
        {
            _mockShareDal = new Mock<IShareDal>();
            _mockMapper = new Mock<IMapper>();
            _mockNoteDal = new Mock<INoteDal>();
            _mockUserDal = new Mock<IUserDal>();
            _mockFriendShipService = new Mock<IFriendShipService>();
            _mockFriendShipDal = new Mock<IFriendShipDal>();
            _shareManager = new ShareManager(_mockShareDal.Object, _mockMapper.Object, _mockNoteDal.Object,
                _mockUserDal.Object, _mockFriendShipService.Object, _mockFriendShipDal.Object);
        }

        [Fact]
        public void Add_ValidParameters_ReturnSuccessResult()
        {
            // Arrange
            var dto = new CreateShareDto { NoteId = 1, SharedWithUserId = 2, Privacy = SharePrivacy.Public };
            var share = new Share();

            //_mockNoteDal.Setup(n => n.Get(b=>b.Id==dto.).Returns(new Note { IsShared = false });
            _mockMapper.Setup(m => m.Map<Share>(dto)).Returns(share);
            _mockShareDal.Setup(s => s.Add(share));

            // Act
            var result = _shareManager.Add(dto);

            // Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.ShareAdd, result.Message);
        }

        [Fact]
        public void Delete_ExistingShareId_ReturnSuccessResult()
        {
            // Arrange
            var share = new Share { Id = 1 };
            _mockShareDal.Setup(s => s.Get(n => n.Id == share.Id)).Returns(share);

            // Act
            var result = _shareManager.Delete(1);

            // Assert
            Assert.IsType<SuccessResult>(result);
            Assert.Equal(Messages.ShareDelete, result.Message);
        }

        [Fact]
        public void GetById_ExistingId_ReturnShareDto()
        {
            // Arrange
            var share = new Share { Id = 1 };
            var shareDto = new ShareDto { Id = 1 };
            _mockShareDal.Setup(s => s.Get(n=>n.Id==share.Id)).Returns(share);
            _mockMapper.Setup(m => m.Map<ShareDto>(share)).Returns(shareDto);

            // Act
            var result = _shareManager.GetById(1);

            // Assert
            Assert.IsType<SuccessDataResult<ShareDto>>(result);
            Assert.Equal(Messages.ShareDetail, result.Message);
            Assert.Equal(1, result.Data.Id);
        }
    }
}