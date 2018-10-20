using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    static class WeaponList
    {
        static Dictionary<string, Weapon> _dict = new Dictionary<string, Weapon>()
        {
            {"Unarmed", new Weapon("unarmed", 1, 75, 2)},
            {"Shortsword", new Weapon("shortsword", 5, 95, 1)}
        };
    }

    public class Weapon
    {
        public string name;

        public int dmg;
        public int acc;

        public int numAttacks;

        //constructor
        public Weapon(string _name, int _dmg, int _acc, int _numAttacks)
        {
            name = _name;
            dmg = _dmg;
            acc = _acc;
            numAttacks = _numAttacks;
        }

    }

    //public class Unarmed
    //{
    //    public string name = "Unarmed";
    //    public int dmg = 1;
    //    public int acc = 75;
    //    public int numAttacks = 2;

    //    public static explicit operator Weapon(Unarmed obj)
    //    {
    //        Weapon output = new Weapon() {  };
    //        //ID = 0 is a placeholder
    //        //alt implementation in Bestiary.Goblin2
    //        return output;
    //    }
    //}
}
