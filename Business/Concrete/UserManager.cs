using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using Entities.DTOs.UsersDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        private readonly IMapper _mapper;

        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        public IResult Delete(int id)
        {
            var result=_userDal.Get(u=>u.Id == id);
            _userDal.Delete(result);
            return new SuccessResult();
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult EditProfil(UserDto userDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdate);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }
        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult();
        }

        public List<OperationClaim> GetClaims(User user)
        {
          return _userDal.GetClaims(user);
        }

        public IDataResult<List<UserDto>> GetUserDetailDtos()
        {
            var users = _userDal.GetAll();
            var userDtos = _mapper.Map<List<UserDto>>(users);
            return new SuccessDataResult<List<UserDto>>(userDtos,Messages.UserDetailList);
        }

        public IDataResult<UserDto> GetByUserId(int userId)
        {
            var user = _userDal.Get(b => b.Id == userId);
            return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(user));
        }

        public IDataResult<List<UserDto>> GetAll()
        {
            var users = _userDal.GetAll();

            // throw new NotImplementedException();
            // var book = _mapper.Map<List<Book>>(BookListDto);
            return new SuccessDataResult<List<UserDto>>(_mapper.Map<List<UserDto>>(users));
        }
    }
}
