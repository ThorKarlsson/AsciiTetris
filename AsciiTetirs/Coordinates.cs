using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiTetris
{
    public class Coordinates : IEquatable<Coordinates>
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(Coordinates obj)
        {
            if (this.x == obj.x && this.y == obj.y)
            {
                return true;
            }
            return false;
        }
    }
}
