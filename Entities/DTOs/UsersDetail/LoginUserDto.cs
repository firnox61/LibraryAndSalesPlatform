using Core;

namespace Entities.DTOs.UsersDetail
{
    public class LoginUserDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
