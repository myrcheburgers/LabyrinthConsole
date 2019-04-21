using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    class Battle
    {
        // Note: isPlayer field (bool) has been added to Creature class
        // This entire class could probably use a rewrite

        RNG rng = new RNG();
        static Random rng2 = new Random();

        int seed; //range: [100000000, 999999999]

        List<Creature> combatants = new List<Creature>();
        List<Creature> partyList = new List<Creature>();
        List<Creature> mobList = new List<Creature>();

        public void Start(Character[] party, Creature[] mobs)
        {
            seed = rng.seed();
            foreach (Character member in party)
            {
                partyList.Add((Creature)member);
                combatants.Add((Creature)member);
            }
            foreach (Creature mob in mobs)
            {
                mobList.Add(mob);
                combatants.Add(mob);
            }

            combatants = SetTurnOrder(combatants);

            Console.WriteLine("Turn order:");
            foreach (Creature entity in combatants)
            {
                Console.WriteLine("    {0} (Speed: {1}, isPlayer = {2})", entity.name, entity.vitals.speed, entity.isPlayer);
            }

            this.Combat(combatants);
        }

        public void Combat(List<Creature> combatants)
        {
            //To be called from Battle.Start()
            bool victory = false;
            bool isValid = false;
            string[] validCommands =
            {
                "help",
                "mobs",
                "attack [ID]",
                "magic [spell] [ID]"
            };
            string input;
            string[] cmd;
            int turnNumber = 0;
            Creature mobTarget;
            while (!victory)
            {
                Console.WriteLine("Starting turn {0}", turnNumber);
                foreach (Creature mob in combatants)
                {
                    if (mob.isPlayer && mob.vitals.hp > 0)
                    {
                        //command tree here
                        while (!isValid)
                        {
                            Console.WriteLine("Turn: {0}. Enter command. Type \"help\" for valid commands.", mob.name);
                            input = Console.ReadLine();
                            cmd = input.Split(' ');
                            switch (cmd[0])
                            {
                                case "help":
                                    {
                                        foreach (string entry in validCommands)
                                        {
                                            Console.WriteLine("{0} ", entry);
                                        }
                                        break;
                                    }
                                case "mobs":
                                case "moblist":
                                    {
                                        foreach (Creature entry in combatants)
                                        {
                                            //yo dawg...
                                            if (!entry.isPlayer && entry.vitals.hp > 0)
                                            {
                                                Console.WriteLine("{0}: [ID] {1} [HP] {2}/{3}", entry.name, entry.id, entry.vitals.hp, entry.vitals.hpmax);
                                            }
                                        }
                                        break;
                                    }
                                case "players":
                                case "plist":
                                    {
                                        foreach (Creature entry in combatants)
                                        {
                                            if (entry.isPlayer)
                                            {
                                                Console.WriteLine("{0}: [ID] {1} [HP] {2}/{3} [MP] {4}/{5}", entry.name, entry.id, entry.vitals.hp, entry.vitals.hpmax, entry.vitals.mp, entry.vitals.mpmax);
                                            }
                                        }
                                        break;
                                    }
                                case "attack":
                                case "a":
                                    {
                                        //Holy shit this is ugly... a dictionary would have been better, but I can't be arsed to go back and change things for a proof of concept
                                        foreach (Creature entry in combatants)
                                        {
                                            if ((entry.id == cmd[1]) && (entry.vitals.hp > 0))
                                            {
                                                isValid = true;
                                                int dmg = Attack(mob, entry);
                                                Console.WriteLine("{0} attacks the {1} for {2} points of damage.", mob.name, entry.name, dmg);
                                                entry.vitals.hp -= dmg;

                                                if (entry.vitals.hp <= 0)
                                                {
                                                    Console.WriteLine("The {0} [{1}] has been defeated!", entry.name, entry.id);
                                                    //if (!entry.isPlayer)
                                                    //{
                                                    //    combatants.Remove(entry);
                                                    //}
                                                }

                                                break;
                                            }
                                            else
                                            {
                                                //Console.WriteLine("Target ID not found. Type \"moblist\" for a list of valid target IDs.");
                                            }
                                        }
                                        if (!isValid)
                                        {
                                            Console.WriteLine("Target ID not found. Type \"moblist\" for a list of valid target IDs.");
                                        }
                                        break;
                                    }
                                case "magic":
                                case "m":
                                    {
                                        //fuggernuts
                                        //TODO: need to change end results for cases when caster and target are the same entity

                                        bool spellCast = false;
                                        bool targetFound = false;
                                        int spellVal;
                                        string spell = cmd[1];
                                        string tarID = cmd[2];
                                        Creature[] mMobs =  new Creature[2];
                                        Creature _caster = mob;
                                        Creature _target = new Creature("placeholder", "phID", 0, 0, 0, 0, 0, 0, false, mob.magic);

                                        foreach (Creature target in combatants)
                                        {
                                            if ((target.id == tarID) && (target.vitals.hp > 0))
                                            {
                                                targetFound = true;
                                                if (mob.magic.elemental.ContainsKey(spell))
                                                {
                                                    if (mob.vitals.mp >= mob.magic.elemental[spell].mpcost)
                                                    {
                                                        //TODO: debug this garbo
                                                        spellCast = true;
                                                        mMobs = mob.magic.elemental[spell].Cast(mob, target);
                                                        _caster = mMobs[0];
                                                        _target = mMobs[1];

                                                        Console.WriteLine("PRE {0} {1} \nPOST {2} {3}", target.name, target.vitals.hp, _target.name, _target.vitals.hp);

                                                        spellVal = target.vitals.hp - _target.vitals.hp;
                                                        Console.WriteLine("{0} casts {1} on {2} for {3} points of damage!", mob.name, mob.magic.elemental[spell].name, target.name, Convert.ToString(spellVal));
                                                    }
                                                }
                                                else if (mob.magic.healing.ContainsKey(spell))
                                                {
                                                    if (mob.vitals.mp >= mob.magic.healing[spell].mpcost)
                                                    {
                                                        spellCast = true;
                                                        mMobs = mob.magic.healing[spell].Cast(mob, target);
                                                        _caster = mMobs[0];
                                                        _target = mMobs[1];
                                                        Console.WriteLine("{0} casts {1} on {2} for {3} points of restoration!", mob.name, mob.magic.healing[spell].name, target.name, _target.vitals.hp - target.vitals.hp);
                                                    }
                                                }
                                            }

                                            if (spellCast)
                                            {
                                                target.vitals.hp = _target.vitals.hp;
                                                target.vitals.mp = _target.vitals.mp;

                                                mob.vitals.hp = _caster.vitals.hp;
                                                mob.vitals.mp = _caster.vitals.mp;

                                                break;
                                            }
                                        }
                                        
                                        if (!targetFound)
                                        {
                                            Console.WriteLine("Target ID not found. Type \"moblist\" for a list of valid target IDs.");
                                        }
                                        else if (!spellCast)
                                        {
                                            Console.WriteLine("Spell not found.");
                                        }
                                        break;
                                    }
                            }
                        }
                        isValid = false;
                    }
                    else if (mob.vitals.hp > 0)
                    {
                        int iPlayer;
                        iPlayer = rng2.Next(combatants.Count);
                        while (!combatants[iPlayer].isPlayer)
                        {
                            iPlayer = rng2.Next(combatants.Count);
                        }
                        
                        Console.WriteLine("Enemy turn: {0} [{1}] [HP {2}/{3}]", mob.name, mob.id, mob.vitals.hp, mob.vitals.hpmax);
                        int dmg = Attack(mob, combatants[iPlayer]);
                        combatants[iPlayer].vitals.hp -= dmg;
                        Console.WriteLine("The {0} [{1}] attacks {2} for {3} points of damage.", mob.name, mob.id, combatants[iPlayer].name, dmg);
                    }
                }

                // deathcheck
                // TODO: make this an action/event
                combatants.RemoveAll(r => !r.isPlayer && r.vitals.hp <= 0);

                // win check
                int mobCount = 0;
                foreach (Creature combatant in combatants)
                {
                    if (!combatant.isPlayer)
                    {
                        mobCount++;
                    }
                }
                if (mobCount == 0)
                {
                    victory = true;
                }

                turnNumber++;
                Console.WriteLine("Key to continue");
                Console.ReadLine();
            }

            if (victory)
            {
                Console.WriteLine("Obligatory placeholder victory text goes here.");
            }
        }

        

        public List<Creature> SetTurnOrder(List<Creature> combatants)
        {
            //int startPos = 0;
            //
            //foreach (Creature entity in combatants)
            //{
            //    entity.speed = AdjustSpeed(entity, seed, startPos);
            //    startPos++;
            //}

            //combatants.Sort((x, y) => y.speed.CompareTo(x.speed)); //descending order

            int n = combatants.Count;
            while (n > 1)
            {
                n--;
                int k = rng2.Next(n + 1);
                Creature value = combatants[k];
                combatants[k] = combatants[n];
                combatants[n] = value;
            }
            return combatants;
        }

        public int Attack(Creature attacker, Creature defender)
        {
            float damage = 5 + (attacker.vitals.atk / defender.vitals.def);
            damage += rng.rndInt((int)Math.Floor(damage / 10) * -1, (int)Math.Floor(damage / 10));

            return (int)Math.Floor(damage);
        }

        #region obsolete
        /**
        public int AdjustSpeed(Creature entity, int seed, int seedStartPos)
        {
            //Set speed to +/- whatever percent
            int percentRange = 15;
            float multiplier = (float)percentRange / 100;
            int adjustment = 0;
            int negativeMultiplier;

            int[] seedArr = seed.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();
            int arrLength = seedArr.Length;

            int intID = rng.StringToInt(entity.name);
            int index = (intID + seedStartPos) % arrLength; //seedArr[i] = 0 to 9
            int iterations = 3; //number of ints to add

            if (seedArr[(index + 4) % arrLength] < 5)
            {
                negativeMultiplier = 1;
            }
            else
            {
                negativeMultiplier = -1;
            }

            for (int i = 0; i < iterations; i++)
            {
                adjustment += seedArr[(index + i) % arrLength];
                //Note: possible range = [0, 9*iterations]
            }

            //newSpeed = speed +/- speed*[0, %range]
            int newSpeed = entity.speed + negativeMultiplier * entity.speed * (adjustment / (9 * iterations) * percentRange);

            return newSpeed;
        }
        
        public void RollInitiative(Character player, ICreature mob)
        {
            #region Initiative calculation
            RNG rng = new RNG();
            //int[] initiative = new int[2];
            int[] initiative = { 0, 0 };
            int seed = rng.seed();
            int[] seedArr = seed.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray(); //Reference https://www.physicsforums.com/threads/converting-integer-into-array-of-single-digits-in-c.558588/

            int halfArr = Convert.ToInt32(Math.Floor(Convert.ToDouble(seedArr.Length) / 2));

            for (int i = 0; i < halfArr; i++)
            {
                initiative[0] += seedArr[i];
            }
            for (int i = halfArr; i < halfArr*2; i++)
            {
                initiative[1] += seedArr[i];
            }
            Console.WriteLine("Seed: {0}", seed);
            Console.WriteLine("Seed array:");
            for (int i = 0; i < seedArr.Length; i++)
            {
                Console.Write("[{0}]", seedArr[i]);
            }
            Console.Write(Environment.NewLine);
            Console.WriteLine("Player initiative: {0}\n{1} initiative: {2}", initiative[0], mob.name, initiative[1]);
            #endregion
        }

        public Creature CreateBattler(Character partyMember)
        {
            return (Creature)partyMember;
        }
        **/
        #endregion
    }
}
