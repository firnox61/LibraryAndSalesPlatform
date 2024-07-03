using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.BooksDetail
{
    public class BookFilterDto:IDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
    }
}
