using SpecialLink.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.People
{
    class User : Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageSource { get; set; }
        public List<Result> Results { get; set; }
    }
}
