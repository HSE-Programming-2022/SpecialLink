using SpecialLink.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.Tests
{
    abstract class Test
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageSource { get; set; }
        public int AmountOfTimesTaken { get; set; }

        public abstract Result GetResult(object firstValue, object secondValue);
    }
}
