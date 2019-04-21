using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    static class Status
    {
        public static bool blind;
        public static int sightRadius = 5;

        public static int ExpToLevel(int exp)
        {
            int _level = exp / 100;
            return _level;
        }

        public static int LevelToExp(int level)
        {
            int _exp = level * 100;
            return _exp;
        }
    }
}
