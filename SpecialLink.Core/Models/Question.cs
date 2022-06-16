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
        public int Weight { get; set; }
    }
}
