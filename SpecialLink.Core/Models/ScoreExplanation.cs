using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models
{
    public class ScoreExplanation
    {
        private int LowestScore { get; set; }
        private int HighestScore { get; set; }
        private string Explanation { get; set; }

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
