using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class NoteValidator:AbstractValidator<Note>//fluentvalidationdan geliyor
    {
        public NoteValidator()
        {
           /* RuleFor(c => c.NoteName).MinimumLength(2);
            RuleFor(c=>c.NoteName).NotEmpty();
            RuleFor(c=>c.DailyPrice).NotEmpty();
            RuleFor(c=>c.DailyPrice).GreaterThan(0);
            RuleFor(c=>c.DailyPrice).GreaterThanOrEqualTo(10).When(c=>c.BrandId==1);*/
            // RuleFor(c => c.NoteName).Must(StartWithA).WithMessage("Araçlar A harfi ile başlamalı");
            


        }

      /*  private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }*/
    }
}
