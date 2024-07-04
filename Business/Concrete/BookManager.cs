using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingCorcerns.Logging;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Business.Concrete
{
    public class BookManager : IBookService
    {
        IBookDal _bookDal;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _environment;
        private readonly ILoggerService _logger;

        public BookManager(IBookDal bookDal, IMapper mapper, IHostEnvironment environment, ILoggerService loggerService)
        {
            _bookDal = bookDal;
            _mapper = mapper;
            _environment = environment;
            _logger = loggerService;
        }
        [ValidationAspect(typeof(BookValidator))]
        public IResult Add(BookCreateDto bookCreateUpdateDto)
        {
            IResult result = BusinessRules.Run(BookAddImage(bookCreateUpdateDto));




            return new SuccessResult(Messages.BookAdd);
        }
        [ValidationAspect(typeof(BookValidator))]
        public IResult Delete(int id)
        {
            var book = _bookDal.Get(b => b.Id == id);
            _bookDal.Delete(book);
            return new SuccessResult();
        }

        public IDataResult<List<BookListDto>> GetAll()
        {
           var books= _bookDal.GetAll();

            // throw new NotImplementedException();
           // var book = _mapper.Map<List<Book>>(BookListDto);
             return new SuccessDataResult<List<BookListDto>>(_mapper.Map<List<BookListDto>>(books));
        }

        public IDataResult<BookDetailDto> GetById(int id)
        {
            var book=_bookDal.Get(b=>b.Id == id);
            return new SuccessDataResult<BookDetailDto>(_mapper.Map<BookDetailDto>(book),Messages.BookDetail);

         //   return new SuccessDataResult<BookDetailDto>(_bookDal.Get(b=>b.Id == id));   
        }
        [ValidationAspect(typeof(BookValidator))]
        public IResult Update(BookUpdateDto bookCreateUpdateDto)
        {
            var newBook = _mapper.Map<Book>(bookCreateUpdateDto);
            _bookDal.Update(newBook);
            return new SuccessResult(Messages.BookUpdate);
        }
        private IResult BookAddImage(BookCreateDto bookCreateUpdateDto)
        {
            var uploadsFolder = Path.Combine(_environment.ContentRootPath, "wwwroot/uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(bookCreateUpdateDto.CoverImageUrl.FileName)}";


            //var uniqueFileName = $"{Guid.NewGuid()}_{bookCreateUpdateDto.CoverImageUrl.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                bookCreateUpdateDto.CoverImageUrl.CopyTo(fileStream);
            }
            var newBook = _mapper.Map<Book>(bookCreateUpdateDto);
            newBook.CoverImageUrl = $"/uploads/{uniqueFileName}";
            _bookDal.Add(newBook);
            return new SuccessResult();
            //return new SuccessResult();
        }

        public IDataResult<List<Book>> GetFilter(BookFilterDto bookFilterDto)
        {
            _logger.LogInfo("GetFilter method called");

            var books = _bookDal.GetAll();

            if (!string.IsNullOrEmpty(bookFilterDto.Title))
            {
                books = books.Where(b => b.Title.Contains(bookFilterDto.Title)).ToList();
            }

            if (!string.IsNullOrEmpty(bookFilterDto.Genre))
            {
                books = books.Where(b => b.Genre.Contains(bookFilterDto.Genre)).ToList();
            }
            _logger.LogInfo("GetFilter method completed");
            return new SuccessDataResult<List<Book>>(books);
        }
    }
}
