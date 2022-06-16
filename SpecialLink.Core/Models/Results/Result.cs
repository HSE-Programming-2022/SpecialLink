using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.Results
{
    public abstract class Result
    {
        public string TestName { get; set; }
        public int Score { get; set; }
        public string Explanation { get; set; }
    }
}
