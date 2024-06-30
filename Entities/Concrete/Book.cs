using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ShelfLocation { get; set; }
        public string CoverImageUrl { get; set; }

        public ICollection<Note> Notes { get; set; }

    }
}
