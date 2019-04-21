using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
                Console.WriteLine("Pointless text (Main(string[] args start)");
                //Console.ReadKey();
                Console.Write(Environment.NewLine);

                Start();
            }
            
        }

        static void Start()
        {
            //Console.WriteLine("Input command.");
            //string input;
            string[] cmd;

            bool isValid = false;
            
            while (!isValid)
            {
                Console.WriteLine("Input command.");
                
                cmd = Console.ReadLine().ToLower().Split(' ');
                switch (cmd[0])
                {
                    case "_rtest":
                        {
                            isValid = true;
                            rTest();
                            break;
                        }
                    case "_battletest":
                    case "btest":
                        {
                            isValid = true;
                            BattleTest();
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
                    case "partysize":
                    case "psize":
                        {
                            isValid = true;
                            int partyCount = Party.memberList.Count;
                            Console.WriteLine("Party size: {0}", partyCount);
                            if (partyCount > 0)
                            {
                                foreach (KeyValuePair<string, Character> member in Party.memberList)
                                {
                                    Party.PrintStats(member.Value);
                                }
                            }
                            break;
                        }
                    case "maptest":
                        {
                            MapTest();
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
                    case "stringint":
                    case "si":
                        {
                            isValid = true;
                            int i = 0;
                            //i = (int)Convert.ToChar(cmd[1]) % 32;

                            char[] cArr = cmd[1].ToCharArray();
                            foreach (char c in cArr)
                            {
                                i += (int)c % 32;
                            }
                            Console.WriteLine("Int value: {0}", i);
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

                    //CreateCharacter();
                    CharacterCreation characterCreation = new CharacterCreation();
                    characterCreation.CreateToParty();

                    Console.WriteLine("Character {0} of {1} created.", i + 1, partySize);
                }
            }

            if (isValid)
            {
                Console.WriteLine("New party:");

                foreach (KeyValuePair<string, Character> entry in Party.memberList)
                {
                    Character pm = entry.Value;
                    Console.WriteLine("Name: {0}, Job: {1}, Level: {2}, HP:{3}/{4}, MP:{5}/{6}", pm.name, pm.job, pm.level, pm.vitals.hp, pm.vitals.hpmax, pm.vitals.mp, pm.vitals.mpmax);
                }
            }
        }

        /*
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
                foreach (string job in PlayerClass.classNames)
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
                            PlayerClass_Old.Warrior tempChar = new PlayerClass_Old.Warrior();
                            newCharacter = (Character)tempChar;
                            isValid = true;
                            break;
                        }
                    case "berserker":
                        {
                            PlayerClass_Old.Berserker tempChar = new PlayerClass_Old.Berserker();
                            newCharacter = (Character)tempChar;
                            isValid = true;
                            break;
                        }
                    case "mage":
                        {
                            PlayerClass_Old.Mage tempChar = new PlayerClass_Old.Mage();
                            //TODO: Delete or modifiy this if ever expanded beyond testing purposes
                            tempChar.magic.elemental = MagicList.elemental;
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
        */

        #endregion

        static void BattleTest()
        {
            if (Party.memberList.Count == 0)
            {
                Console.WriteLine("Make a party first (command \"party\")");
            }
            else
            {
                //enemy builder
                Goblin2 preGoblin = new Goblin2();
                preGoblin.AdjustStats();
                Creature goblin = (Creature)preGoblin;
                goblin.id = "goblin1";

                preGoblin.AdjustStats();
                Creature goblin2 = (Creature)preGoblin;
                goblin2.id = "goblin2";

                Creature[] mobArray = { goblin, goblin2 };

                //char array setup because I can't be arsed to change code in several other areas
                Character[] charArray = new Character[Party.memberList.Count];
                Party.memberList.Values.CopyTo(charArray, 0);

                Battle battle = new Battle();
                battle.Start(charArray, mobArray);
            }
        }

        static void MagicTest()
        {
            //Build mage, build enemy, implement Magic

            //mage builder
            //Character newCharacter = new Character();
            //PlayerClass_Old.Mage tempChar = new PlayerClass_Old.Mage();
            //newCharacter = (Character)tempChar;
            //newCharacter.name = "Mage";
            //newCharacter.hp = newCharacter.hpmax;
            //newCharacter.mp = newCharacter.mpmax;
            Character newCharacter = new Character();
            newCharacter.name = "Mage";
            newCharacter.playerClass = PlayerClass.Mage;
            newCharacter.InitializeVitals();

            //newCharacter.magic.elemental.Add(MagicList.elemental["fire"].name, MagicList.elemental["fire"]);
            newCharacter.magic.elemental.Add("fire", MagicList.elemental["fire"]);

            Party.AddMember(newCharacter);

            //enemy builder
            Goblin2 preGoblin = new Goblin2();
            preGoblin.AdjustStats();
            Creature goblin = (Creature)preGoblin;
            goblin.id = "placeholderID";

            //Pre-cast
            Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", goblin.name, goblin.vitals.hp, goblin.vitals.hpmax, goblin.vitals.mp, goblin.vitals.mpmax);
            Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", newCharacter.name, newCharacter.vitals.hp, newCharacter.vitals.hpmax, newCharacter.vitals.mp, newCharacter.vitals.mpmax);

            //cast
            //Console.WriteLine("{0} casts {1}.", newCharacter.name, MagicList.elemental["fire"].name);
            Console.WriteLine("{0} casts {1}.", newCharacter.name, newCharacter.magic.elemental["fire"].name);

            Creature[] creaArr;

            //creaArr = MagicList.elemental["fire"].Cast((Creature)newCharacter, goblin);
            creaArr = newCharacter.magic.elemental["fire"].Cast((Creature)newCharacter, goblin);

            newCharacter.vitals.mp = creaArr[0].vitals.mp;
            goblin = creaArr[1];

            //post-cast
            Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", goblin.name, goblin.vitals.hp, goblin.vitals.hpmax, goblin.vitals.mp, goblin.vitals.mpmax);
            Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", newCharacter.name, newCharacter.vitals.hp, newCharacter.vitals.hpmax, newCharacter.vitals.mp, newCharacter.vitals.mpmax);

            //that... actually works?

            Party.RemoveAll();

            #region nope
            ////post-cast
            //Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", goblin.name, goblin.hp, goblin.hpmax, goblin.mp, goblin.mpmax);
            //Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", newCharacter.name, newCharacter.hp, newCharacter.hpmax, newCharacter.mp, newCharacter.mpmax);

            ////with testdict:
            //Console.WriteLine("{0} casts {1}.", newCharacter.name, MagicList.test["test"].name);

            //creaArr = MagicList.test["test"].Cast((Creature)newCharacter, goblin);

            //newCharacter.mp = creaArr[0].mp;
            //goblin = creaArr[1];

            ////post-cast
            //Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", goblin.name, goblin.hp, goblin.hpmax, goblin.mp, goblin.mpmax);
            //Console.WriteLine("{0}: HP {1}/{2} MP {3}/{4}", newCharacter.name, newCharacter.hp, newCharacter.hpmax, newCharacter.mp, newCharacter.mpmax);
            #endregion
        }
        
        static void MapTest()
        {
            /**
            Map tmap = new Map(25, 25);
            //tmap.CreateEmpty(25, 25);
            //Thread.Sleep(200);
            tmap.CreateBorder();
            tmap.InitializePlayerPosition(4, 5);
            //Thread.Sleep(100);
            tmap.Display();
            tmap.MovePlayer();
            **/
            //Status.blind = true;

            int[] startPos = { 2, 2 };
            MapController.LoadMap(1, startPos);
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
            goblin.id = "placeholderID";
            Console.WriteLine("Name: {0} Level: {1} ID: {2} Max HP: {3} Current HP: {4} Max MP: {5} Current MP: {6} ATK: {7} DEF: {8} SPD: {9}", goblin.name, goblin.level, goblin.id, goblin.vitals.hpmax, goblin.vitals.hp, goblin.vitals.mpmax, goblin.vitals.mp, goblin.vitals.atk, goblin.vitals.def, goblin.vitals.speed);

            Battle testBattle = new Battle();

            //testBattle.RollInitiative(hero, goblin);
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
