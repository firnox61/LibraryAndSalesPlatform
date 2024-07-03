using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IShelfService
    {
        IResult Add(Shelf shelf);
        IDataResult<List<Shelf>> GetAll();
        IDataResult<Shelf> GetById(int id);
    }
}
