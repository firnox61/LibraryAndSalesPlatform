using Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.BooksDetail
{
    public class BookShelfDetailDto:IDto
    {
         public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
       // public IFormFile CoverImageUrl { get; set; }
        public int LayerNumber { get; set; }
        public int SequenceNumber { get; set; }
        public string SectionCode { get; set; }
        //  public int ShelfId { get; set; }
    }
}
