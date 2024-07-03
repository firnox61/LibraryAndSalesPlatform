using AutoMapper;
using Business.Concrete;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests
{
    public class BookManagerTests
    {
        private readonly Mock<IBookDal> _mockBookDal;
        private readonly Mock<IMapper> _mockMapper;//nesneler oluşuyor
        private readonly Mock<IHostEnvironment> _mockEnvironment;
        private readonly BookManager _bookManager;

        public BookManagerTests()
        {
            _mockBookDal = new Mock<IBookDal>();
            _mockMapper = new Mock<IMapper>();
            _mockEnvironment = new Mock<IHostEnvironment>();

            _mockEnvironment.Setup(e => e.ContentRootPath).Returns(Directory.GetCurrentDirectory());

            _bookManager = new BookManager(_mockBookDal.Object, _mockMapper.Object, _mockEnvironment.Object);
        }

        [Fact]
        public void Add_ShouldReturnSuccessResult_WhenBookIsValid()
        {
            // Arrange
            var bookCreateDto = new BookCreateDto
            {
                Title = "Test Book",
                CoverImageUrl = Mock.Of<IFormFile>()
            };

            _mockMapper.Setup(m => m.Map<Book>(It.IsAny<BookCreateDto>())).Returns(new Book());

            // Act
            var result = _bookManager.Add(bookCreateDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(Messages.BookAdd, result.Message);
            _mockBookDal.Verify(b => b.Add(It.IsAny<Book>()), Times.Once);
        }

        [Fact]
        public void Delete_ShouldDeleteBookFromRepository()
        {
            // Arrange
            var bookId = 1;
            var bookToDelete = new Book { Id = bookId };

            _mockBookDal.Setup(b => b.Get(It.IsAny<Expression<Func<Book, bool>>>())).Returns(bookToDelete);

            // Act
            var result = _bookManager.Delete(bookId);

            // Assert
            Assert.True(result.Success);
            _mockBookDal.Verify(b => b.Delete(It.IsAny<Book>()), Times.Once);
        }

        /*[Fact]
        public void GetAll_ShouldReturnListOfBookListDto()
        {
            // Arrange
            var booksInRepository = new List<Book>
    {
        new Book { Id = 1, Title = "Book 1" },
        new Book { Id = 2, Title = "Book 2" }
    };

            _mockBookDal.Setup(b => b.GetAll()).Returns(() => booksInRepository);

            // Act
            var result = _bookManager.GetAll();

            // Assert
            Assert.IsType<SuccessDataResult<List<BookListDto>>>(result);
            Assert.Equal(booksInRepository.Count, result.Data.Count);
        }*/

        [Fact]
        public void GetById_ShouldReturnBookDetailDto()
        {
            // Arrange
            var bookId = 1;
            var bookInRepository = new Book { Id = bookId, Title = "Test Book" };

            _mockBookDal.Setup(b => b.Get(It.IsAny<Expression<Func<Book, bool>>>())).Returns(bookInRepository);

            // Act
            var result = _bookManager.GetById(bookId);

            // Assert
            Assert.IsType<SuccessDataResult<BookDetailDto>>(result);
            Assert.Equal(bookInRepository.Title, result.Data.Title);
        }

        [Fact]
        public void Update_ShouldReturnSuccessResult()
        {
            // Arrange
            var bookUpdateDto = new BookUpdateDto
            {
                Id = 1,
                Title = "Updated Book"
            };

            var updatedBook = new Book { Id = bookUpdateDto.Id, Title = bookUpdateDto.Title };

            _mockMapper.Setup(m => m.Map<Book>(It.IsAny<BookUpdateDto>())).Returns(updatedBook);

            // Act
            var result = _bookManager.Update(bookUpdateDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(Messages.BookUpdate, result.Message);
            _mockBookDal.Verify(b => b.Update(It.IsAny<Book>()), Times.Once);
        }
    }
}
