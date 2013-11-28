using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiTetris
{
    public static class Tetrimino
    {
        public static bool[,] NextPiece()
        { 
            Random r = new Random();
            int option = r.Next(5);

            switch (option)
            {
                case 0:
                    return LineShape();
                case 1:
                    return BlockShape();
                case 2:
                    return LShape();
                case 3:
                    return ReverseLShape();
                case 4:
                    return ZShape();
                default:
                    throw new InvalidOperationException("Invalid shape option, random number out of range");
            }
        }

        private static bool[,] LineShape()
        {
            var shape = new bool[4, 1];
            for (int x = 0; x < 4; x++)
            {
                shape[x, 0] = true;
            }

            return shape;
        }

        private static bool[,] BlockShape()
        {
            var shape = new bool[2, 2];
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    shape[x, y] = true;
                }
            }

            return shape;
        }

        private static bool[,] LShape()
        {
            var shape = new bool[2, 3];
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (x == 0 || y == 2)
                    {
                        shape[x, y] = true;
                    }
                }
            }

            return shape;
        }

        private static bool[,] ReverseLShape()
        {
            var shape = new bool[2, 3];
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (x == 1 || y == 2)
                    {
                        shape[x, y] = true;
                    }
                }
            }

            return shape;
        }

        private static bool[,] ZShape()
        {
            var shape = new bool[2, 3];
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (!(x == 0 && y == 0))
                    {
                        if (!(x == 1 && y == 2))
                        {
                            shape[x, y] = true;
                        }
                    }
                }
            }

            return shape;
        }

    }
}
