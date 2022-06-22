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

        readonly List<string> _explanationTexts1 = new List<string>()
        {
            "Ой, в ваших взаимотношениях может быть много ссор и неприятностей(",
            "Результат неутешительный, в ваших отношениях возможны споры и ссоры. Будьте аккуратней друг с другом!",
            "Это не самый лучший результат. Впрочем, результат теста на совместимость меркнет и бледнеет по сравнению с хорошо налаженной коммуникацией.",
            "Уровень вашей совместимости не очень высок, но не стоит расстраиваться. Наш алгоритм не может предсказать все :)",
            "Процент вашей совместимости невелик, однако, мы верим, что сила дружбы или любви победит все! Не отчаивайтесь!"
        };
        readonly List<string> _explanationTexts2 = new List<string>()
        {
            "У вас есть шансы, но их мало...",
            "Нельзя сказать, что ваша совместимость оптимальна, но все же она не ноль :)",
            "Невозможно идеально подходить каждому человеку в этом мире. И все же, не стоит расстраиваться, вы сами творите свою судьбу!",
            "Основа ваших отношений не слишком прочна, однако, успех ваших отношений зависит исключительно от вас.",
            "В ваших отношениях могут быть разногласия и, время от времени, даже ссоры. Не пренебрегайте проявлениями ваших чувств по отношению к вашему партнеру или другу!"
        };
        readonly List<string> _explanationTexts3 = new List<string>()
        {
            "Достаточно хорошая совместимость, но не исключены недопонимания. Не забывайте уделять внимание вашему партнеру или другу.",
            "Это очень даже неплохой результат! Ваши отношения могут быть очень успешными.",
            "Ваши отношения имеют под собой прочную основу. И это чудесно, не так ли?",
            "Процент вашей совместимости достаточно высок, однако, не стоит забывать, что проявлять внимание к партнеру тоже чрезвычайно важно.",
            "Неплохо, неплохо! Ваши отношения стабильны, в них почти отсутствуют неприятности. Что может быть лучше?)"
        };
        readonly List<string> _explanationTexts4 = new List<string>()
        {
            "Ого! Да вы просто созданы друг для друга!",
            "Идеальная совместимость -- редкость. Вам очень повезло!",
            "Вам повезло найти 'своего' человека. Цените его/ее/их!",
            "Это невероятный результат. Уровень вашей совместимости вызывает белую зависть у окружающих.",
            "Поздравляем, ваши отношения имеют феноменальные шансы на успех. Цените вашего друга или партнера!"
        };

        public ComputationBasedTest()
        {
            Explanations = CreateExplanations();
            ImageSource = "icon_3.jpg";
        }

        [JsonConstructor]
        public ComputationBasedTest(int useless)
        {
            ImageSource = "icon_3.jpg";
        }

        private List<ScoreExplanation> CreateExplanations()
        {
            Random rnd = new Random();
            List<ScoreExplanation> explanations = new List<ScoreExplanation>
            {
                new ScoreExplanation(0, 24, _explanationTexts1[rnd.Next(0,5)]),
                new ScoreExplanation(25, 49, _explanationTexts2[rnd.Next(0,5)]),
                new ScoreExplanation(50, 74, _explanationTexts3[rnd.Next(0,5)]),
                new ScoreExplanation(75, 100, _explanationTexts4[rnd.Next(0,5)]),
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
