using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.Results
{
    public class ComputingOrAnswerBasedResult : Result
    {
        public string FirstValue { get; set; }
        public string SecondValue { get; set; }
    }
}
