using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.NoteDetail;
using Entities.DTOs.ShareDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ShareManager : IShareService
    {
        IShareDal _shareDal;
        IMapper _mapper;
        INoteDal _noteDal;
        private readonly IUserDal _userDal;
        //  private DataContext _context;

        public ShareManager(IShareDal shareDal, IMapper mapper, INoteDal noteDal, IUserDal userDal)
        {
            _shareDal = shareDal;
            _mapper = mapper;
           // _context = dataContext;
            _noteDal = noteDal;
            _userDal = userDal;
 
        }

        public IResult Add(CreateShareDto createShareDto)
        {
            // var note = _context.Notes.Find(createShareDto.NoteId);
            IResult result = BusinessRules.Run(CheckIfNoteIsShared(createShareDto.NoteId), SendNoteShare(createShareDto.SharedWithUserId, createShareDto.NoteId));
           // var note = _noteDal.Get(n => n.UserId == id);
          // var sharedUser=_userDal.Get(createShareDto.)
            if (result != null)
            {
                return result;
            }
            var newShare = _mapper.Map<Share>(createShareDto);
            _shareDal.Add(newShare);
            return new SuccessResult();
            /*if (note != null && note.IsShared == true)
            {
                var newShare = _mapper.Map<Share>(createShareDto);
                _shareDal.Add(newShare);
                return new SuccessResult();
            }
            return new ErrorResult(Messages.NoteIsNotShared);*/


        }

        public IResult Delete(int id)
        {
            var share=_shareDal.Get(s=>s.Id == id);
            _shareDal.Delete(share);
            return new SuccessResult();
        }

        public IDataResult<List<Share>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<ShareDto> GetById(int id)
        {
            var share = _shareDal.Get(s => s.Id == id);
            return new SuccessDataResult<ShareDto>(_mapper.Map<ShareDto>(share));    
        }

        public IResult Update(CreateShareDto createShareDto)
        {
            var newShare = _mapper.Map<Share>(createShareDto);
            _shareDal.Update(newShare);
            return new SuccessResult();
        }
        private IResult CheckIfNoteIsShared(int id)
        {
            var result= _noteDal.Get(n=>n.Id == id);
            if(result.IsShared==true)
            {
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }
        private IResult SendNoteShare(int userId, int id)
        {
            if (_noteDal == null)
            {
                throw new ArgumentNullException(nameof(_noteDal), "_noteDal is not initialized.");
            }
            if (_mapper == null)
            {
                throw new ArgumentNullException(nameof(_mapper), "_mapper is not initialized.");
            }

            // Kullanıcı ID'sine göre notu alın
            var note = _noteDal.Get(n => n.Id == id);

            // note nesnesinin null olup olmadığını kontrol edin
            if (note == null)
            {
                return new ErrorResult("Note not found for the given user ID.");
            }


            CreateNoteDTo newNote = new CreateNoteDTo()
            {
                //Id=note.Id,
                Description = note.Description,
                IsShared = note.IsShared,
                UserId = userId,
                BookId = note.BookId,
            };;
            var result = _mapper.Map<Note>(newNote);
           

             _noteDal.Add(result);
            return new SuccessResult();
            


        }
    }
}
