using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
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
}
