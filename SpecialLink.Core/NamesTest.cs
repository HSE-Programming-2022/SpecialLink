using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialLink.Core
{
    public class NamesTest
    {
        private string FirstName { get; }

        private string SecondName { get; }

        public NamesTest(string name1, string name2)
        {
            CultureInfo culture = new CultureInfo("ru-RU");
            FirstName = name1.Substring(0, 1).ToUpper(culture) + name1.Substring(1);
            SecondName = name2.Substring(0, 1).ToUpper(culture) + name2.Substring(1);
        }

        private int GiveRandomPercentage(int start, int end)
        {
            Random rnd = new Random();
            return rnd.Next(start, end + 1);  // creates a number in [start;end]
        }

        private bool CompareLengths()
        {
            if (Math.Abs(FirstName.Length - SecondName.Length) <= 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CompareNames()
        {

            if (FirstName[0] == SecondName[0])
            {
                if (CompareLengths())
                {
                    return GiveRandomPercentage(76, 100);
                }
                else
                {
                    return GiveRandomPercentage(51, 75);
                }
            }
            else
            {
                if (CompareLengths())
                {
                    return GiveRandomPercentage(26, 50);
                }
                else
                {
                    return GiveRandomPercentage(0, 25);
                }
            }
        }
    }
}
