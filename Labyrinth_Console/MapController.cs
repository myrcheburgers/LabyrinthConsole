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
            #region first attempt...
            //Map refMap = ZoneList.mapIDList[mapID];
            //Map map = new Map(refMap.thisMap.GetLength(0), refMap.thisMap.GetLength(1));

            //Map map = ZoneList.mapIDList[mapID]; 
            //map.InitializePlayerPosition(playerPos[0], playerPos[1]);
            //map.Display();
            //map.MovePlayer();

            //set movementMode to false to fall out and change areas?
            #endregion

            Map map = ZoneList.mapIDList[0];
            Map destMap = ZoneList.mapIDList[mapID];

            map.zonePoints = destMap.zonePoints;
            map.areaName = destMap.areaName;
            map.ID = destMap.ID;
            map.thisMap = destMap.thisMap;
            map.encounterRate = destMap.encounterRate;
            //this.mobList = destination.mobList;

            Status.blind = destMap.defaultBlind;

            map.InitializePlayerPosition(playerPos[0], playerPos[1]);
            map.Display();
            map.MovePlayer();

            //fugly code, but hory shet it works now
        }

        //public void ChangeArea(int )
        public Map ChangeArea(int mapID, PositionKey playerPos)
        {
            //TODO try in map when experimenting:
            //movementMod = false;
            //ChangeArea(replace params with structs)
            Map map = ZoneList.mapIDList[mapID];
            map.InitializePlayerPosition(playerPos.i, playerPos.j);

            return map;
        }
    }
}
