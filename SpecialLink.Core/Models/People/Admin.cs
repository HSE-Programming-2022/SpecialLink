﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.People
{
    class Admin : Person
    {
        public DateTime LastLogin { get; set; }
    }
}
