using SpecialLink.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core.Models.People
{
    public class User : Person
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageSource { get; set; }
        public List<Result> Results { get; set; }
        readonly List<string> source = new List<string>()
        {
            "profile_1.jpg",
            "profile_2.jpg",
            "profile_3.jpg",
            "profile_4.jpg",
            "profile_5.jpg",
            "profile_6.jpg",
            "profile_7.jpg",
            "profile_8.jpg",
            "profile_9.jpg",
            "profile_10.jpg",
            "profile_11.jpg",
            "profile_12.jpg"
        };

        public User()
        {
            Random rnd = new Random();
            ImageSource = source[rnd.Next(0, 12)];
            Results = new List<Result>();
        }
    }
}
