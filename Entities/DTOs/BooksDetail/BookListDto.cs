using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.BooksDetail
{
    public class BookListDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string CoverImageUrl { get; set; }
        public int ShelfId { get; set; }
    }
}
