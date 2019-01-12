using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    public struct Line
    {
        //will represent start and end of line
        public int iAnchor;
        public int jAnchor;
        public int iEnd;
        public int jEnd;
        //public int length;
        public enum Direction { horizontal, vertical, diagonal };

        public Line(int _iAnchor, int _jAnchor, int length, Direction direction)
        {
            iAnchor = _iAnchor;
            jAnchor = _jAnchor;

            #region clutter
            //if ((int)direction == 0)
            //{
            //    iEnd = iAnchor;
            //    jEnd = jAnchor + length - 1;
            //}
            //else if ((int)direction == 1)
            //{
            //    iEnd = iAnchor + length - 1;
            //    jEnd = jAnchor;
            //}
            //else
            //{
            //    iEnd = iAnchor + length - 1;
            //    jEnd = jAnchor + length - 1;
            //}
            #endregion

            if ((int)direction < 2)
            {
                iEnd = iAnchor + ((length - 1) * (int)direction);
                jEnd = jAnchor + ((length - 1) * (1 - (int)direction));
            }
            else
            {
                iEnd = iAnchor + length - 1;
                jEnd = jAnchor + length - 1;
            }
        }
    }
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
        Space or some other char to represent navigable water

        **/

        //bool isInitialized = false;
        bool isInitialized = true;
        //only one map is ever going to be used at once, so whatever
        static int columns;
        const int jMin = 5;
        const int jMax = 30;
        static int rows;
        const int iMin = 5;
        const int iMax = 30;

        //char[,] map;
        char wall = '#';
        char floor = ' ';
        char playerPos = 'O';

        int encounterRate;
        //Creature implementation might need some work for this to not be a pain in the arse:
        //Creature[] mobList;

        public Map(int _rows, int _columns)
        {
            rows = MinMaxCheck(_rows, iMin, iMax);
            //rows = _rows;
            columns = MinMaxCheck(_columns, jMin, jMax);
            //columns = _columns;
        }

        char[,] thisMap = new char[rows, columns];

        //public void CreateEmpty(int userWidth, int userHeight)
        //{
        //    columns = MinMaxCheck(userWidth, jMin, jMax);
        //    rows = MinMaxCheck(userHeight, iMin, iMax);

        //    isInitialized = true;
        //}

        public void CreateBorder()
        {
            if (isInitialized)
            {
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
            }
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
