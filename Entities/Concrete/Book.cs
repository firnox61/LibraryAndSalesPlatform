using Core.Entites;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
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
        public string? CoverImageUrl { get; set; }
        public int ShelfId { get; set; }
        [JsonIgnore]
        public Shelf Shelf { get; set; }
        // Navigation properties
        [JsonIgnore]
        public ICollection<Note> Notes { get; set; }

    }
}
