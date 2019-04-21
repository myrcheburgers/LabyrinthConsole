using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    class CharacterCreation
    {
        public void CreateToParty()
        {
            Character tempChar = CreateCharacterDialog();
            Party.AddMember(tempChar);
        }
        public Character CreateCharacterDialog()
        {
            //string setName = SetName();
            //Console.WriteLine("Name set: {0}", setName);
            
            Character character = new Character
            {
                name = SetName(),
                playerClass = SetClass()
            };

            character.vitals.hpmax = character.playerClass.baseHP;
            character.vitals.hp = character.vitals.hpmax;
            character.vitals.mpmax = character.playerClass.baseMP;
            character.vitals.mp = character.vitals.mpmax;
            character.vitals.speed = character.playerClass.baseSpeed;
            character.vitals.atk = character.playerClass.baseATK;
            character.vitals.def = character.playerClass.baseDEF;

            return character;
        }

        string SetName()
        {
            bool isValid = false;
            bool confirm = false;
            string input = "";
            string affirm = "";
            string prompt;
            int count1 = 0;
            int count2;

            while (!isValid)
            {
                //confirm = false;
                count2 = 0;
                prompt = count1 > 0 ? "Let's try again. Enter character name." : "Enter character name.";
                count1++;
                Console.WriteLine(prompt);
                input = Console.ReadLine();

                if (input.Length < 2)
                {
                    input = "Sir Douchebag";
                }

                while (!confirm)
                {
                    prompt = count2 > 0 ? "Yes or no, dumdum. Is your name " + input + " ?" : "Is your name " + input + " ?";
                    Console.WriteLine(prompt);
                    count2++;
                    affirm = Console.ReadLine();
                    switch (affirm.ToLower())
                    {
                        case "yes":
                        case "y":
                            {
                                isValid = true;
                                confirm = true;
                                return input;
                            }
                        case "no":
                        case "n":
                            {
                                confirm = true;
                                if (count1 > 10 || count2 > 10)
                                {
                                    input = "Sir Douchebag";
                                    Console.WriteLine("Your name is {0}.", input);
                                    return input;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        default:
                            {
                                if (count1 > 10 || count2 > 10)
                                {
                                    input = "Sir Douchebag";
                                    Console.WriteLine("Your name is {0}.", input);
                                    return input;
                                }
                                else
                                {
                                    break;
                                }
                            }
                    }
                }
            }
            return input;
        }

        PlayerClass SetClass()
        {
            bool isValid = false;
            string input;
            string[] jobs = { "Warrior", "Berserker", "Mage" };

            while (!isValid)
            {
                Console.Write("Enter character class. (Current options:");
                foreach (string job in jobs)
                {
                    Console.Write(" [{0}]", job);
                }
                Console.Write(")");
                Console.Write(Environment.NewLine);

                input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "warrior":
                    case "war":
                        {
                            isValid = true;
                            return PlayerClass.Warrior;
                        }
                    case "berserker":
                    case "ber":
                        {
                            isValid = true;
                            return PlayerClass.Berserker;
                        }
                    case "mage":
                    case "mag":
                        {
                            isValid = true;
                            //tempChar.magic.elemental = MagicList.elemental;
                            return PlayerClass.Mage;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid command.");
                            break;
                        }
                }
            }
            return PlayerClass.DebugQueen;
        }
    }
}
