using Newtonsoft.Json;
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

        public QuestionBasedTest()
        {
            Explanations = CreateExplanations();
        }

        [JsonConstructor]
        public QuestionBasedTest(int useless)
        {

        }

        private List<ScoreExplanation> CreateExplanations()
        {
            // это проценты
            List<ScoreExplanation> explanations = new List<ScoreExplanation>
            {
                new ScoreExplanation(0, 24, "Ой, в ваших взаимотношениях будет много ссор и неприятностей("),
                new ScoreExplanation(25, 49, "У вас есть шансы на будущее, но их мало..."),
                new ScoreExplanation(50, 74, "Вы достаточно хорошо знаете друг друга"),
                new ScoreExplanation(75, 100, "Ого! Да вы просто созданы друг для друга!"),
            };
            return explanations;
        }

        private string GetExplanationByScore(int score)
        {
            string answer = null;
            foreach (var e in Explanations)
            {
                if ((e.ReturnScore(true) <= score) & (score <= e.ReturnScore(false)))
                {
                    answer = e.ReturnExplanation();
                }
            }
            return answer;
        }


        public override Result GetResult(object valueOne, object valueTwo)
        {
            var firstValue = valueOne as List<int>;
            var secondValue = valueTwo as List<int>;

            QuestionBasedResult result = new QuestionBasedResult();
            result.Score = ComputingScorePercentage(firstValue, secondValue);
            result.Explanation = GetExplanationByScore(result.Score);
            result.NumberOfMatches = NumOfMatches(firstValue, secondValue);

            return result;
        }

        private int ComputingScorePercentage(List<int> firstList, List<int> secondList)
        {

            int maxScore = 0;
            foreach (var q in Questions)
            {
                maxScore += q.ReturnWeight();
            }

            int score = 0;
            for (int i = 0; i < firstList.Count; i++)
            {
                if (firstList[i] == secondList[i])
                {
                    score += Questions[i].ReturnWeight();
                }
            }
            return Convert.ToInt32(score * 100 / maxScore);
        }

        private int NumOfMatches(List<int> firstList, List<int> secondList)
        {
            int matches = 0;
            for (int i = 0; i < firstList.Count; i++)
            {
                if (firstList[i] == secondList[i])
                {
                    matches += 1;
                }
            }
            return matches;
        }
    }
}
