using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Tests
{
    class ZodiacTest
    {
        public string FirstSign { get; set; }
        public string SecondSign { get; set; }
        public int Score { get; set; }
        public string Explanation { get; set; }

        public ZodiacTest(string signOne, string signTwo, int percentage, string text)
        {
            FirstSign = signOne;
            SecondSign = signTwo;
            Score = percentage;
            Explanation = text;
        }
    }
}
