using AxeOfExile.Cartography;
using AxeOfExile.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile.Entities
{
    public class City : IMapObject
    {
        public ConsoleColor GetMapColor()
        {
            return ConsoleColor.DarkBlue;
        }

        public char GetMapSign()
        {
            return 'X';
        }

        public Player player { get; set; }
    }
}
