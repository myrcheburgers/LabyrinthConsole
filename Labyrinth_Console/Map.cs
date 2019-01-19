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

        //bool isInitialized = false;
        //bool isInitialized = true;
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
        char floor = ' ';
        char playerPos = 'O';

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

        void CreateWall(int i, int j)
        {
            thisMap[i, j] = wall;
        }

        void CreateWallLine(int iAnchor, int jAnchor, int length, int dir)
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

        void CreateFloor(int i, int j)
        {
            thisMap[i, j] = floor;
        }

        void ClearMap()
        {
            //todo because I'm dumb
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
                    default:
                        {
                            break;
                        }
                }
            }
        }
        #endregion

        public void Display()
        {
            int rowLength = thisMap.GetLength(0);
            int colLength = thisMap.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write("{0} ", thisMap[i, j]);
                    //space to make the map display square-ish
                }
                Console.Write(Environment.NewLine);
            }
        }

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
    }
}
