using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.BooksDetail
{
    public class BookUpdateDto : IDto
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ShelfLocation { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
