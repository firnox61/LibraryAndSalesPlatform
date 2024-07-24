using AutoMapper;
using Business.Concrete;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Integration
{
    public class BookManagerTests
    {
        private readonly Mock<IBookDal> _mockBookDal;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHostEnvironment> _mockEnvironment;
        private readonly BookManager _bookManager;

        public BookManagerTests()
        {
            _mockBookDal = new Mock<IBookDal>();
            _mockMapper = new Mock<IMapper>();
            _mockEnvironment = new Mock<IHostEnvironment>();
            _mockEnvironment.Setup(env => env.ContentRootPath).Returns("some/path");
            _bookManager = new BookManager(_mockBookDal.Object, _mockMapper.Object, _mockEnvironment.Object);
        }

        [Fact]
        public void Add_ValidBook_ReturnsSuccessResult()
        {
            
            var bookCreateDto = new BookCreateDto
            {
                Title = "Test Book",
                Genre = "Test Genre",
                Description = "Test Description",
                CoverImageUrl = new Mock<IFormFile>().Object,
                ShelfId = 1
            };

            _mockMapper.Setup(m => m.Map<Book>(It.IsAny<BookCreateDto>())).Returns(new Book());

            
            var result = _bookManager.Add(bookCreateDto);

            
            Assert.True(result.Success);
            Assert.Equal(Messages.BookAdd, result.Message);
        }

        [Fact]
        public void GetAll_ReturnsListOfBooks()
        {
           
            var books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Genre = "Genre 1" },
            new Book { Id = 2, Title = "Book 2", Genre = "Genre 2" }
        };

            var bookDtos = new List<BookListDto>
        {
            new BookListDto { Id = 1, Title = "Book 1", Genre = "Genre 1" },
            new BookListDto { Id = 2, Title = "Book 2", Genre = "Genre 2" }
        };
            //It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>())).Returns(books);
            _mockBookDal.Setup(dal => dal.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<Book, bool>>>())).Returns(books);
            _mockMapper.Setup(m => m.Map<List<BookListDto>>(books)).Returns(bookDtos);


            var result = _bookManager.GetAll();

            Assert.True(result.Success);
            Assert.Equal(2, result.Data.Count);
        }
    }
}