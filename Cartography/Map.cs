using System;
using System.Collections.Generic;
using System.Text;
using AxeOfExile.Entities;
using AxeOfExile.Entities.Creatures;
using MapGeneratorSpace;

namespace AxeOfExile.Cartography
{
    /// <summary>
    /// Class represents game map
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Array with map tiles
        /// </summary>
        private Tile[,] _map;

        /// <summary>
        /// Return the X axis size
        /// </summary>
        public int XAxis { get { return _map.GetLength(0); } }

        /// <summary>
        /// Return the Y axis size
        /// </summary>
        public int YAxis { get { return _map.GetLength(1); } }

        public Map(int width, int height)
        {
            GenerateMap(width, height);
        }

        public Map() { }

        /// <summary>
        /// This function use MapGenerator class for generate world map
        /// </summary>
        /// <param name="width">Map width</param>
        /// <param name="height">Map height</param>
        private void GenerateMap(int width, int height)
        {
            MapGenerator mapGenerator = new MapGenerator(width, height);
            int[,] mapTemplate = mapGenerator.GetMapTemplate(out double firstRandomValue);
            FillMap(mapTemplate, firstRandomValue);

        }

        /// <summary>
        /// This function fill the map with required tiles
        /// </summary>
        /// <param name="mapTemplate">Map template from map generator</param>
        /// <param name="firstRandomValue">First random value from map generator. It is used for count difference between tiles</param>
        private void FillMap(int[,] mapTemplate, double firstRandomValue)
        {
            _map = new Tile[mapTemplate.GetLength(0), mapTemplate.GetLength(1)];

            for (int i = 0; i < mapTemplate.GetLength(0); i++)
            {
                for (int j = 0; j < mapTemplate.GetLength(1); j++)
                {
                    if (mapTemplate[i, j] < firstRandomValue * 0.6)
                    {
                        // Generate tile for water
                        _map[i, j] = new Tile('O', ConsoleColor.Cyan, true);
                    }
                    else if (mapTemplate[i, j] < firstRandomValue)
                    {
                        // Generate tile for forest
                        _map[i, j] = new Tile('^', ConsoleColor.Green, false);
                    }
                    else if (mapTemplate[i, j] < firstRandomValue * 1.4)
                    {
                        // Generate tile for grass
                        _map[i, j] = new Tile('#', ConsoleColor.DarkYellow, false);
                    }
                    else
                    {
                        // Generate tile for mountain
                        _map[i, j] = new Tile('M', ConsoleColor.DarkGray, true);
                    }
                }
            }
        }

        /// <summary>
        /// Function draw a mini map
        /// </summary>
        /// <param name="p">Center position</param>
        /// <param name="size">Ray size</param>
        public void DisplayMiniMap(Point p, int size, char playerSign)
        {
            for(int i = p.X - size; i <= p.X + size; i++)
            {
                for(int j = p.Y - size; j <= p.Y + size; j++)
                {
                    if (i < 0 || j < 0 || i >= XAxis || j >= YAxis)
                    {
                        Console.Write(' ');
                    }
                    else if (j == p.Y && i == p.X)
                    {
                        Console.Write(playerSign);
                    }
                    else
                    {
                        _map[i, j].DrawMinimap();
                    }

                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Function draws a full map
        /// </summary>
        public void DisplayFullMap()
        {
            for (int i = 0; i < XAxis; i++)
            {
                for (int j = 0; j < YAxis; j++)
                {
                    _map[i, j].Draw();
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Return tile sign on that position
        /// </summary>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        /// <returns>Return tile sign if it is available, else return space(' ')</returns>
        public char GetTileSign(int posX, int posY)
        {
            if (posX < 0 || posY < 0 || posX > this.XAxis || posY > this.YAxis)
                return ' ';
            return _map[posX, posY].Sign;
        }

        public Monster GetMonster(int posX, int posY)
        {
            return _map[posX, posY].MapObject as Monster;
        }

        public void RemoveMonster(int posX, int posY)
        {
            _map[posX, posY].MapObject = null;
        }


        public bool CompareObject(object o, int posX, int posY)
        {
            if (posX < 0 || posY < 0 || posX > this.XAxis || posY > this.YAxis) return false;
            if (o is null) return false;
            if (_map[posX, posY].MapObject is null) return false;
            if (_map[posX, posY].MapObject.GetType().Equals(o.GetType())) return true;
            return false;
        }

        /// <summary>
        /// Set MapObject on that position
        /// </summary>
        /// <param name="posX">X position</param>
        /// <param name="posY">Y position</param>
        /// <param name="mapObject">Map object</param>
        public void SetMapObject(int posX, int posY, IMapObject mapObject)
        {
            if (posX < 0 || posY < 0 || posX > this.XAxis || posY > this.YAxis)
                return;

            _map[posX, posY].MapObject = mapObject;
        }
    }
}
