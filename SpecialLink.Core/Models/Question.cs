using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public string FirstAnswer { get; set; }
        public string SecondAnswer { get; set; }
        private int Weight { get; set; }

        public int ReturnWeight()
        {
            return Weight;
        }

        public void ChangeWeight(int newWeight)
        {
            Weight = newWeight;
        }
    }
}
