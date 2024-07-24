using Entities.Concrete;
using Entities.DTOs.BooksDetail;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class BookValidator : AbstractValidator<BookCreateDto>
    {
        public BookValidator() {

            RuleFor(b => b.Title).MinimumLength(2).NotEmpty();
            RuleFor(b => b.Description).MinimumLength(2).NotEmpty();
            RuleFor(b => b.Genre).MinimumLength(2).NotEmpty();
            RuleFor(b => b.Title).Must(StartWithA);//.WithMessage("")





        }
        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
