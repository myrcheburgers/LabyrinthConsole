using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Labyrinth_Console
{
    class Map
    {
        //PositionKey(int _i, int _j)
        //DestinationCoordinates(int iDest, int jDest, int ID)
        public Dictionary<PositionKey, DestinationCoordinates> zonePoints = new Dictionary<PositionKey, DestinationCoordinates>();

        public string areaName;
        public int ID;
        public bool defaultBlind;
        public bool movementMode = false;

        //only one map is ever going to be used at once, so whatever
        static int columns;
        const int jMin = 5;
        const int jMax = 30;
        static int rows;
        const int iMin = 5;
        const int iMax = 30;

        const int minInputTime = 100;

        //[i j]
        int[] playerPosition = new int[2];

        //char[,] map;
        char wall = '#';
        char floor = '-';
        //char water = '~';
        char water = 'W';
        char playerPos = 'O';
        char zonePos = 'X';

        //[i, j, destinationZoneID
        int[] zonePoint = new int[3];

        public int encounterRate;
        //Creature implementation might need some work for this to not be a pain in the arse:
        //Creature[] mobList;

        //constructor + initialization stuff
        public char[,] thisMap;
        public Map(int _rows, int _columns)
        {
            rows = MinMaxCheck(_rows, iMin, iMax);
            columns = MinMaxCheck(_columns, jMin, jMax);
            thisMap = new char[rows, columns]; //neat, that actually fixed crash issues
        }

        public void InitializePlayerPosition(int _playerRow, int _playerCol)
        {
            playerPosition[0] = MinMaxCheck(_playerRow, 1, iMax - 1);
            playerPosition[1] = MinMaxCheck(_playerCol, 1, jMax - 1);
            thisMap[playerPosition[0], playerPosition[1]] = playerPos;
        }

        #region wall creation, etc
        public void CreateBorder()
        {
            //if (isInitialized)
            //{
                for (int i = 0; i < thisMap.GetLength(0); i++)
                {
                    for (int j = 0; j < thisMap.GetLength(1); j++)
                    {
                        if (i == 0 || j == 0 || i == thisMap.GetLength(0) - 1 || j == thisMap.GetLength(1) - 1)
                        {
                            thisMap[i, j] = wall;
                        }
                        else
                        {
                            thisMap[i, j] = floor;
                        }
                    }
                }
            //}
        }

        public void CreateCircle(int iCenter, int jCenter, int r, string type)
        {
            bool isValid;
            char material = ' '; //placeholder
            switch (type.ToLower())
            {
                case "wall":
                    {
                        isValid = true;
                        material = wall;
                        break;
                    }
                case "floor":
                    {
                        isValid = true;
                        material = floor;
                        break;
                    }
                case "water":
                    {
                        isValid = true;
                        material = water;
                        break;
                    }
                default:
                    {
                        isValid = false;
                        Console.WriteLine("Invalid character type: {0}. Press enter to continue.", type.ToLower());
                        Console.ReadLine();
                        break;
                    }
            }

            if (isValid)
            {
                //try
                //{
                    for (int i = iCenter - r; i <= iCenter + r; i++)
                    {
                        for (int j = jCenter - r; j <= jCenter + r; j++)
                        {
                            if (Math.Sqrt(((float)i*(float)i) + ((float)j*(float)j)) <= (float)r)
                            {
                                thisMap[i, j] = material;
                            }
                        }
                    }
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine("Exception: {0}\nPress enter to continue.", e.ToString());
                //    Console.ReadLine();
                //}
            }
        }

        public void CreateWall(int i, int j)
        {
            thisMap[i, j] = wall;
        }

        public void CreateWallLine(int iAnchor, int jAnchor, int length, int dir)
        {
            //dir: 0 = horizontal, 1 = [y=x], -1 = [y=-x], 9 = vertical
            switch (dir)
            {
                case 0:
                    {
                        if (jAnchor + length - 1 > jMax)
                        {
                            length = jMax - jAnchor;
                        }

                        for (int a = jAnchor; a < jAnchor + length; a++)
                        {
                            CreateWall(iAnchor, a);
                        }
                        break;
                    }
                case 1:
                case -1:
                    {
                        if (jAnchor + length - 1 > jMax)
                        {
                            length = jMax - jAnchor;
                        }
                        if (iAnchor + length - 1 > iMax)
                        {
                            length = iMax - iAnchor;
                        }

                        for (int a = 0; a < length; a++)
                        {
                            CreateWall(iAnchor + a * dir, jAnchor + a);
                        }

                        break;
                    }
                case 9:
                    {
                        if (iAnchor + length - 1 > iMax)
                        {
                            length = iMax - iAnchor;
                        }

                        for (int a = iAnchor; a < iAnchor + length; a++)
                        {
                            CreateWall(a, jAnchor);
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid input");
                        break;
                    }
            }
        }

        public void CreateFloor(int i, int j)
        {
            thisMap[i, j] = floor;
        }

        void ClearMap()
        {
            //todo because I'm dumb
            //Or not, because this is probably not needed
        }
        #endregion
        
        #region zone changes
        public void CreateZonePoint(PositionKey position, DestinationCoordinates destination)
        {
            thisMap[position.i, position.j] = zonePos;
            zonePoints.Add(position, destination);
        }

        void ZoneTransition(int iPlayer, int jPlayer)
        {
            try
            {
                DestinationCoordinates dest = zonePoints[new PositionKey(iPlayer, jPlayer)];
                LoadNewArea(ZoneList.mapIDList[dest.MapID], dest.iDestination, dest.jDestination);
                //Map destinationMap = new Map(rows, columns);
                //destinationMap = ZoneList.mapIDList[dest.MapID];
                //LoadNewArea(destinationMap, dest.iDestination, dest.jDestination);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:\n[iPlayer] {0}, [jPlayer] {1},\n{2}\n Press enter to continue.", iPlayer, jPlayer, e.ToString());
                Console.ReadLine();
            }
        }
        
        void LoadNewArea(Map destination, int iDestinationPlayerPosition, int jDestinationPlayerPosition)
        {
            // 2/23/19: this only works for the first zone transition...?
            // ...because it's directly modifying the first dictionary entry loaded

            this.zonePoints = destination.zonePoints;

            this.areaName = destination.areaName;
            this.ID = destination.ID;
            Status.blind = destination.defaultBlind;
            //this.movementMode = destination.movementMode;

            this.thisMap = destination.thisMap; //yo dawg...

            //constants: jMin, jMax, iMin, iMax, minInputTime
            //this.playerPosition = destinationPlayerPosition;
            this.playerPosition = new int[] { iDestinationPlayerPosition, jDestinationPlayerPosition };

            ////char[,] map;
            //char wall = '#';
            //char floor = '-';
            //char playerPos = 'O';
            //char zonePos = 'X';

            ////[i, j, destinationZoneID
            //int[] zonePoint = new int[3];

            this.encounterRate = destination.encounterRate;
            //Creature implementation might need some work for this to not be a pain in the arse:
            //this.mobList = destination.mobList;
        }
        #endregion

        #region movement
        //take current position plus whatever movement, check if within bounds and if floor is present, apply movement
        //Note: Y axis of display will be inverted

        void MoveUp()
        {
            if (thisMap[playerPosition[0] - 1, playerPosition[1]] == floor)
            {
                AdjustTile(floor);
                playerPosition[0] -= 1;
                AdjustTile(playerPos);
            }
            else if (thisMap[playerPosition[0] - 1, playerPosition[1]] == zonePos)
            {
                //ZoneTransition(playerPosition);
                //wipe zonepoint dictionary before rebuild

                //try
                //{
                AdjustTile(floor);
                playerPosition[0] -= 1;
                //AdjustTile(zonePos);
                ZoneTransition(playerPosition[0], playerPosition[1]);
                AdjustTile(playerPos);
                //}
                //catch(KeyNotFoundException)
                //{
                //    movementMode = false;
                //    Console.WriteLine("Position: {0}, {1}", playerPosition[0], playerPosition[1]);
                //    Console.WriteLine("Position [array]: {0}", playerPosition);
                //    Console.WriteLine("Attempted position: {0}, {1}", playerPosition[0] - 1, playerPosition[1]);
                //    Console.WriteLine("Press Enter to continue.");
                //    Console.ReadLine();

                //    //TODO:Apparently ZoneTransition can't be fed the entire array?
                //}
            }
        }
        void MoveDown()
        {
            if (thisMap[playerPosition[0] + 1, playerPosition[1]] == floor)
            {
                AdjustTile(floor);
                playerPosition[0] += 1;
                AdjustTile(playerPos);
            }
            else if (thisMap[playerPosition[0] + 1, playerPosition[1]] == zonePos)
            {

                AdjustTile(floor);
                playerPosition[0] += 1;
                ZoneTransition(playerPosition[0], playerPosition[1]);
                AdjustTile(playerPos);
            }
        }
        void MoveLeft()
        {
            if (thisMap[playerPosition[0], playerPosition[1] - 1] == floor)
            {
                AdjustTile(floor);
                playerPosition[1] -= 1;
                AdjustTile(playerPos);
            }
            else if (thisMap[playerPosition[0], playerPosition[1] - 1] == zonePos)
            {

                AdjustTile(floor);
                playerPosition[1] -= 1;
                ZoneTransition(playerPosition[0], playerPosition[1]);
                AdjustTile(playerPos);
            }
        }
        void MoveRight()
        {
            if (thisMap[playerPosition[0], playerPosition[1] + 1] == floor)
            {
                AdjustTile(floor);
                playerPosition[1] += 1;
                AdjustTile(playerPos);
            }
            else if (thisMap[playerPosition[0], playerPosition[1] + 1] == zonePos)
            {

                AdjustTile(floor);
                playerPosition[1] += 1;
                ZoneTransition(playerPosition[0], playerPosition[1]);
                AdjustTile(playerPos);
            }
        }
        
        void AdjustTile(char tile)
        {
            thisMap[playerPosition[0], playerPosition[1]] = tile;
        }

        public void MovePlayer()
        {
            movementMode = true;
            while (movementMode)
            {
                var inputKey = Console.ReadKey(true).Key;
                switch (inputKey)
                {
                    case ConsoleKey.Escape:
                        {
                            movementMode = false;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            MoveUp();
                            Display();
                            Thread.Sleep(minInputTime);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            MoveDown();
                            Display();
                            Thread.Sleep(minInputTime);
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            MoveLeft();
                            Display();
                            Thread.Sleep(minInputTime);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            MoveRight();
                            Display();
                            Thread.Sleep(minInputTime);
                            break;
                        }
                    case ConsoleKey.B:
                        {
                            //TODO: eventually delete this block
                            Status.blind = !Status.blind;
                            Display();
                            Thread.Sleep(minInputTime);
                            break;
                        }
                    case ConsoleKey.D:
                        {
                            //TODO: eventually delete this block
                            Console.WriteLine("Current area's zone points:");
                            foreach (KeyValuePair<PositionKey, DestinationCoordinates> entry in zonePoints)
                            {
                                Console.WriteLine("Target position: [i] {0}, [j] {1} \nDestination Coordinates: [i] {2}, [j] {3}, [mapID] {4}", entry.Key.i, entry.Key.j, entry.Value.iDestination, entry.Value.jDestination, entry.Value.MapID);
                            }
                            Console.WriteLine("Current Map ID: {0}", this.ID);
                            Thread.Sleep(minInputTime);
                            break;
                        }
                    case ConsoleKey.Decimal:
                        {
                            //TODO: delete
                            int[] testPos = new int[2];
                            int iTest;
                            int jTest;
                            string input;

                            Console.WriteLine("enter i:");
                            input = Console.ReadLine();
                            bool iProceed = Int32.TryParse(input, out iTest);
                            if (iProceed)
                            {
                                Console.WriteLine("enter j:");
                                input = Console.ReadLine();
                                bool jProceed = Int32.TryParse(input, out jTest);
                                if (jProceed)
                                {
                                    testPos = new int[] { iTest, jTest };
                                    //testPos[0] = iTest;
                                    //testPos[1] = jTest;
                                    Console.WriteLine("[i] {0} [j] {1}", testPos[0], testPos[1]);

                                    Console.WriteLine("Attempting to read Dictionary<PositionKey, DestinationCoords>()");
                                    try
                                    {
                                        //Console.WriteLine("[MapID] {0}", zonePoints[testPos].MapID);
                                        Console.WriteLine("[MapID] {0}", zonePoints[new PositionKey(iTest, jTest)].MapID);
                                        //hory shet, that works
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Exception: {0}", e.ToString());
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Nope.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nope.");
                            }

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        #endregion

        void PrintZoneName()
        {
            for (int i = 0; i < areaName.Length + 4; i++)
            {
                Console.Write('-');
            }
            Console.Write(Environment.NewLine);
            Console.WriteLine("| {0} |", areaName);
            for (int i = 0; i < areaName.Length + 4; i++)
            {
                Console.Write('-');
            }
            Console.Write(Environment.NewLine);
        }

        public void Display()
        {
            int rowLength = thisMap.GetLength(0);
            int colLength = thisMap.GetLength(1);

            Console.Clear();

            PrintZoneName();

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (Status.blind)
                    {
                        if (SightCheck(i, j))
                        {
                            Console.Write("{0} ", thisMap[i, j]);
                        }
                        else
                        {
                            Console.Write("  ");
                        }
                    }
                    else
                    {
                        Console.Write("{0} ", thisMap[i, j]);
                        //space to make the map display square-ish
                    }
                }
                Console.Write(Environment.NewLine);
            }
        }
        
        #region misc functions
        int MinMaxCheck(int input, int min, int max)
        {
            if (input < min)
            {
                return min;
            }
            else if (input > max)
            {
                return max;
            }
            else
            {
                return input;
            }
        }

        bool SightCheck(int i, int j)
        {
            //r^2 = x^2 + y^2
            float y = (float)(i - playerPosition[0]);
            float x = (float)(j - playerPosition[1]);
            if ((int)Math.Sqrt(y * y + x * x) <= Status.sightRadius)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
