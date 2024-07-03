using Core.Entites;
using Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Share:IEntity
    {
        public int Id { get; set; }
       // public int SharedWithUserId { get; set; }
        //public DateTime SharedAt { get; set; }
        public int NoteId { get; set; }
        [JsonIgnore]
        public Note Note { get; set; }
        public int SharedWithUserId { get; set; } // Paylaşılan kullanıcının Id'si
        [JsonIgnore]
        public User SharedWithUser { get; set; } // Navigation property
        public SharePrivacy Privacy { get; set; }
        //  public User SharedWithUser { get; set; }
    }
}
