using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{   
    static class ZoneList
    {
        public static readonly Dictionary<int, Map> mapIDList = new Dictionary<int, Map>()
        {
            {1, MapList.Corridor1()},
            {2, MapList.Corridor2()}
        };
    }
    static class MapList
    {
        public static Map Corridor1()
        {
            Map map = new Map(25, 25);
            map.areaName = "Corridor 1";
            map.ID = 1;
            Status.blind = true;
            map.CreateBorder();
            map.CreateWallLine(1, 5, 20, 9);
            map.CreateWallLine(5, 15, 19, 9);

            map.CreateFloor(6, 5);
            map.CreateFloor(7, 5);

            for (int i = 8; i <= 12; i++)
            {
                for (int j = 10; j <= 14; j++)
                {
                    map.CreateWall(i, j);
                }
            }

            //map.CreateZonePoint(2, 4, new DestinationCoordinates(4, 3, 2));
            map.CreateZonePoint(new PositionKey(2, 4), new DestinationCoordinates(4, 3, 2));
            //map.InitializePlayerPosition(2, 2);

            return map;
        }

        public static Map Corridor2()
        {
            //placeholder
            Map map = new Map(25, 25);
            map.areaName = "Corridor 2";
            map.ID = 2;
            Status.blind = true;
            map.CreateBorder();
            map.CreateWallLine(1, 5, 20, 9);
            map.CreateWallLine(5, 15, 19, 9);

            map.CreateFloor(6, 5);
            map.CreateFloor(7, 5);

            for (int i = 8; i <= 12; i++)
            {
                for (int j = 10; j <= 14; j++)
                {
                    map.CreateWall(i, j);
                }
            }

            //map.CreateZonePoint(1, 1, 1);

            //map.InitializePlayerPosition(2, 2);

            return map;
        }
    }
}
