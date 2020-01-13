using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile.Cartography
{
    public interface IMapObject
    {
        char GetMapSign();
        ConsoleColor GetMapColor();
    }
}
