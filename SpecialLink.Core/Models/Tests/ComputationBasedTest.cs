using Newtonsoft.Json;
using SpecialLink.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.Tests
{
    public class ComputationBasedTest : Test
    {
        public List<ScoreExplanation> Explanations { get; set; }

        public ComputationBasedTest()
        {
            Explanations = CreateExplanations();
        }

        [JsonConstructor]
        public ComputationBasedTest(int useless)
        {
            ImageSource = "icon_3.jpg";
        }

        private List<ScoreExplanation> CreateExplanations()
        {
            List<ScoreExplanation> explanations = new List<ScoreExplanation>
            {
                new ScoreExplanation(0, 24, "Ой, в ваших взаимотношениях будет много ссор и неприятностей("),
                new ScoreExplanation(25, 49, "У вас есть шансы, но их мало..."),
                new ScoreExplanation(50, 74, "Достаточно хорошая совметимость, но не исключены недопонимания"),
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
            var firstValue = valueOne as string;
            var secondValue = valueTwo as string;

            firstValue = ToUpperReg(firstValue);
            secondValue = ToUpperReg(secondValue);

            ComputingOrAnswerBasedResult result = new ComputingOrAnswerBasedResult();
            result.FirstValue = firstValue;
            result.SecondValue = secondValue;
            result.Score = CompareNames(result.FirstValue, result.SecondValue);
            result.Explanation = GetExplanationByScore(result.Score);

            return result;
        }

        private string ToUpperReg(string name)
        {
            CultureInfo culture = new CultureInfo("ru-RU");
            return name.Substring(0, 1).ToUpper(culture) + name.Substring(1);
        }

        private int GiveRandomPercentage(int start, int end)
        {
            Random rnd = new Random();
            return rnd.Next(start, end + 1);  // creates a number in [start;end]
        }

        private bool CompareLengths(string firstName, string secondName)
        {
            if (Math.Abs(firstName.Length - secondName.Length) <= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int CompareNames(string firstName, string secondName)
        {

            if (firstName[0] == secondName[0])
            {
                if (CompareLengths(firstName, secondName))
                {
                    return GiveRandomPercentage(75, 100);
                }
                else
                {
                    return GiveRandomPercentage(50, 74);
                }
            }
            else
            {
                if (CompareLengths(firstName, secondName))
                {
                    return GiveRandomPercentage(25, 49);
                }
                else
                {
                    return GiveRandomPercentage(0, 24);
                }
            }
        }

    }
}
