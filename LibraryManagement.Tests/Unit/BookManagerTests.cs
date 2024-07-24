using AutoMapper;
using Business.Concrete;
using Business.Constants;
using Core.CrossCuttingCorcerns.Logging;
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

namespace LibraryManagement.Tests.Unit
{
    public class BookManagerTests
    {
        private readonly BookManager _bookManager;
        private readonly Mock<IBookDal> _mockBookDal;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHostEnvironment> _mockEnvironment;

        public BookManagerTests()
        {
            _mockBookDal = new Mock<IBookDal>();
            _mockMapper = new Mock<IMapper>();
            _mockEnvironment = new Mock<IHostEnvironment>();
            _bookManager = new BookManager(_mockBookDal.Object, _mockMapper.Object, _mockEnvironment.Object);
        }

        [Fact]
        public void Add_BookCreateDto_Method_Test()
        {
            // Arrange
            var bookCreateDto = new BookCreateDto
            {
                Title = "Test Book",
                Genre = "Test Genre",
                Description = "Test Description",
                CoverImageUrl = new FormFile(new MemoryStream(), 0, 0, null, "test.jpg"),
                ShelfId = 1
            };
            _mockMapper.Setup(m => m.Map<Book>(bookCreateDto)).Returns(new Book());

            // Act
            var result = _bookManager.Add(bookCreateDto);

            // Assert
            _mockBookDal.Verify(m => m.Add(It.IsAny<Book>()), Times.Once);
            Assert.True(result.Success);
            Assert.Equal(Messages.BookAdd, result.Message);
        }

        [Fact]
        public void Delete_Book_Exists_Method_Test()
        {
            // Arrange
            int bookId = 1;
            var book = new Book { Id = bookId, Title = "Test Book" };
            _mockBookDal.Setup(m => m.Get(b => b.Id == bookId)).Returns(book);

            // Act
            var result = _bookManager.Delete(bookId);

            // Assert
            _mockBookDal.Verify(m => m.Delete(book), Times.Once);
            Assert.True(result.Success);
        }

        [Fact]
        public void GetAll_Should_Return_SuccessDataResult_With_BookListDtos()
        {
            // Arrange
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "Test Book", Genre = "Test Genre", Description = "Test Description", ShelfId = 1 }
        };
            var bookListDtos = new List<BookListDto>
        {
            new BookListDto { Id = 1, Title = "Test Book", Genre = "Test Genre", Description = "Test Description", ShelfId = 1 }
        };
            //It.IsAny<System.Linq.Expressions.Expression<Func<FriendShip, bool>>>()))
            _mockBookDal.Setup(m => m.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>())).Returns(books);
            _mockMapper.Setup(m => m.Map<List<BookListDto>>(books)).Returns(bookListDtos);

            // Act
            var result = _bookManager.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(bookListDtos, result.Data);
        }

        [Fact]
        public void GetById_BookDetailDto_Method_Test()
        {
            // Arrange
            int bookId = 1;
            var book = new Book { Id = bookId, Title = "Test Book", Genre = "Test Genre", Description = "Test Description", ShelfId = 1 };
            var bookDetailDto = new BookDetailDto { Id = bookId, Title = "Test Book", Genre = "Test Genre", Description = "Test Description", ShelfId = 1 };
            _mockBookDal.Setup(m => m.Get(b => b.Id == bookId)).Returns(book);
            _mockMapper.Setup(m => m.Map<BookDetailDto>(book)).Returns(bookDetailDto);

            // Act
            var result = _bookManager.GetById(bookId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(bookDetailDto, result.Data);
            Assert.Equal(Messages.BookDetail, result.Message);
        }

        [Fact]
        public void Update_BookUpdateDto_Method_Test()
        {
            // Arrange
            var bookUpdateDto = new BookUpdateDto
            {
                Id = 1,
                Title = "Updated Test Book",
                Genre = "Updated Test Genre",
                Description = "Updated Test Description",
                ShelfId = 1
            };
            var book = new Book { Id = 1, Title = "Updated Test Book", Genre = "Updated Test Genre", Description = "Updated Test Description", ShelfId = 1 };
            _mockMapper.Setup(m => m.Map<Book>(bookUpdateDto)).Returns(book);

            // Act
            var result = _bookManager.Update(bookUpdateDto);

            // Assert
            _mockBookDal.Verify(m => m.Update(book), Times.Once);
            Assert.True(result.Success);
            Assert.Equal(Messages.BookUpdate, result.Message);
        }

        /*[Fact]
        public void GetFilter_Should_Return_SuccessDataResult_With_FilteredBooks()
        {
            // Arrange
            var bookFilterDto = new BookFilterDto { Title = "Test Book", Genre = "Test Genre" };
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "Test Book", Genre = "Test Genre", Description = "Test Description", ShelfId = 1 },
            new Book { Id = 2, Title = "Another Book", Genre = "Another Genre", Description = "Another Description", ShelfId = 1 }
        };
            var filteredBooks = books.Where(b => b.Title.Contains(bookFilterDto.Title) && b.Genre.Contains(bookFilterDto.Genre)).ToList();
            _mockBookDal.Setup(m => m.GetAll()).Returns(books);

            // Act
            var result = _bookManager.GetFilter(bookFilterDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(filteredBooks, result.Data);
        }*/

        [Fact]
        public void GetBookShelfDetail_Method_Test()
        {
            // Arrange
            var bookShelfDetails = new List<BookShelfDetailDto>
        {
            new BookShelfDetailDto { Id = 1, Title = "Test Book", Genre = "Test Genre", Description = "Test Description", LayerNumber = 1, SequenceNumber = 1, SectionCode = "A1" }
        };
            _mockBookDal.Setup(m => m.BookShelfDetail()).Returns(bookShelfDetails);

            // Act
            var result = _bookManager.GetBookShelfDetail();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(bookShelfDetails, result.Data);
            Assert.Equal(Messages.BookShelfDetail, result.Message);
        }
    }
}
