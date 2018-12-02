using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    public static class ClassNames
    {
        public static string[] names = { "Warrior", "Berserker", "Mage" };

        public static readonly Dictionary<string, string> abbreviate = new Dictionary<string, string>()
        {
            {"Warrior", "WAR" },
            {"Berserker", "BER" },
            {"Mage", "MAG" }
        };
    }

    class PlayerClass
    {
        /**
         * From Character.cs:
         * 
         * Numbers notes... let's say:
         *      Growth -- 100 points divided between each stat... could totally use this to make a lulzy glass cannon build
         *      HP, MP -- 200 points divided between each (level 1)
         *      ATK/DEF -- 50? points divided between each (level 1)
         * 
         *      Could use MPmax for spell potency modifier since I don't feel like adding more stats (TODO: add Magic class)
         * 
         **/

        public class Warrior
        {
            public string name;
            public string job = "Warrior";

            public int hpmax = 175;
            public int mpmax = 25;

            public int atk = 25;
            public int def = 25;
            public int speed = 30;

            public MagicLearned magic = new MagicLearned();

            //levelups
            public int hpGrowth = 30;
            public int mpGrowth = 10;
            public int atkGrowth = 30;
            public int defGrowth = 30;

            //explicit type conversion
            public static explicit operator Character(Warrior obj)
            {
                Character output = new Character()
                {
                    name = obj.name,
                    hpmax = obj.hpmax,
                    mpmax = obj.mpmax,
                    atk = obj.atk,
                    def = obj.def,
                    speed = obj.speed,

                    magic = obj.magic,

                    hpGrowth = obj.hpGrowth,
                    mpGrowth = obj.mpGrowth,
                    atkGrowth = obj.atkGrowth,
                    defGrowth = obj.defGrowth,
                    job = obj.job
                };
                //ID = 0 is a placeholder
                //alt implementation in Bestiary.Goblin2
                return output;
            }
        }

        public class Berserker
        {
            public string name;
            public string job = "Berserker";

            public int hpmax = 190;
            public int mpmax = 10;

            public int atk = 35;
            public int def = 15;
            public int speed = 30;

            public MagicLearned magic = new MagicLearned();

            //levelups
            public int hpGrowth = 35;
            public int mpGrowth = 5;
            public int atkGrowth = 40;
            public int defGrowth = 20;

            //explicit type conversion
            public static explicit operator Character(Berserker obj)
            {
                Character output = new Character()
                {
                    name = obj.name,
                    hpmax = obj.hpmax,
                    mpmax = obj.mpmax,
                    atk = obj.atk,
                    def = obj.def,
                    speed = obj.speed,

                    magic = obj.magic,

                    hpGrowth = obj.hpGrowth,
                    mpGrowth = obj.mpGrowth,
                    atkGrowth = obj.atkGrowth,
                    defGrowth = obj.defGrowth,
                    job = obj.job
                };
                //ID = 0 is a placeholder
                //alt implementation in Bestiary.Goblin2
                return output;
            }
        }

        public class Mage
        {
            public string name;
            public string job = "Mage";

            public int hpmax = 120;
            public int mpmax = 80;

            public int atk = 15;
            public int def = 15;
            public int speed = 30;

            public MagicLearned magic = new MagicLearned();
            

            //levelups
            public int hpGrowth = 10;
            public int mpGrowth = 50;
            public int atkGrowth = 20;
            public int defGrowth = 20;

            //explicit type conversion
            public static explicit operator Character(Mage obj)
            {
                Character output = new Character() {
                    name = obj.name,
                    hpmax = obj.hpmax,
                    mpmax = obj.mpmax,
                    atk = obj.atk,
                    def = obj.def,
                    speed = obj.speed,

                    magic = obj.magic,

                    hpGrowth = obj.hpGrowth,
                    mpGrowth = obj.mpGrowth,
                    atkGrowth = obj.atkGrowth,
                    defGrowth = obj.defGrowth,
                    job = obj.job
                };
                //ID = 0 is a placeholder
                //alt implementation in Bestiary.Goblin2
                return output;
            }
        }
    }
}
