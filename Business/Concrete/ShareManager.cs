using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
        private IShareDal _shareDal;
        private IMapper _mapper;
        private INoteDal _noteDal;
        private readonly IUserDal _userDal;
        private IFriendShipService _friendShipService;
        private IFriendShipDal _friendShipDal;
        public ShareManager(IShareDal shareDal, IMapper mapper, INoteDal noteDal, IUserDal userDal
            , IFriendShipService friendShipService, IFriendShipDal friendShipDal)
        {
            _shareDal = shareDal;
            _mapper = mapper;
            _noteDal = noteDal;
            _userDal = userDal;
            _friendShipService = friendShipService;
            _friendShipDal = friendShipDal;
          
        }
        [ValidationAspect(typeof(ShareValidator))]
        public IResult Add(CreateShareDto createShareDto)
        {
           
            IResult result = BusinessRules.Run(CheckIfNoteIsShared(createShareDto.NoteId),SharePrivateSettings(createShareDto));
            if (result != null)//kurala uymayan bir durum oluşmuşsa
            {
                return result;
                
            }
            SendNoteShare(createShareDto.SharedWithUserId, createShareDto.NoteId);
            var share = _mapper.Map<Share>(createShareDto);
            _shareDal.Add(share);
            return new SuccessResult(Messages.ShareAdd);

           
        }

        public IResult Delete(int id)
        {
            var share=_shareDal.Get(s=>s.Id == id);
            _shareDal.Delete(share);
            return new SuccessResult(Messages.ShareDelete);
        }

        public IDataResult<List<ShareDto>> GetAll()
        {
            var shares=_shareDal.GetAll();
            return new SuccessDataResult<List<ShareDto>>(_mapper.Map<List<ShareDto>>(shares));
        }

        public IDataResult<ShareDto> GetById(int id)
        {
            var share = _shareDal.Get(s => s.Id == id);
            return new SuccessDataResult<ShareDto>(_mapper.Map<ShareDto>(share), Messages.ShareDetail);    
        }
        [ValidationAspect(typeof(ShareValidator))]
        public IResult Update(ShareDto shareDto)
        {
            var newShare = _mapper.Map<Share>(shareDto);
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
            };
            var result = _mapper.Map<Note>(newNote);
           
            //_shareDal.Add()
             _noteDal.Add(result);
            return new SuccessResult(Messages.ShareTrue);
            


        }
    }
}
