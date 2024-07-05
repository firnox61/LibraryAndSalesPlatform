using Core.Entites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Shelf:IEntity
    {
        public int Id { get; set; }
       // public string Location { get; set; }
        public int LayerNumber { get; set; }
        public int SequenceNumber { get; set; }
        public string SectionCode { get; set; }
        // Detailed shelf location
        [JsonIgnore]                     // Navigation properties
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
