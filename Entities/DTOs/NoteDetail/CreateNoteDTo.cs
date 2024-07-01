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
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsShared { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        
    }
}
/*
 *  public int Id { get; set; }
        public string Description { get; set; }
        public bool IsShared { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
       public Book Book { get; set; }
 */