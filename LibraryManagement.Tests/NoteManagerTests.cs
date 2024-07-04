using AutoMapper;
using Business.Concrete;
using Business.Constants;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.NoteDetail;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests
{
    public class NoteManagerTests
    {
        private readonly Mock<INoteDal> _mockNoteDal;
        private readonly Mock<IMapper> _mockMapper;
        private readonly NoteManager _noteManager;

        public NoteManagerTests()
        {
            _mockNoteDal = new Mock<INoteDal>();
            _mockMapper = new Mock<IMapper>();
            _noteManager = new NoteManager(_mockNoteDal.Object, _mockMapper.Object);
        }

        [Fact]
        public void Add_ShouldReturnSuccessResult_WhenNoteIsValid()
        {
            // Arrange
            var createNoteDto = new CreateNoteDTo { Description = "Test Note", UserId = 1, BookId = 1, IsShared = false };
            var note = new Note { Description = "Test Note", UserId = 1, BookId = 1, IsShared = false };
            _mockMapper.Setup(m => m.Map<Note>(It.IsAny<CreateNoteDTo>())).Returns(note);
            _mockNoteDal.Setup(n => n.Add(It.IsAny<Note>())).Verifiable();

            // Act
            var result = _noteManager.Add(createNoteDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(Messages.NoteAdd, result.Message);
            _mockNoteDal.Verify(n => n.Add(It.IsAny<Note>()), Times.Once);
        }

        [Fact]
        public void Delete_ShouldReturnSuccessResult_WhenNoteExists()
        {
            // Arrange
            var note = new Note { Id = 1 };
            _mockNoteDal.Setup(n => n.Get(It.IsAny<Expression<Func<Note, bool>>>())).Returns(note);
            _mockNoteDal.Setup(n => n.Delete(It.IsAny<Note>())).Verifiable();

            // Act
            var result = _noteManager.Delete(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(Messages.NoteDelete, result.Message);
            _mockNoteDal.Verify(n => n.Delete(It.IsAny<Note>()), Times.Once);
        }

        [Fact]
        public void GetById_ShouldReturnSuccessDataResult_WithNoteDto()
        {
            // Arrange
            var note = new Note { Id = 1, Description = "Test Note" };
            var createNoteDto = new CreateNoteDTo { Id = 1, Description = "Test Note" };
            _mockNoteDal.Setup(n => n.Get(It.IsAny<Expression<Func<Note, bool>>>())).Returns(note);
            _mockMapper.Setup(m => m.Map<CreateNoteDTo>(It.IsAny<Note>())).Returns(createNoteDto);

            // Act
            var result = _noteManager.GetById(1);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(createNoteDto.Description, result.Data.Description);
        }

       /* [Fact]
        public void GetAll_ShouldReturnSuccessDataResult_WithListOfNoteDtos()
        {
            // Arrange
            var notes = new List<Note> { new Note { Id = 1, Description = "Note 1" }, new Note { Id = 2, Description = "Note 2" } };
            var noteDtos = new List<CreateNoteDTo> { new CreateNoteDTo { Id = 1, Description = "Note 1" }, new CreateNoteDTo { Id = 2, Description = "Note 2" } };
            _mockNoteDal.Setup(n => n.GetAll()).Returns(notes);
            _mockMapper.Setup(m => m.Map<List<CreateNoteDTo>>(It.IsAny<List<Note>>())).Returns(noteDtos);

            // Act
            var result = _noteManager.GetAll();

            // Assert
            Assert.True(result.Success);
            Assert.Equal(noteDtos.Count, result.Data.Count);
        }*/

        [Fact]
        public void Update_ShouldReturnSuccessResult_WhenNoteIsValid()
        {
            // Arrange
            var createNoteDto = new CreateNoteDTo { Id = 1, Description = "Updated Note", UserId = 1, BookId = 1, IsShared = false };
            var note = new Note { Id = 1, Description = "Updated Note", UserId = 1, BookId = 1, IsShared = false };
            _mockMapper.Setup(m => m.Map<Note>(It.IsAny<CreateNoteDTo>())).Returns(note);
            _mockNoteDal.Setup(n => n.Update(It.IsAny<Note>())).Verifiable();

            // Act
            var result = _noteManager.Update(createNoteDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(Messages.NoteUpdate, result.Message);
            _mockNoteDal.Verify(n => n.Update(It.IsAny<Note>()), Times.Once);
        }

        [Fact]
        public void GetAllByUserId_ShouldReturnSuccessDataResult_WithListOfNoteDetailDtos()
        {
            // Arrange
            var userId = 1;
            var notes = new List<Note> { new Note { Id = 1, Description = "User's Note 1", UserId = userId }, new Note { Id = 2, Description = "User's Note 2", UserId = userId } };
            var noteDetailDtos = new List<NoteDetailDto> { new NoteDetailDto { Id = 1, Description = "User's Note 1" }, new NoteDetailDto { Id = 2, Description = "User's Note 2" } };
            _mockNoteDal.Setup(n => n.GetAll(It.IsAny<Expression<Func<Note, bool>>>())).Returns(notes);
            _mockMapper.Setup(m => m.Map<List<NoteDetailDto>>(It.IsAny<List<Note>>())).Returns(noteDetailDtos);

            // Act
            var result = _noteManager.GetAllByUserId(userId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(noteDetailDtos.Count, result.Data.Count);
        }

        [Fact]
        public void GetAllByBookId_ShouldReturnSuccessDataResult_WithListOfNoteDetailDtos()
        {
            // Arrange
            var bookId = 1;
            var notes = new List<Note> { new Note { Id = 1, Description = "Book's Note 1", BookId = bookId }, new Note { Id = 2, Description = "Book's Note 2", BookId = bookId } };
            var noteDetailDtos = new List<NoteDetailDto> { new NoteDetailDto { Id = 1, Description = "Book's Note 1" }, new NoteDetailDto { Id = 2, Description = "Book's Note 2" } };
            _mockNoteDal.Setup(n => n.GetAll(It.IsAny<Expression<Func<Note, bool>>>())).Returns(notes);
            _mockMapper.Setup(m => m.Map<List<NoteDetailDto>>(It.IsAny<List<Note>>())).Returns(noteDetailDtos);

            // Act
            var result = _noteManager.GetAllByBookId(bookId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(noteDetailDtos.Count, result.Data.Count);
        }
    }
}
