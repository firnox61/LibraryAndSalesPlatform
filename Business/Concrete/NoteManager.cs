using AutoMapper;
using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.NoteDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class NoteManager : INoteService
    {
        INoteDal _noteDal;
        IMapper _mapper;

        public NoteManager(INoteDal noteDal, IMapper mapper)
        {
            _noteDal = noteDal;
            _mapper = mapper;
        }

        public IResult Add(CreateNoteDTo createNoteDTo)
        {
           var newNote=_mapper.Map<Note>(createNoteDTo);
            _noteDal.Add(newNote);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var note= _noteDal.Get(n=>n.Id==id);
            _noteDal.Delete(note);
            return new SuccessResult();
        }

        public IDataResult<CreateNoteDTo> GetById(int id)
        {
            var note = _noteDal.Get(n => n.Id == id);
            return new SuccessDataResult<CreateNoteDTo>(_mapper.Map<CreateNoteDTo>(note));
        }

        public IDataResult<List<CreateNoteDTo>> GetAll()
        {
            var notes= _noteDal.GetAll();
            return new SuccessDataResult<List<CreateNoteDTo>>(_mapper.Map<List<CreateNoteDTo>>(notes));
        }

        public IResult Update(CreateNoteDTo createNoteDTo)
        {
            var newNote = _mapper.Map<Note>(createNoteDTo);
            _noteDal.Update(newNote);
            return new SuccessResult();
        }
    }
}
