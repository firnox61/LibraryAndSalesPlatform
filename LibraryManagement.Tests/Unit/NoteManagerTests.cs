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

namespace LibraryManagement.Tests.Unit
{
    public class NoteManagerTests
    {
        private readonly NoteManager _noteManager;
        private readonly Mock<INoteDal> _mockNoteDal;
        private readonly Mock<IMapper> _mockMapper;

        public NoteManagerTests()
        {
            _mockNoteDal = new Mock<INoteDal>();
            _mockMapper = new Mock<IMapper>();
            _noteManager = new NoteManager(_mockNoteDal.Object, _mockMapper.Object);
        }

        [Fact]
        public void Add_CreateNoteDto_Is_Valid_Method_Test()
        {
            // Arrange
            var createNoteDto = new CreateNoteDTo {Description = "Test Note", IsShared = true, UserId = 1, BookId = 1 };
            var note = new Note { Id = 1, Description = "Test Note", IsShared = true, UserId = 1, BookId = 1 };

            _mockMapper.Setup(m => m.Map<Note>(createNoteDto)).Returns(note);

            // Act
            var result = _noteManager.Add(createNoteDto);

            // Assert
            _mockNoteDal.Verify(m => m.Add(note), Times.Once);
            Assert.True(result.Success);
            Assert.Equal(Messages.NoteAdd, result.Message);
        }

        [Fact]
        public void Delete_Note_Exists_Method_Test()
        {
            // Arrange
            int noteId = 1;
            var note = new Note { Id = noteId, Description = "Test Note", IsShared = true, UserId = 1, BookId = 1 };
            _mockNoteDal.Setup(m => m.Get(n => n.Id == noteId)).Returns(note);

            // Act
            var result = _noteManager.Delete(noteId);

            // Assert
            _mockNoteDal.Verify(m => m.Delete(note), Times.Once);
            Assert.True(result.Success);
            Assert.Equal(Messages.NoteDelete, result.Message);
        }

        [Fact]
        public void GetById_Success_CreateNoteDto_Method_Test()
        {
            // Arrange
            int noteId = 1;
            var note = new Note { Id = noteId, Description = "Test Note", IsShared = true, UserId = 1, BookId = 1 };
            var noteDetailDto = new NoteDetailDto { Id = noteId, Description = "Test Note", IsShared = true, UserId = 1, BookId = 1 };
            _mockNoteDal.Setup(m => m.Get(n => n.Id == noteId)).Returns(note);
            _mockMapper.Setup(m => m.Map<NoteDetailDto>(note)).Returns(noteDetailDto);

            // Act
            var result = _noteManager.GetById(noteId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(noteDetailDto, result.Data);
            Assert.Equal(Messages.NoteDetail, result.Message);
        }

         [Fact]
         public void GetAll_Should_Return_SuccessDataResult_With_CreateNoteDtos()
         {
             // Arrange
             var notes = new List<Note>
         {
             new Note { Id = 1, Description = "Test Note", IsShared = true, UserId = 1, BookId = 1 }
         };
             var noteDetailDtos = new List<NoteDetailDto>
         {
             new NoteDetailDto { Id = 1, Description = "Test Note", IsShared = true, UserId = 1, BookId = 1 }
         };
            //   _mockFriendShipDal.Setup(m => m.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<FriendShip, bool>>>())).Returns(friendships);

            _mockNoteDal.Setup(m => m.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<Note, bool>>>())).Returns(notes);
             _mockMapper.Setup(m => m.Map<List<NoteDetailDto>>(notes)).Returns(noteDetailDtos);

             // Act
             var result = _noteManager.GetAll();

             // Assert
             Assert.True(result.Success);
             Assert.Equal(noteDetailDtos, result.Data);
             Assert.Equal(Messages.NoteList, result.Message);
         }

        [Fact]
        public void Update_CreateNoteDto_Method_Test()
        {
            // Arrange
            var updateNoteDto = new UpdateNoteDto { Id = 1, Description = "Updated Test Note", IsShared = true, UserId = 1, BookId = 1 };
            var note = new Note { Id = 1, Description = "Updated Test Note", IsShared = true, UserId = 1, BookId = 1 };

            _mockMapper.Setup(m => m.Map<Note>(updateNoteDto)).Returns(note);

            // Act
            var result = _noteManager.Update(updateNoteDto);

            // Assert
            _mockNoteDal.Verify(m => m.Update(note), Times.Once);
            Assert.True(result.Success);
            Assert.Equal(Messages.NoteUpdate, result.Message);
        }

        [Fact]
        public void GetAllByUserId_NoteDetailDtos_Method_Test()
        {
            // Arrange
            int userId = 1;
            var notes = new List<Note>
        {
            new Note { Id = 1, Description = "Test Note", IsShared = true, UserId = userId, BookId = 1 }
        };
            var noteDetailDtos = new List<NoteDetailDto>
        {
            new NoteDetailDto { Id = 1, Description = "Test Note", IsShared = true, UserId = userId, BookId = 1 }
        };
            _mockNoteDal.Setup(m => m.GetAll(n => n.UserId == userId)).Returns(notes);
            _mockMapper.Setup(m => m.Map<List<NoteDetailDto>>(notes)).Returns(noteDetailDtos);

            // Act
            var result = _noteManager.GetAllByUserId(userId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(noteDetailDtos, result.Data);
            Assert.Equal(Messages.UserByNote, result.Message);
        }

        [Fact]
        public void GetAllByBookId_Note_Method_Test()
        {
            // Arrange
            int bookId = 1;
            var notes = new List<Note>
        {
            new Note { Id = 1, Description = "Test Note", IsShared = true, UserId = 1, BookId = bookId }
        };
            var noteDetailDtos = new List<NoteDetailDto>
        {
            new NoteDetailDto { Id = 1, Description = "Test Note", IsShared = true, UserId = 1, BookId = bookId }
        };
            _mockNoteDal.Setup(m => m.GetAll(n => n.BookId == bookId)).Returns(notes);
            _mockMapper.Setup(m => m.Map<List<NoteDetailDto>>(notes)).Returns(noteDetailDtos);

            // Act
            var result = _noteManager.GetAllByBookId(bookId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(noteDetailDtos, result.Data);
        }
    }
}
