using SpecialLink.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.Tests
{
    public class AnswerBasedTest : Test
    {
        public List<Combination> Combinations { get; set; }

        public override Result GetResult(object valueOne, object valueTwo)
        {
            var firstValue = valueOne as string;
            var secondValue = valueTwo as string;
            Result result = new ComputingOrAnswerBasedResult();
            foreach (var combination in Combinations)
            {
                if (((firstValue == combination.FirstValue) && (secondValue == combination.SecondValue))||
                        ((firstValue == combination.SecondValue) && (secondValue == combination.FirstValue)))
                {
                    result = new ComputingOrAnswerBasedResult()
                    {
                        FirstValue = firstValue,
                        SecondValue = secondValue,
                        Score = combination.Score,
                        Explanation = combination.Explanation
                    };
                }
            }
            return result;
        }
    }
}
