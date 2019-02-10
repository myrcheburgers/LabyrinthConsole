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
        //TODO: create separate (void) controller class for map creation

        /**
        
        End goal:

        ######
        #    #
        ###  #
        ##   #
        ######

        With # representing walls/boundaries
        Space or some other char to represent navigable space

        **/

        //int[] for i, j and int for destination map ID
        public Dictionary<int[], DestinationCoordinates> zonePoints = new Dictionary<int[], DestinationCoordinates>();

        public string areaName;
        public int ID;
        //bool defaultBlind;
        bool movementMode = false;

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
        char playerPos = 'O';
        char zonePos = 'X';

        //[i, j, destinationZoneID
        int[] zonePoint = new int[3];

        int encounterRate;
        //Creature implementation might need some work for this to not be a pain in the arse:
        //Creature[] mobList;

        //constructor + initialization stuff
        char[,] thisMap;
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

        #region wall creation
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
        public void CreateZonePoint(int i, int j, DestinationCoordinates destination)
        {
            thisMap[i, j] = zonePos;
            zonePoints.Add(new int[] { i, j }, destination);
        }

        void ZoneTransition(int[] position)
        {
            DestinationCoordinates dest = zonePoints[position];
            LoadNewArea(ZoneList.mapIDList[dest.MapID], new int[] { dest.iDestination, dest.jDestination });
        }

        void LoadNewArea(Map destination, int[] destinationPlayerPosition)
        {
            this.zonePoints = destination.zonePoints;

            this.areaName = destination.areaName;
            this.ID = destination.ID;
            //this.movementMode = destination.movementMode;

            this.thisMap = destination.thisMap; //yo dawg...

            //constants: jMin, jMax, iMin, iMax, minInputTime
            this.playerPosition = destinationPlayerPosition;

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

                try
                {
                    ZoneTransition(playerPosition);
                }
                catch(KeyNotFoundException)
                {
                    movementMode = false;
                    Console.WriteLine("Position: {0}, {1}", playerPosition[0], playerPosition[1]);
                    Console.WriteLine("Position [array]: {0}", playerPosition);
                    Console.WriteLine("Attempted position: {0}, {1}", playerPosition[0] - 1, playerPosition[1]);
                    Console.WriteLine("Press Enter to continue.");
                    Console.ReadLine();

                    //TODO:Apparently ZoneTransition can't be fed the entire array?
                }
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
        }
        void MoveLeft()
        {
            if (thisMap[playerPosition[0], playerPosition[1] - 1] == floor)
            {
                AdjustTile(floor);
                playerPosition[1] -= 1;
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
