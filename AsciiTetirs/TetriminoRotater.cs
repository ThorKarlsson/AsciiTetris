using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiTetris
{
    public static class TetriminoRotater
    {
        public static List<Coordinates> CounterClockwiseRotation(List<Coordinates> pieceLocation)
        {
            List<Coordinates> newLocation = new List<Coordinates>();

            Coordinates pivotPoint = FindPivotPoint(pieceLocation);
            foreach (Coordinates c in pieceLocation)
            {
                newLocation.Add(new Coordinates(c.x - pivotPoint.x, c.y - pivotPoint.y));
            }

            foreach (Coordinates c in newLocation)
            {
                int newX = -c.y;
                int newY = c.x;

                c.x = pivotPoint.x - newX;
                c.y = pivotPoint.y - newY;
            }

            return newLocation;
        }

        public static List<Coordinates> ClockwiseRotation(List<Coordinates> pieceLocation)
        {
            List<Coordinates> newLocation = new List<Coordinates>();

            Coordinates pivotPoint = FindPivotPoint(pieceLocation);
            foreach (Coordinates c in pieceLocation)
            {
                newLocation.Add(new Coordinates(c.x - pivotPoint.x, c.y - pivotPoint.y));
            }

            foreach (Coordinates c in newLocation)
            {
                int newX = c.y;
                int newY = -c.x;

                c.x = pivotPoint.x - newX;
                c.y = pivotPoint.y - newY;
            }

            return newLocation;
        }

        private static Coordinates FindPivotPoint(List<Coordinates> coordinates)
        {
            int x = 20; int y = 20;
            foreach(Coordinates c in coordinates)
            {
                if(c.x < x)
                {
                    x = c.x;
                }
                if(c.y < y)
                {
                    y = c.y;
                }
            }
            return new Coordinates(x, y);
        }
    }
}
