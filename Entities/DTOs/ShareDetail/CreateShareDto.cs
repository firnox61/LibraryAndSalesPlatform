using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ShareDetail
{
    public class CreateShareDto : IDto
    {
        public int NoteId { get; set; }
        public int SharedWithUserId { get; set; }
    }
}
