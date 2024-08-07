﻿using Core.Entites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Note:IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsShared { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        public ICollection<Share> Shares { get; set; }




    }
}
