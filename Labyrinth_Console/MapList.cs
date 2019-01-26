using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    static class MapList
    {
        public static Map Corridor1()
        {
            Map map = new Map(25, 25);
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

            map.InitializePlayerPosition(2, 2);

            return map;
        }
    }
}
