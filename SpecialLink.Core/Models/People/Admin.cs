using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.People
{
    public class Admin : Person
    {
        public DateTime LastLogin { get; set; }

        public Admin()
        {
            LastLogin = DateTime.Now;
        }
    }
}
