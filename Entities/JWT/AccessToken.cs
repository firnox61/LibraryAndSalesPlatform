﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.JWT
{
    public class AccessToken
    {
        public string Token { get; set; }//tokenşşmşz
        public DateTime Expiration { get; set; }//bitiş süremiz
    }
}
