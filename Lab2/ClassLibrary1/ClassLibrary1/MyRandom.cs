using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class MyRandom : Random
    {
        public MyRandom() : base() { }

        public MyRandom(int Seed) : base(Seed) { }

        public bool Next(bool lowerBound, bool upperBound)
        {
            if (lowerBound & upperBound)
            {
                return true;
            }
            else if (!lowerBound & !upperBound)
            {
                return false;
            }
            
            Random rnd = new Random();
            int randomInt = rnd.Next(0, 2);

            if (randomInt == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

            
        }
    }
}
