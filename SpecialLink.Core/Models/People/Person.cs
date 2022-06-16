using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.People
{
    public abstract class Person
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
