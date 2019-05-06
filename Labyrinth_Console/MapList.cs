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
            {-1, MapList.Debug()},
            {0, MapList.Empty()},
            {1, MapList.Corridor1()},
            {2, MapList.Corridor2()},
            {101, MapList.Courtyard1()}
        };
    }
    static class MapList
    {
        public static Map Empty()
        {
            Map map = new Map(25, 25);
            map.areaName = "NullSpace";
            map.ID = 0;
            Status.blind = false;
            map.CreateBorder();

            return map;
        }

        public static Map Debug()
        {
            Map map = new Map(25, 25);
            map.areaName = "Debug Room";
            map.ID = -1;
            Status.blind = false;
            map.CreateBorder();

            map.CreateCircleCustom(10, 10, 5, 'W');

            // to corridor 1 (one-way)
            map.CreateZonePoint(new PositionKey(1, 1), new DestinationCoordinates(3, 3, 1));

            return map;
        }

        public static Map Corridor1()
        {
            Map map = new Map(25, 25);
            map.areaName = "Corridor 1";
            map.ID = 1;
            map.defaultBlind = true;
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

            map.CreateZonePoint(new PositionKey(23, 23), new DestinationCoordinates(4, 3, 2)); //Corridor2
            map.CreateZonePoint(new PositionKey(1, 4), new DestinationCoordinates(2, 2, 101)); //Courtyard1
            //map.InitializePlayerPosition(2, 2);

            return map;
        }

        public static Map Corridor2()
        {
            //placeholder
            Map map = new Map(25, 25);
            map.areaName = "Corridor 2";
            map.ID = 2;
            map.defaultBlind = true;
            map.CreateBorder();
            map.CreateWallLine(1, 5, 20, 9);
            map.CreateWallLine(5, 15, 19, 9);

            map.CreateFloor(6, 5);
            map.CreateFloor(7, 5);

            for (int i = 10; i <= 14; i++)
            {
                for (int j = 14; j <= 18; j++)
                {
                    map.CreateWall(i, j);
                }
            }

            map.CreateZonePoint(new PositionKey(5, 1), new DestinationCoordinates(4, 4, 1));

            return map;
        }

        public static Map Courtyard1()
        {
            Map map = new Map(25, 25);
            map.areaName = "Courtyard";
            map.ID = 101;
            map.defaultBlind = false;
            map.CreateBorder();
            map.CreateCircle(12, 12, 6, "water");

            map.CreateZonePoint(new PositionKey(5, 1), new DestinationCoordinates(2, 2, 1));
            map.CreateZonePoint(new PositionKey(15, 22), new DestinationCoordinates(10, 4, 2));

            return map;
        }
    }
}
