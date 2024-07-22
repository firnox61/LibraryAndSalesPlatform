using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class FriendValidator:AbstractValidator<FriendShip>
    {
        public FriendValidator() 
        {
            RuleFor(f => f.Friend).NotEmpty();
            RuleFor(f => f.UserId).NotEmpty();

        }
    }
}
