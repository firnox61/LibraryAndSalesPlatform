using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using Entities.DTOs.ShelfDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IShelfService
    {
        IResult Add(CreateShelfDto createShelfDto);
        IDataResult<List<Shelf>> GetAll();
        IDataResult<Shelf> GetById(int id);
    }
}
