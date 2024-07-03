using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator() {

            RuleFor(b => b.Title).MinimumLength(2).NotEmpty();
            RuleFor(b => b.Description).MinimumLength(2).NotEmpty();
            RuleFor(b => b.Genre).MinimumLength(2).NotEmpty();



        }
    }
}
