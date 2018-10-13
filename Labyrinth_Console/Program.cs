using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    class Program
    {
        //Character hero = new Character();
        static bool exit = false;

        static void Main(string[] args)
        {
            //bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Hello World!");
                Console.ReadKey();

                Start();
            }
            
        }

        static void Start()
        {
            Character hero = new Character();
            

            Console.WriteLine("Input character name.");
            hero.name = Console.ReadLine();
            if (hero.name.Length < 2)
            {
                hero.name = "Sir Douchebag";
            } else if (hero.name.ToLower() == "exit")
            {
                hero.name = "irrelevant. Good bye.";
                exit = true;
            }

            Console.WriteLine("Your name is {0}.", hero.name);

            Console.WriteLine("Creature test:");
            //original:
            //Goblin.AdjustStats();
            //ICreature goblin = new Creature(Goblin.name, 1, Goblin.level, Goblin.hpmax, Goblin.mpmax, Goblin.atk, Goblin.def, Goblin.speed);

            //Console.WriteLine("Name: {0} Level: {1} ID: {2} Max HP: {3} Current HP: {4} Max MP: {5} Current MP: {6} ATK: {7} DEF: {8} SPD: {9}", goblin.name, goblin.level, goblin.id, goblin.hpmax, goblin.hp, goblin.mpmax, goblin.mp, goblin.atk, goblin.def, goblin.speed);

            //Goblin2 goblin2 = new Goblin2();
            //goblin2.AdjustStats();
            //Creature gob2 = (Creature)goblin2;

            //with explicit casting:
            Goblin2 preGoblin = new Goblin2();
            preGoblin.AdjustStats();
            Creature goblin = (Creature)preGoblin;
            goblin.id = 99;
            Console.WriteLine("Name: {0} Level: {1} ID: {2} Max HP: {3} Current HP: {4} Max MP: {5} Current MP: {6} ATK: {7} DEF: {8} SPD: {9}", goblin.name, goblin.level, goblin.id, goblin.hpmax, goblin.hp, goblin.mpmax, goblin.mp, goblin.atk, goblin.def, goblin.speed);
            
            Battle testBattle = new Battle();

            testBattle.Start(hero, goblin);
        }
    }
}
