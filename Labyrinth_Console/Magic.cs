using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    static class MagicList
    {
        //implement with a string list on a per-creature basis?
        //or just as additional dictionary/ies to allow modifications on a per-character basis

        public static readonly Dictionary<string, Magic.Elemental> elemental = new Dictionary<string, Magic.Elemental>()
        {
            {"fire", new Magic.Elemental("Fire", "fire", 5, 10)},
            {"fire2", new Magic.Elemental("Fire II", "fire", 12, 20)}
        };
    }

    public abstract class Magic
    {
        //TODO: Enfeebling and enhancing -- add an effects<Magic> list to creatures and characters with HP/Acc/etc and time remaining, apply at beginning or end of each turn during battle
        public virtual Creature[] Cast(Creature caster, Creature target)
        {
            return null;
        }

        

        public class Elemental : Magic
        {
            public string name;
            public string element;
            //public int hpcost; ==> I guess can just be implemented on a per-magic-type basis with how I've set things up?
            public int mpcost;
            public int damage;


            //effect
            public override Creature[] Cast(Creature caster, Creature target)
            {
                //TODO: implement elemental resistances
                Creature[] creatureArr = new Creature[2];

                caster.mp -= mpcost;
                target.hp -= (int)Math.Floor((float)damage * (float)caster.mpmax / 10);

                creatureArr[0] = caster;
                creatureArr[1] = target;

                return creatureArr;
            }

            //constructor
            public Elemental(string _name, string _element, int _mpcost, int _dmg)
            {
                name = _name;
                damage = _dmg;
                mpcost = _mpcost;
                element = _element;
            }
        }

        public class Healing : Magic
        {
            public override Creature[] Cast(Creature caster, Creature target)
            {
                return null;
            }
        }

        public class Enhancing : Magic
        {
            public override Creature[] Cast(Creature caster, Creature target)
            {
                return null;
            }
        }
    }

    
}
