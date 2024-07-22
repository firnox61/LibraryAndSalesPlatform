using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class ShelfValidator:AbstractValidator<Shelf>
    {
        public ShelfValidator() 
        {
            RuleFor(b => b.SectionCode).NotEmpty();
            RuleFor(b => b.LayerNumber).NotEmpty();
            RuleFor(b => b.SequenceNumber).NotEmpty();
        }
    }
}
