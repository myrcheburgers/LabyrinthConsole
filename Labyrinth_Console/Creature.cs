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

        public Creature(string name, string id, Vitals vitals, bool isPlayer, MagicLearned magic)
        {
            this.name = name;
            this.id = id;
            this.vitals = vitals;
            this.isPlayer = isPlayer;
            this.magic = magic;
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

        }

        public void SetStatsFromCSV()
        {
            // To be implemented
        }
    }
}
