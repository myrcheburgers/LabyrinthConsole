using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    public class RNG
    {
        public Random rnd = new Random();

        public int rndInt(int min, int max)
        {
            return rnd.Next(min, max);
        }

        public int seed()
        {
            return rndInt(100000000, 999999999);
        }
    }
}
