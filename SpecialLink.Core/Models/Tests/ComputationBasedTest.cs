using SpecialLink.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.Tests
{
    public class ComputationBasedTest : Test
    {
        public List<ScoreExplanation> Explanations { get; set; }

        public override Result GetResult(object valueOne, object valueTwo)
        {
            var firstValue = valueOne as string;
            var secondValue = valueTwo as string;
            // супер умные расчеты вики
            ComputingOrAnswerBasedResult result = new ComputingOrAnswerBasedResult();
            return result;
        }
    }
}
