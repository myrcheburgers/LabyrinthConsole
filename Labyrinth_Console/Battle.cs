using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    class Battle
    {
        //TODO: convert character type to creature type for battles -- will simplifiy yet-to-be-implemented targeting systems
        public void Start(Character player, ICreature mob)
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
    }
}
