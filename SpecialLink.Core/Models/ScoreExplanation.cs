using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models
{
    public class ScoreExplanation
    {
        public int LowestScore { get; set; }
        public int HighestScore { get; set; }
        public string Explanation { get; set; }

        public ScoreExplanation(int bottom, int top, string text)
        {
            LowestScore = bottom;
            HighestScore = top;
            Explanation = text;
        }

        public int ReturnScore(bool isBottom)
        {
            if (isBottom is true)
            {
                return LowestScore;
            }
            else
            {
                return HighestScore;
            }
        }

        public string ReturnExplanation()
        {
            return Explanation;
        }

    }
}
