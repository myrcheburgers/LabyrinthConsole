using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Labyrinth_Console.RNG;

namespace Labyrinth_Console
{
    public abstract class Bestiary
    {
        public string name;
        public int level;
        public int hpmax;
        public int mpmax;
        public int atk;
        public int def;
        public int speed;

        //public abstract Creature Create(int id);

        #region old garbo
        //string name = "Goblin";
        //int level = 1;
        //int hpmax = 30;
        //int mpmax = 20;
        //int atk = 8;
        //int def = 5;
        //int speed = 30;



        //ICreature Create(int id)
        //{
        //    ICreature mob = new Creature(name, id, level, hpmax, mpmax, atk, def, speed);
        //    return mob;
        //}
        #endregion
    }

    public static class Goblin
    {
        //string name;
        //int level;
        //int hpmax;
        //int mpmax;
        //int atk;
        //int def;
        //int speed;

        //public Creature Create(int id)
        //{
        //    //ICreature goblin = new Creature(name, 1, level, hpmax, mpmax, atk, def, speed);

        //    //return goblin;
        //    //throw new NotImplementedException();
        //    name = "Goblin";
        //    level = 1;
        //    hpmax = 30;
        //    mpmax = 20;
        //    atk = 8;
        //    def = 5;
        //    speed = 30;
        //    return new Creature(name, id, level, hpmax, mpmax, atk, def, speed);
        //}
        

        public static string name = "Goblin";
        public static int _level = 1;
        public static int _hpmax = 30;
        public static int _mpmax = 20;
        public static int _atk = 8;
        public static int _def = 5;
        public static int _speed = 30;

        public static int level;
        public static int hpmax;
        public static int mpmax;
        public static int atk;
        public static int def;
        public static int speed;

        static int multiplier = 15;

        public static void AdjustStats()
        {
            RNG rng = new RNG();
            level = _level;
            hpmax = Convert.ToInt32(Math.Floor((Convert.ToDecimal(_hpmax + _hpmax * 0.01 * rng.rndInt(-1* multiplier, multiplier)))));
            mpmax = Convert.ToInt32(Math.Floor((Convert.ToDecimal(_mpmax + _mpmax * 0.01 * rng.rndInt(-1 * multiplier, multiplier)))));
            atk = Convert.ToInt32(Math.Floor((Convert.ToDecimal(_atk + _atk * 0.01 * rng.rndInt(-1 * multiplier, multiplier)))));
            def = Convert.ToInt32(Math.Floor((Convert.ToDecimal(_def + _def * 0.01 * rng.rndInt(-1 * multiplier, multiplier)))));
            speed = Convert.ToInt32(Math.Floor((Convert.ToDecimal(_speed + _speed * 0.01 * rng.rndInt(-1 * multiplier, multiplier)))));
        }
    }

    public class Goblin2
    {
        //string name;
        //int level;
        //int hpmax;
        //int mpmax;
        //int atk;
        //int def;
        //int speed;

        //public Creature Create(int id)
        //{
        //    //ICreature goblin = new Creature(name, 1, level, hpmax, mpmax, atk, def, speed);

        //    //return goblin;
        //    //throw new NotImplementedException();
        //    name = "Goblin";
        //    level = 1;
        //    hpmax = 30;
        //    mpmax = 20;
        //    atk = 8;
        //    def = 5;
        //    speed = 30;
        //    return new Creature(name, id, level, hpmax, mpmax, atk, def, speed);
        //}


        public string name = "Goblin";
        public int _level = 1;
        public int _hpmax = 30;
        public int _mpmax = 20;
        public int _atk = 8;
        public int _def = 5;
        public int _speed = 30;

        public int level;
        public int hpmax;
        public int mpmax;
        public int atk;
        public int def;
        public int speed;

        int multiplier = 15;

        public void AdjustStats()
        {
            RNG rng = new RNG();
            level = _level;
            hpmax = Convert.ToInt32(Math.Floor((Convert.ToDecimal(_hpmax + _hpmax * 0.01 * rng.rndInt(-1 * multiplier, multiplier)))));
            mpmax = Convert.ToInt32(Math.Floor((Convert.ToDecimal(_mpmax + _mpmax * 0.01 * rng.rndInt(-1 * multiplier, multiplier)))));
            atk = Convert.ToInt32(Math.Floor((Convert.ToDecimal(_atk + _atk * 0.01 * rng.rndInt(-1 * multiplier, multiplier)))));
            def = Convert.ToInt32(Math.Floor((Convert.ToDecimal(_def + _def * 0.01 * rng.rndInt(-1 * multiplier, multiplier)))));
            speed = Convert.ToInt32(Math.Floor((Convert.ToDecimal(_speed + _speed * 0.01 * rng.rndInt(-1 * multiplier, multiplier)))));
        }

        public static explicit operator Creature(Goblin2 obj)
        {
            Creature output = new Creature(obj.name, 0, obj.level, obj.hpmax, obj.mpmax, obj.atk, obj.def, obj.speed);
            //ID = 0 is a placeholder
            return output;
        }
    }
}