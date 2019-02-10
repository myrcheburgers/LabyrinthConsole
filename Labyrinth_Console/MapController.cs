using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    class MapController
    {
        public static void LoadMap(int mapID, int[] playerPos)
        {
            Map map = ZoneList.mapIDList[mapID];
            map.InitializePlayerPosition(playerPos[0], playerPos[1]);
            map.Display();
            map.MovePlayer();
            //set movementMode to false to fall out and change areas?
        }

        //public void ChangeArea(int )
        public Map ChangeArea(int mapID, int[] playerPos)
        {
            Map map = ZoneList.mapIDList[mapID];
            map.InitializePlayerPosition(playerPos[0], playerPos[1]);

            return map;
        }
    }
}
