using AxeOfExile.Entities;
using AxeOfExile.Entities.Creatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile.Cartography
{
    /// <summary>
    /// Struct used for represent map ground
    /// </summary>
    public class Tile
    {
        public Tile(char sign, ConsoleColor color, bool isBlocked) : this(sign, color, isBlocked, null) { }
        
        private Tile(char sign, ConsoleColor color, bool isBlocked, IMapObject mapObject)
        {
            this.Sign = sign;
            this.Color = color;
            this.IsBlocked = isBlocked;
            this.MapObject = mapObject;
        }

        /// <summary>
        /// Tile display sign
        /// </summary>
        public char Sign { get; set; }

        /// <summary>
        /// Tile display color
        /// </summary>
        public ConsoleColor Color { get; set; }

        /// <summary>
        /// Flag to check if MapObject can stand on tile
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Store game object on this field
        /// </summary>
        public IMapObject MapObject { get; set; }

        public override string ToString()
        {
            return Sign.ToString();
        }

        /// <summary>
        /// Draw a tile
        /// </summary>
        public void Draw()
        {
            ConsoleColor actual = Console.ForegroundColor;
            //Console.ForegroundColor = this.Color;
            Console.ForegroundColor = (this.MapObject is Player || this.MapObject is City) ? this.MapObject.GetMapColor() : this.Color;
            //Console.Write(this);
            Console.Write((this.MapObject is Player || this.MapObject is City) ? this.MapObject.GetMapSign() : this.Sign);
            Console.ForegroundColor = actual;
        }

        /// <summary>
        /// Draw a tile in minimap
        /// </summary>
        public void DrawMinimap()
        {
            ConsoleColor actual = Console.ForegroundColor;
            Console.ForegroundColor = (this.MapObject is null) ? this.Color : this.MapObject.GetMapColor();
            Console.Write((this.MapObject is null) ? this.Sign : this.MapObject.GetMapSign());
            Console.ForegroundColor = actual;
        }
    }
}
