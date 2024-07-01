using Core.Utilities.Result;
using Entities.DTOs.NoteDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface INoteService
    {
        IDataResult<List<CreateNoteDTo>> GetAll();
        IDataResult<CreateNoteDTo> GetById(int id);
        IResult Add(CreateNoteDTo createNoteDTo);
        IResult Update(CreateNoteDTo createNoteDTo);
        IResult Delete(int id);
        
        

    }
}
