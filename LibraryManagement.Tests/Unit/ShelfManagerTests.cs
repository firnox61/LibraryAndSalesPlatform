using AutoMapper;
using Business.Concrete;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ShelfDetail;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Unit
{
    public class ShelfManagerTests
    {
        private readonly ShelfManager _shelfManager;
        private readonly Mock<IShelfDal> _mockShelfDal;
        private readonly Mock<IMapper> _mockMapper;

        public ShelfManagerTests()
        {
            _mockShelfDal = new Mock<IShelfDal>();
            _mockMapper = new Mock<IMapper>();
            _shelfManager = new ShelfManager(_mockShelfDal.Object, _mockMapper.Object);
        }

        [Fact]
        public void Shelf_Add_Method_Test()
        {
            // Arrange
            var createShelfDto = new CreateShelfDto { Id = 1, LayerNumber = 2, SequenceNumber = 3, SectionCode = "A1" };
            var shelf = new Shelf { Id = 1, LayerNumber = 2, SequenceNumber = 3, SectionCode = "A1" };

            _mockMapper.Setup(m => m.Map<Shelf>(createShelfDto)).Returns(shelf);

            // Act
            var result = _shelfManager.Add(createShelfDto);

            // Assert
            _mockShelfDal.Verify(m => m.Add(shelf), Times.Once);
            Assert.True(result.Success);
            Assert.Equal(Messages.ShelfdAdd, result.Message);
        }

        /*  [Fact]
          public void GetAll_Should_Return_SuccessDataResult_With_Shelfs()
          {
              // Arrange
              var shelfs = new List<Shelf>
          {
              new Shelf { Id = 1, LayerNumber = 2, SequenceNumber = 3, SectionCode = "A1" }
          };
              _mockShelfDal.Setup(m => m.GetAll()).Returns(shelfs);

              // Act
              var result = _shelfManager.GetAll();

              // Assert
              Assert.True(result.Success);
              Assert.Equal(shelfs, result.Data);
              Assert.Equal(Messages.ShelfdList, result.Message);
          }*/

        [Fact]
        public void Shelf_GetById_Method_Test()
        {
            // Arrange
            var shelf = new Shelf { Id = 1, LayerNumber = 2, SequenceNumber = 3, SectionCode = "A1" };
            _mockShelfDal.Setup(m => m.Get(It.IsAny<System.Linq.Expressions.Expression<Func<Shelf, bool>>>())).Returns(shelf);

            // Act
            var result = _shelfManager.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(shelf, result.Data);
            Assert.Equal(Messages.ShelfDetail, result.Message);
        }
    }
}
