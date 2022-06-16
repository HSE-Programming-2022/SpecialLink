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
            QuestionBasedResult result = new QuestionBasedResult();
            result.Score = CompareAndCalculate(firstValue, secondValue);
            foreach (var explanation in Explanations)
            {
                if ((result.Score >= explanation.LowestScore) && (result.Score < explanation.HighestScore))
                {
                    result.Explanation = explanation.Explanation;
                }
            }
            result.ScoreResult = ScoreResult(firstValue, secondValue);
            return result;
        }

        private int CompareAndCalculate(List<int> answer1, List<int> answer2)
        {
            int differencePoints = 0;
            int generalPoints = 0;
            for (int i = 0; i < answer1.Count; i++)
            {
                differencePoints += Math.Abs(answer1[i] - answer2[i]) * Questions[i].Weight;
                generalPoints += Questions[i].Weight;
            }
            int innerScore = (generalPoints - differencePoints)* 100 / generalPoints;
            return innerScore;
        }

        private int ScoreResult(List<int> answer1, List<int> answer2)
        {
            int scoreResult = 0;
            for (int i = 0; i < answer1.Count; i++)
            {
                if (answer1[i] == answer2[i])
                {
                    scoreResult += 1;
                }
            }
            return scoreResult;
        }
    }
}
