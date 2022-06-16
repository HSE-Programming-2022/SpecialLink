using SpecialLink.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.Tests
{
    public class QuestionBasedTest : Test
    {
        public List<Question> Questions { get; set; }
        public List<ScoreExplanation> Explanations { get; set; }

        public override Result GetResult(object valueOne, object valueTwo)
        {
            var firstValue = valueOne as List<int>;
            var secondValue = valueTwo as List<int>;
            // подсчет процента совместимости
            Result result = new QuestionBasedResult();
            return result;
        }
    }
}
