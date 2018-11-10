using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    public class MagicLearned
    {
        //for storing learned spells for each character/creature

        public Dictionary<string, Magic.Elemental> elemental = new Dictionary<string, Magic.Elemental>()
        {

        };

        public Dictionary<string, Magic.Healing> healing = new Dictionary<string, Magic.Healing>()
        {

        };
    }
}
