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
            //Console.WriteLine("Input command.");
            string cmd;
            bool isValid = false;
            
            while (!isValid)
            {
                Console.WriteLine("Input command.");
                cmd = Console.ReadLine().ToLower();
                switch (cmd)
                {
                    case "_rtest":
                        {
                            isValid = true;
                            rTest();
                            break;
                        }
                    case "createparty":
                    case "makeparty":
                    case "party":
                        {
                            isValid = true;
                            BuildParty();
                            break;
                        }
                    case "mtest":
                    case "magictest":
                        {
                            isValid = true;
                            MagicTest();
                            break;
                        }
                    case "stats":
                    case "stattest":
                        {
                            isValid = true;
                            StatTests();
                            break;
                        }
                    case "exit":
                        {
                            isValid = true;
                            exit = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid command.");
                            break;
                        }
                }
            }
            
        }

        #region party builder
        static void BuildParty()
        {
            int partySize = 1;
            string cmd;
            bool isValid = false;

            while (!isValid)
            {
                Console.WriteLine("Choose a party size (1 - 3 members).");
                cmd = Console.ReadLine().ToLower();
                switch (cmd)
                {
                    case "one":
                    case "1":
                        {
                            partySize = 1;
                            isValid = true;
                            break;
                        }
                    case "two":
                    case "2":
                        {
                            partySize = 2;
                            isValid = true;
                            break;
                        }
                    case "three":
                    case "3":
                        {
                            partySize = 3;
                            isValid = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid command.");
                            Console.WriteLine("     Syntax:\n    n\n     where n = 1, 2, or 3");
                            break;
                        }
                }
            }

            if (isValid)
            {
                for (int i = 0; i < partySize; i++)
                {
                    Console.WriteLine("Creating character {0} of {1}.", i + 1, partySize);

                    CreateCharacter();
                }
            }

            if (isValid)
            {
                Console.WriteLine("New party:");

                foreach (KeyValuePair<string, Character> entry in Party.memberList)
                {
                    Character pm = entry.Value;
                    Console.WriteLine("Name: {0}, Job: {1}, Level: {2}, HP:{3}/{4}, MP:{5}/{6}", pm.name, pm.job, pm.level, pm.hp, pm.hpmax, pm.mp, pm.mpmax);
                }
            }
        }

        static void CreateCharacter()
        {
            bool isValid = false;
            string input;
            string name = "placeholder";

            Console.WriteLine("Creating new character.");

            Character newCharacter = new Character();
            
            while (!isValid)
            {
                Console.WriteLine("Enter character name.");
                input = Console.ReadLine();

                //TODO: check party list dictionary and make sure name doesn't conflict with an existing member
                if (input.Length < 2)
                {
                    input = "Sir Douchebag";
                }

                name = input;

                isValid = true;
            }
            
            isValid = false;

            while (!isValid)
            {
                Console.Write("Enter character class. (Current options:");
                foreach (string job in ClassNames.names)
                {
                    Console.Write(" [{0}]", job);
                }
                Console.Write(")");
                Console.Write(Environment.NewLine);

                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "warrior":
                        {
                            PlayerClass.Warrior tempChar = new PlayerClass.Warrior();
                            newCharacter = (Character)tempChar;
                            isValid = true;
                            break;
                        }
                    case "berserker":
                        {
                            PlayerClass.Berserker tempChar = new PlayerClass.Berserker();
                            newCharacter = (Character)tempChar;
                            isValid = true;
                            break;
                        }
                    case "mage":
                        {
                            PlayerClass.Mage tempChar = new PlayerClass.Mage();
                            newCharacter = (Character)tempChar;
                            isValid = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid command.");
                            break;
                        }
                }
            }

            newCharacter.name = name;
            newCharacter.hp = newCharacter.hpmax;
            newCharacter.mp = newCharacter.mpmax;
            Party.AddMember(newCharacter);
        }

        #endregion

        static void MagicTest()
        {
            //Build mage, build enemy, implement Magic

            //mage builder
            Character newCharacter = new Character();
            PlayerClass.Mage tempChar = new PlayerClass.Mage();
            newCharacter = (Character)tempChar;
            newCharacter.name = "Mage";
            newCharacter.hp = newCharacter.hpmax;
            newCharacter.mp = newCharacter.mpmax;
            Party.AddMember(newCharacter);

            //enemy builder
            Goblin2 preGoblin = new Goblin2();
            preGoblin.AdjustStats();
            Creature goblin = (Creature)preGoblin;
            goblin.id = 99;

            //Pre-cast
            Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", goblin.name, goblin.hp, goblin.hpmax, goblin.mp, goblin.mpmax);
            Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", newCharacter.name, newCharacter.hp, newCharacter.hpmax, newCharacter.mp, newCharacter.mpmax);

            //cast
            //Magic.Elemental firecast = MagicList.elemental["fire"];
            Console.WriteLine("{0} casts {1}.", newCharacter.name, MagicList.elemental["fire"].name);

            Creature[] creaArr;

            creaArr = MagicList.elemental["fire"].Cast((Creature)newCharacter, goblin);

            newCharacter.mp = creaArr[0].mp;
            goblin = creaArr[1];

            //post-cast
            Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", goblin.name, goblin.hp, goblin.hpmax, goblin.mp, goblin.mpmax);
            Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", newCharacter.name, newCharacter.hp, newCharacter.hpmax, newCharacter.mp, newCharacter.mpmax);
        }

        static void StatTests()
        {
            Character hero = new Character();

            Console.WriteLine("Input character name.");
            hero.name = Console.ReadLine();
            if (hero.name.Length < 2)
            {
                hero.name = "Sir Douchebag";
            }
            else if (hero.name.ToLower() == "exit")
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

        static void rTest()
        {
            int testint = 10;
            int[] testarr = { 10, 10 };
            rTestFunction(testint);

            Console.WriteLine(testint);
            int derp = rTestFunction(testint);
            Console.WriteLine(derp);
        }
        static int rTestFunction(int i)
        {
            return i + 2;
        }
    }
}
