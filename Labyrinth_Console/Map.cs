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

        bool isInitialized = false;
        int columns;
        const int jMin = 5;
        const int jMax = 30;
        int rows;
        const int iMin = 5;
        const int iMax = 30;

        char[,] map;
        char wall = '#';
        char floor = ' ';


        void CreateEmpty(int userWidth, int userHeight)
        {
            columns = MinMaxCheck(userWidth, jMin, jMax);
            rows = MinMaxCheck(userHeight, iMin, iMax);

            isInitialized = true;
        }

        void CreateBorder()
        {
            if (isInitialized)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        if (i == 0 || j == 0 || i == rows - 1 || j == columns - 1)
                        {
                            map[i, j] = wall;
                        }
                        else
                        {
                            map[i, j] = floor;
                        }
                    }
                }
            }
        }

        void CreateWall(int i, int j)
        {
            map[i, j] = wall;
        }

        void CreateWallLine(int iAnchor, int jAnchor, int length)
        {
            //TODO
        }

        void CreateFloor(int i, int j)
        {
            map[i, j] = floor;
        }

        void ClearMap()
        {
            //todo because I'm dumb
        }

        void Display(char[,] inputMap)
        {
            int rowLength = inputMap.GetLength(0);
            int colLength = inputMap.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write("{0}", inputMap[i, j]);
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
