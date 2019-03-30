using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    public struct PlayerClass
    {
        public string name;
        public string abbreviation;

        // base stats
        public int baseHP;
        public int baseMP;
        public int baseATK;
        public int baseDEF;
        public int baseSpeed;

        // levelups
        public int growthHP;
        public int growthMP;
        public int growthATK;
        public int growthDEF;

        public PlayerClass(string name, string abbreviation, int baseHP, int baseMP, int baseATK, int baseDEF, int baseSpeed, int growthHP, int growthMP, int growthATK, int growthDEF)
        {
            this.name = name;
            this.abbreviation = abbreviation;
            this.baseHP = baseHP;
            this.baseMP = baseMP;
            this.baseATK = baseATK;
            this.baseDEF = baseDEF;
            this.baseSpeed = baseSpeed;
            this.growthHP = growthHP;
            this.growthMP = growthMP;
            this.growthATK = growthATK;
            this.growthDEF = growthDEF;
        }

        // player class presets
        static readonly PlayerClass debug = new PlayerClass("Debug Queen", "BUG", 9999, 9999, 999, 999, 999, 999, 999, 999, 999);
        static readonly PlayerClass warriorClass = new PlayerClass("Warrior", "WAR", 175, 25, 25, 25, 30, 30, 10, 30, 30);
        static readonly PlayerClass berserkerClass = new PlayerClass("Berserker", "BER", 190, 10, 35, 15, 30, 35, 5, 40, 20);
        static readonly PlayerClass mageClass = new PlayerClass("Mage", "MAG", 120, 80, 15, 15, 30, 10, 50, 20, 20);

        public static readonly string[] classNames = { "Warrior", "Berserker", "Mage" };

        public static PlayerClass DebugQueen { get { return debug; } }
        public static PlayerClass Warrior { get { return warriorClass; } }
        public static PlayerClass Berserker { get { return berserkerClass; } }
        public static PlayerClass Mage { get { return mageClass; } } // need to add magic elsewhere
    }
}
