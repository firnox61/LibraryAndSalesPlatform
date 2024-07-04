using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.CrossCuttingCorcerns.Logging;
using Core.Utilities;
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
        private IFriendShipService _friendShipService;
        private IFriendShipDal _friendShipDal;
        private readonly ILoggerService _logger;
        public ShareManager(IShareDal shareDal, IMapper mapper, INoteDal noteDal, IUserDal userDal
            , IFriendShipService friendShipService, IFriendShipDal friendShipDal, ILoggerService loggerService)
        {
            _shareDal = shareDal;
            _mapper = mapper;
            // _context = dataContext;
            _noteDal = noteDal;
            _userDal = userDal;
            _friendShipService = friendShipService;
            _friendShipDal = friendShipDal;
            _logger = loggerService;
        }

        public IResult Add(CreateShareDto createShareDto)
        {
            _logger.LogInfo("Share Add  method called");
            // var note = _context.Notes.Find(createShareDto.NoteId);
            IResult result = BusinessRules.Run(CheckIfNoteIsShared(createShareDto.NoteId),SharePrivateSettings(createShareDto));
            if (result != null)//kurala uymayan bir durum oluşmuşsa
            {
                _logger.LogWarning("Share Add Method not Completed");
                return result;
                
               // return new ErrorResult();
            }
            SendNoteShare(createShareDto.SharedWithUserId, createShareDto.NoteId);
            _logger.LogInfo("Share Add method completed");
            return new SuccessResult(Messages.ShareAdd);

           
        }

        public IResult Delete(int id)
        {
            var share=_shareDal.Get(s=>s.Id == id);
            _shareDal.Delete(share);
            return new SuccessResult(Messages.ShareDelete);
        }

        public IDataResult<List<Share>> GetAll()
        {
            var shares=_shareDal.GetAll();
            return new SuccessDataResult<List<Share>>(shares);
        }

        public IDataResult<ShareDto> GetById(int id)
        {
            var share = _shareDal.Get(s => s.Id == id);
            return new SuccessDataResult<ShareDto>(_mapper.Map<ShareDto>(share), Messages.ShareDetail);    
        }

        public IResult Update(CreateShareDto createShareDto)
        {
            var newShare = _mapper.Map<Share>(createShareDto);
            _shareDal.Update(newShare);
            return new SuccessResult(Messages.ShareUpdate);
        }
        private IResult CheckIfNoteIsShared(int id)
        {
            var result= _noteDal.Get(n=>n.Id == id);
            if(result.IsShared==true)
            {
                return new SuccessResult(Messages.ShareTrue);
            }
            else
            {
                return new ErrorResult(Messages.ShareFail);
            }
        }
        private Result SharePrivateSettings(CreateShareDto createShareDto)
        {
            var newShare = _mapper.Map<Share>(createShareDto);
            if(newShare.Privacy==SharePrivacy.Public)
            {
                //_shareDal.Add(newShare);
                return new SuccessResult();
            }
            else if(newShare.Privacy==SharePrivacy.FriendsOnly)
            {
                var isFriend = _friendShipDal.Get(f => f.FriendId == newShare.SharedWithUserId && f.FriendId == createShareDto.SharedWithUserId) != null;

                if (isFriend)
                {
                   // _shareDal.Add(newShare);
                    return new SuccessResult();
                }
                else
                {
                    return new ErrorResult(Messages.NotFriend);
                }
            }
            else
            {
                return new ErrorResult(Messages.ShareNot);
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
            return new SuccessResult(Messages.ShareTrue);
            


        }
    }
}
