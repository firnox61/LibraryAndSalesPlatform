using Entities.Concrete;
using Entities.DTOs.ShareDetail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ShareValidator:AbstractValidator<CreateShareDto>
    {
        public ShareValidator() 
        {
            RuleFor(p=>p.SharedWithUserId).NotNull().NotEmpty();
            RuleFor(p => p.Privacy).NotEmpty();
         

        }
    }
}
