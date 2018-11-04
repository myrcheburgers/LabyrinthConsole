using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    public class Character
    {
        /**
         * Numbers notes... let's say:
         *      Growth -- 100 points divided between each stat... could totally use this to make a lulzy glass cannon build
         *      HP, MP -- 200 points divided between each (level 1)
         *      ATK/DEF -- 50? points divided between each (level 1)
         * 
         *      Could use MPmax for spell potency modifier since I don't feel like adding more stats (TODO: add Magic class)
         *       => something like Cast(Magic spellName, Creature caster, Creature target) [caster and target may or may not be same entity]
         * 
         **/

        public string name;
        //public string id;

        public string job;

        public int level;
        public int exp;

        //public enum vitals
        //{
        //    hp,
        //    hpmax,
        //    mp,
        //    mpmax
        //}

        //vitals
        public int hp;
        public int hpmax;
        public int mp;
        public int mpmax;

        //public enum stats
        //{
        //    atk,
        //    def,
        //    speed
        //}
        //stats

        public int atk;
        public int def;
        public int speed;

        //growth rates for levelups -- let's say a total of 100 for all categories?
        public int hpGrowth;
        public int mpGrowth;
        public int atkGrowth;
        public int defGrowth;

        //equipment
        public Weapon mainhand;

        void EquipWeapon(Weapon weapon)
        {
            //name, dmg, acc, numAttacks
            mainhand.name = weapon.name;
            mainhand.dmg = weapon.dmg;
            mainhand.acc = weapon.acc;
            mainhand.numAttacks = weapon.numAttacks;
        }
        void LevelUp()
        {
            //when implemented, iterate once for each level gained
            //TODO: implement EXP curve, base level on total EXP
            hpmax += hpGrowth;
            mpmax += mpGrowth;
            atk += Convert.ToInt32(Math.Floor(Convert.ToSingle(atkGrowth) * 0.1));
            def += Convert.ToInt32(Math.Floor(Convert.ToSingle(defGrowth) * 0.1));
        }

        
        //For battlers:
        public static explicit operator Creature(Character obj)
        {
            Creature output = new Creature(obj.name, 0, obj.level, obj.hpmax, obj.mpmax, obj.atk, obj.def, obj.speed);
            //ID = 0 is a placeholder
            return output;
        }
    }
}
