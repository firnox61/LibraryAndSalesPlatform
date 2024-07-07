using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Core.Utilities.Result;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.UsersDetail;
using Entities.JWT;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Tests.Unit
{
    public class AuthManagerTests
    {
        private readonly AuthManager _authManager;
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUserDal> _mockUserDal;
        private readonly Mock<ITokenHelper> _mockTokenHelper;

        public AuthManagerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockMapper = new Mock<IMapper>();
            _mockUserDal = new Mock<IUserDal>();
            _mockTokenHelper = new Mock<ITokenHelper>();
            _authManager = new AuthManager(_mockUserService.Object, _mockMapper.Object, _mockUserDal.Object, _mockTokenHelper.Object);
        }

        [Fact]
        public void CreateAccessToken_Method_Test()
        {
            var user = new User { Id = 1, Email = "test@test.com" };
            var claims = new List<OperationClaim> { new OperationClaim { Id = 1, Name = "Admin" } };
            var accessToken = new AccessToken { Token = "testToken", Expiration = DateTime.Now.AddHours(1) };

            _mockUserService.Setup(m => m.GetClaims(user)).Returns(claims);
            _mockTokenHelper.Setup(m => m.CreateToken(user, claims)).Returns(accessToken);

            var result = _authManager.CreateAccessToken(user);

            Assert.True(result.Success);
            Assert.Equal(accessToken, result.Data);
            Assert.Equal(Messages.AccessTokenCreated, result.Message);
        }

        [Fact]
        public void Login_Success_Method_Test()
        {
            var loginUserDto = new LoginUserDto { Email = "test@test.com", Password = "password" };
            var user = new User { Id = 1, Email = "test@test.com", PasswordHash = new byte[1], PasswordSalt = new byte[1] };

            _mockUserService.Setup(m => m.GetByMail(loginUserDto.Email)).Returns(user);
            HashingHelper.CreatePasswordHash("password", out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var result = _authManager.Login(loginUserDto);

            Assert.True(result.Success);
            Assert.Equal(user, result.Data);
            Assert.Equal(Messages.SuccessfulLog, result.Message);
        }

        [Fact]
        public void Login_User_Not_Found_Method_Test()
        {
            var loginUserDto = new LoginUserDto { Email = "test@test.com", Password = "password" };

            _mockUserService.Setup(m => m.GetByMail(loginUserDto.Email)).Returns((User)null);

            var result = _authManager.Login(loginUserDto);

            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public void Login_Password_Is_Invalid_Method_Test()
        {
            var loginUserDto = new LoginUserDto { Email = "test@test.com", Password = "wrongpassword" };
            var user = new User { Id = 1, Email = "test@test.com", PasswordHash = new byte[1], PasswordSalt = new byte[1] };

            _mockUserService.Setup(m => m.GetByMail(loginUserDto.Email)).Returns(user);
            HashingHelper.CreatePasswordHash("password", out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            var result = _authManager.Login(loginUserDto);

            Assert.False(result.Success);
            Assert.Equal(Messages.PasswordError, result.Message);
            Assert.Null(result.Data);
        }

        [Fact]
        public void Register_Succes_Method_Test()
        {
            var registerUserDto = new RegisterUserDto
            {
                Email = "test@test.com",
                Password = "password",
                FirstName = "John",
                LastName = "Doe"
            };
            var user = new User
            {
                Id = 1,
                Email = "test@test.com",
                FirstName = "John",
                LastName = "Doe",
                Status = true,
                PasswordHash = new byte[1],
                PasswordSalt = new byte[1]
            };

            _mockMapper.Setup(m => m.Map<User>(registerUserDto)).Returns(user);
            _mockUserService.Setup(m => m.Add(It.IsAny<User>())).Returns(new SuccessResult(Messages.UserAdded));

            var result = _authManager.Register(registerUserDto, registerUserDto.Password);

            Assert.True(result.Success);
            Assert.Equal(user, result.Data);
            Assert.Equal(Messages.UserRegistered, result.Message);
        }

        [Fact]
        public void UserExists_When_User_Already_Exists_Method_Test()
        {
            var email = "test@test.com";
            var user = new User { Id = 1, Email = email };

            _mockUserService.Setup(m => m.GetByMail(email)).Returns(user);

            var result = _authManager.UserExists(email);

            Assert.False(result.Success);
            Assert.Equal("Kullanıcı mevcuttur", result.Message);
        }

        [Fact]
        public void UserExists_When_User_Does_Not_Exist_Method_Test()
        {
            var email = "test@test.com";

            _mockUserService.Setup(m => m.GetByMail(email)).Returns((User)null);

            var result = _authManager.UserExists(email);

            Assert.True(result.Success);
        }
    }
}
