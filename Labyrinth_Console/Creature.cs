using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    class Creature
    {
        public string name;
        public string id;
        public int level;
        public Vitals vitals;
        public bool isPlayer;
        public MagicLearned magic;
        public ElementalResistance eRes;
        public PhysicalResistance pRes;
        

        public Creature(string name, string id, int level, int hpmax, int mpmax, int atk, int def, int speed, bool isPlayer, MagicLearned magic)
        {
            this.name = name;
            this.id = id;
            this.level = level;
            this.vitals.hpmax = hpmax;
            this.vitals.hp = hpmax;
            this.vitals.mpmax = mpmax;
            this.vitals.mp = mpmax;
            this.vitals.atk = atk;
            this.vitals.def = def;
            this.vitals.speed = speed;
            this.isPlayer = isPlayer;
            this.magic = magic;
        }

        public Creature(string name, string id, int level, Vitals vitals, bool isPlayer, MagicLearned magic, ElementalResistance eRes, PhysicalResistance pRes)
        {
            this.name = name;
            this.id = id;
            this.level = level;
            this.vitals = vitals;
            this.isPlayer = isPlayer;
            this.magic = magic;
            this.eRes = eRes;
            this.pRes = pRes;
        }

        public Creature()
        {
            this.name = "NULL";
            this.id = "NULL_ID";
            this.vitals.hpmax = 1;
            this.vitals.hp = vitals.mpmax;
            this.vitals.mpmax = 1;
            this.vitals.mp = vitals.mpmax;
            this.vitals.atk = 25;
            this.vitals.def = 25;
            this.vitals.speed = 30;
            this.isPlayer = false;
            this.eRes = ElementalResistance.Standard;
            this.pRes = PhysicalResistance.standard;
        }
        
        public void SetStatsFromCSV()
        {
            // To be implemented
        }

        int AdjustStat(int baseStat)
        {
            int multiplier = 15;
            Random rnd = new Random();
            int newStat = (int)Math.Floor(baseStat + baseStat * 0.01 * rnd.Next(-1 * multiplier, multiplier));

            return newStat;
        }

        public Vitals AdjustVitals(Vitals baseVitals)
        {
            //int multiplier = 15;
            //Random rnd = new Random();
            Vitals newVitals = baseVitals;
            newVitals.hpmax = AdjustStat(baseVitals.hpmax);
            newVitals.hp = newVitals.hpmax;
            newVitals.mpmax = AdjustStat(baseVitals.mpmax);
            newVitals.mp = newVitals.mpmax;
            newVitals.atk = AdjustStat(baseVitals.atk);
            newVitals.def = AdjustStat(baseVitals.def);
            newVitals.speed = AdjustStat(baseVitals.speed);

            return newVitals;
        }

        public void AdjustVitals()
        {
            //this
            this.vitals.hpmax = AdjustStat(this.vitals.hpmax);
            this.vitals.hp = this.vitals.hpmax;
            this.vitals.mpmax = AdjustStat(this.vitals.mpmax);
            this.vitals.mp = this.vitals.mpmax;
            this.vitals.atk = AdjustStat(this.vitals.atk);
            this.vitals.def = AdjustStat(this.vitals.def);
            this.vitals.speed = AdjustStat(this.vitals.speed);
        }

        static readonly Creature goblinCreature = new Creature("Goblin", "NULL_ID", 1, new Vitals(30, 20, 8, 5, 30), false, new MagicLearned(), ElementalResistance.Standard, PhysicalResistance.standard);
        static readonly Creature dootdoot = new Creature("Mr. Skeltal", "NULL_ID", 1, new Vitals(110, 5, 2, 16, 20), false, new MagicLearned(), ElementalResistance.Standard, PhysicalResistance.standard);

        public static Creature goblin { get { return goblinCreature; } }
        public static Creature spookyScarySkeleton { get { return dootdoot; } }
    }
}
