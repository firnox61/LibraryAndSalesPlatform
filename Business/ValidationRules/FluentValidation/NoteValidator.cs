using Business.Constants;
using Entities.Concrete;
using Entities.DTOs.NoteDetail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class NoteValidator:AbstractValidator<CreateNoteDTo>//fluentvalidationdan geliyor
    {
        public NoteValidator()
        {
            RuleFor(c => c.Description).MinimumLength(2);
            RuleFor(c=>c.IsShared).NotEmpty();
          
            


        }

      /*  private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }*/
    }
}
