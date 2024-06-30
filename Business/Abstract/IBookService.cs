using Core.Utilities.Result;
using Entities.DTOs.BooksDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBookService
    {
        IDataResult<List<BookListDto>> GetAll();
        IDataResult<BookDetailDto> GetById(int id);
        IResult Add(BookCreateDto bookCreateUpdateDto); 
        IResult Update(BookUpdateDto bookCreateUpdateDto);
        IResult Delete(int id);
    }
}
