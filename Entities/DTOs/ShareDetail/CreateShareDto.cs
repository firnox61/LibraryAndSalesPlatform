using Core;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ShareDetail
{
    public class CreateShareDto : IDto
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int SharedWithUserId { get; set; }
        public SharePrivacy Privacy { get; set; }

    }
}
/* public int Id { get; set; }
       // public int SharedWithUserId { get; set; }
        //public DateTime SharedAt { get; set; }
        public int NoteId { get; set; }
        public Note Note { get; set; }
        public int SharedWithUserId { get; set; } // Paylaşılan kullanıcının Id'si
        public User SharedWithUser { get; set; } // Navigation property
        public SharePrivacy Privacy { get; set; }*/