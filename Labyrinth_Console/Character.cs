using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    class Character
    {
        public string name;
        public string id;

        public string job;

        public int level;
        public int exp;


        //public int hp;
        //public int hpmax;
        //public int mp;
        //public int mpmax;
        //public int atk;
        //public int def;
        //public int speed;
        public Vitals vitals;

        //growth rates for levelups -- let's say a total of 100 for all categories?
        // moved to playerClass struct
        //public int hpGrowth;
        //public int mpGrowth;
        //public int atkGrowth;
        //public int defGrowth;

        //equipment
        public Weapon mainhand;

        //magic
        public MagicLearned magic = new MagicLearned();

        public PhysicalResistance pRes;
        public ElementalResistance eRes;

        public PlayerClass playerClass;

        public void InitializeVitals()
        {
            vitals.hpmax = playerClass.baseHP;
            vitals.hp = vitals.hpmax;
            vitals.mpmax = playerClass.baseMP;
            vitals.mp = vitals.mpmax;
            vitals.atk = playerClass.baseATK;
            vitals.def = playerClass.baseDEF;
            vitals.speed = playerClass.baseSpeed;
        }
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
            vitals.hpmax += playerClass.growthHP;
            vitals.mpmax += playerClass.growthMP;
            vitals.atk += Convert.ToInt32(Math.Floor(Convert.ToSingle(playerClass.growthATK) * 0.1));
            vitals.def += Convert.ToInt32(Math.Floor(Convert.ToSingle(playerClass.growthDEF) * 0.1));
        }

        
        //For battlers:
        public static explicit operator Creature(Character obj)
        {
            Creature output = new Creature(obj.name, obj.id, obj.level, obj.vitals, true, obj.magic, obj.eRes, obj.pRes); //add resistances, class etc
            //ID = 0 is a placeholder
            return output;
        }
    }
}
