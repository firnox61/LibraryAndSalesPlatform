using Core;

namespace Entities.DTOs.UsersDetail
{
    public class RegisterUserDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
