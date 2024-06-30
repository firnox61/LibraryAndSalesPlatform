using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.NoteDetail
{
    public class CreateNoteDTo : IDto
    {
        public string Description { get; set; }
        public bool IsShared { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
