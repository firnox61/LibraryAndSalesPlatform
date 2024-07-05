using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ShelfDetail
{
    public class CreateShelfDto:IDto
    {
        public int Id { get; set; }
        //public string Location { get; set; }
        public int LayerNumber { get; set; }
        public int SequenceNumber { get; set; }
        public string SectionCode { get; set; }
    }
}
