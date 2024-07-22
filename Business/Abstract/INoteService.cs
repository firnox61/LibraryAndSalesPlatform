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
        IDataResult<List<NoteDetailDto>> GetAll();
        IDataResult<NoteDetailDto> GetById(int id);
        IResult Add(CreateNoteDTo createNoteDTo);
        IResult Update(UpdateNoteDto updateNoteDto);
        IResult Delete(int id);
        IDataResult<List<NoteDetailDto>> GetAllByUserId(int id);
        IDataResult<List<NoteDetailDto>> GetAllByBookId(int id);
      



    }
}
