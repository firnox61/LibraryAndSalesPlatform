
using Entities.Concrete;
using Entities.DTOs.UsersDetail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator:AbstractValidator<RegisterUserDto>
    {
        public UserValidator() {

            RuleFor(r => r.FirstName).NotEmpty();
            RuleFor(r=>r.Email).NotNull();

        }
    }
}
