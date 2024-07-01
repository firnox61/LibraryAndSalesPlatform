﻿using AutoMapper;
using Azure.Core;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.UsersDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private readonly IMapper _mapper;
        IUserDal _userDal;

        public AuthManager(IUserService userService, IMapper mapper, IUserDal userDal)
        {
            _userService = userService;
            _mapper = mapper;
            _userDal = userDal;
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> Login(LoginUserDto loginUserDto)
        {
            var userToCheck = _userService.GetByMail(loginUserDto.Email);
            if (userToCheck == null)//kullanıcı varmı diye kontrol ediyoruz
            {
                return new ErrorDataResult<User>();
            }
            //kullanıcıyı bulduk biz onun gönderdiği şifreyi salt ve hashleyip veritabanındaki ile aynı mı diye kontrol edicez
            if (!HashingHelper.VerifyPasswordHash(loginUserDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLog);
        }

        public IDataResult<User> Register(RegisterUserDto registerUserDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
           var user=_mapper.Map<User>(registerUserDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userService.Add(user);
            return new SuccessDataResult<User>(user);
        }
        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult("Kullanıcı mevcuttur");
            }
            return new SuccessResult();
        }
    }
}
