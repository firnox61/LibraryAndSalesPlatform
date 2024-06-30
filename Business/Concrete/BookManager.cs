using AutoMapper;
using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
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

        public BookManager(IBookDal bookDal, IMapper mapper)
        {
            _bookDal = bookDal;
            _mapper = mapper;
        }

        public IResult Add(BookCreateDto bookCreateUpdateDto)
        {
           var newBook=_mapper.Map<Book>(bookCreateUpdateDto);
            _bookDal.Add(newBook);
            return new SuccessResult(); 
        }

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
            return new SuccessDataResult<BookDetailDto>(_mapper.Map<BookDetailDto>(book));

         //   return new SuccessDataResult<BookDetailDto>(_bookDal.Get(b=>b.Id == id));   
        }

        public IResult Update(BookUpdateDto bookCreateUpdateDto)
        {
            var newBook = _mapper.Map<Book>(bookCreateUpdateDto);
            _bookDal.Update(newBook);
            return new SuccessResult();
        }
    }
}
