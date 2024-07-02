using Core.Utilities.Result;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using Entities.DTOs.ShareDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IShareService
    {
        IDataResult<List<Share>> GetAll();
        IDataResult<ShareDto> GetById(int id);
        IResult Add(CreateShareDto createShareDto);
        IResult Update(CreateShareDto createShareDto);
        IResult Delete(int id);
    }
}
