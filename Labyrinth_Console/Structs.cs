using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    #region mapping
    public struct DestinationCoordinates
    {
        public int iDestination;
        public int jDestination;
        public int MapID;

        public DestinationCoordinates(int iDest, int jDest, int ID)
        {
            iDestination = iDest;
            jDestination = jDest;
            MapID = ID;
        }
    }

    public struct PositionKey
    {
        public int i;
        public int j;

        public PositionKey(int _i, int _j)
        {
            i = _i;
            j = _j;
        }
    }
    #endregion

    #region creatures
    public struct ElementalResistance
    {
        // Percentages
        public int earth;
        public int water;
        public int wind;
        public int fire;
        public int ice;
        public int thunder;
        
        public ElementalResistance(int earth, int water, int wind, int fire, int ice, int thunder)
        {
            this.earth = earth;
            this.water = water;
            this.wind = wind;
            this.fire = fire;
            this.ice = ice;
            this.thunder = thunder;
        }
        
        static readonly ElementalResistance standardResistance = new ElementalResistance(100, 100, 100, 100, 100, 100);

        // Shorthand for writing @@ElementalResistance(100, 100, 100, 100, 100, 100)@@
        public static ElementalResistance standard { get { return standardResistance; } }
    }

    public struct PhysicalResistance
    {
        // Percentages
        public int blunt;
        public int slashing;
        public int piercing;

        public PhysicalResistance (int blunt, int slashing, int piercing)
        {
            this.blunt = blunt;
            this.slashing = slashing;
            this.piercing = piercing;
        }

        static readonly PhysicalResistance standardResistance = new PhysicalResistance(100, 100, 100);

        // Shorthand for writing @@PhysicalResistance(100, 100, 100)@@
        public static PhysicalResistance standard { get { return standardResistance; } }
    }

    public struct Vitals
    {
        public int hpmax;
        public int hp;
        public int mpmax;
        public int mp;
        public int atk;
        public int def;
        public int speed;

        public Vitals(int hp, int mp, int atk, int def, int speed)
        {
            this.hpmax = hp;
            this.hp = hp;
            this.mpmax = mp;
            this.mp = mp;
            this.atk = atk;
            this.def = def;
            this.speed = speed;
        }
    }
    #endregion
}
