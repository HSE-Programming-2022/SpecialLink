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
        readonly List<string> _explanationTexts1 = new List<string>()
        {
            "Ой, в ваших взаимотношениях может быть много ссор и неприятностей(",
            "Результат неутешительный, возможно вам стоит получше узнать друг друга и найти общую почву!",
            "Это не самый лучший результат. Впрочем, результат теста (даже такого классного) меркнет и бледнеет по сравнению с хорошо налаженной коммуникацией.",
            "Уровень вашей совместимости не очень высок, но не стоит расстраиваться. Поговорите со своим партнером, обсудите ваши ответы. Это возможность сблизиться :)",
            "Процент вашей совместимости невелик, однако, мы верим, что сила дружбы или любви победит все! Не отчаивайтесь!"
        };
        readonly List<string> _explanationTexts2 = new List<string>()
        {
            "У вас есть шансы, но их мало...",
            "Нельзя сказать, что ваша совместимость оптимальна, но все же она не ноль :)",
            "Невозможно идеально подходить каждому человеку в этом мире. У нас у всех разные вкусы и ценности!",
            "Основа ваших отношений не слишком прочна, однако, успех ваших отношений зависит исключительно от вас.",
            "В ваших отношениях могут быть разногласия и, время от времени, даже ссоры. Важно с самого начала обсудить значимые для вас вещи!"
        };
        readonly List<string> _explanationTexts3 = new List<string>()
        {
            "Достаточно хорошая совместимость, но не исключены недопонимания. Почаще общайтесь на важные для вас темы!",
            "Это очень даже неплохой результат! Ваши отношения имеют неплохие шансы быть очень успешными.",
            "Ваши отношения имеют под собой прочную основу. И это чудесно, не так ли?",
            "Процент вашей совместимости достаточно высок, однако, не стоит забывать, что это не гарант идеальных отношений :)",
            "Неплохо, неплохо! Ваши отношения стабильны, в них почти отсутствуют разногласия. Что может быть лучше?)"
        };
        readonly List<string> _explanationTexts4 = new List<string>()
        {
            "Ого! Да вы просто созданы друг для друга! Ну, или вы просто созданы друг для друга в рамках параметров этого теста ))",
            "Идеальная совместимость -- редкость. Вам очень повезло!",
            "Вам повезло найти 'своего' человека. Цените его/ее/их!",
            "Это невероятный результат. Уровень вашей совместимости вызывает белую зависть у окружающих. А мы просто очень рады за вас.",
            "Поздравляем, ваши отношения имеют феноменальные шансы на успех, если вы, конечно, честно проходили наш тест)) Цените вашего друга или партнера!"
        };

        public QuestionBasedTest()
        {
            Explanations = CreateExplanations();
            ImageSource = "icon_2.jpg";
        }

        [JsonConstructor]
        public QuestionBasedTest(int useless)
        {

        }

        private List<ScoreExplanation> CreateExplanations()
        {
            // это проценты
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
