using System;

namespace MapGeneratorSpace
{
    /// <summary>
    /// Map Generator used diamond-square algorithm for generate the map
    /// </summary>
    public class MapGenerator
    {
        /// <summary>
        /// Width declared by user
        /// </summary>
        private readonly int _playerWidth;

        /// <summary>
        /// Height declared by user
        /// </summary>
        private readonly int _playerHeight;

        /// <summary>
        /// Map size. This value is counted automatically for parameters provided by user and it must be a power of 2 number.
        /// </summary>
        private readonly int _size;

        /// <summary>
        /// Array contains generated map
        /// </summary>
        private int[,] _map;

        /// <summary>
        /// Number which is added to field after counting average value
        /// </summary>
        private double _randomValue;

        /// <summary>
        /// First random value
        /// </summary>
        private readonly double _firstRandomValue;

        /// <summary>
        /// Reference for Random class object
        /// </summary>
        private readonly Random _random;

        /// <summary>
        /// Map generator constructor
        /// </summary>
        /// <param name="height">Map height (Min: 16)</param>
        /// <param name="width">Map width (Min: 16)</param>
        public MapGenerator(int width, int height)
        {
            //_size = (int)((Math.Log(_size, 2) % 10 == 0) ? size : Math.Pow(2, (int)Math.Log(_size, 2)));
            _playerWidth = (width < 16) ? 16 : width;
            _playerHeight = (height < 16) ? 16 : height;
            _size = CountMapSizeForGenerator();
            _map = new int[_size, _size];
            _random = new Random();
            _randomValue = Math.Log(_size, 2);
            _firstRandomValue = (int)_randomValue;
        }

        /// <summary>
        /// Count the minimum square side length
        /// </summary>
        /// <returns>Returns the minimal length</returns>
        private int CountMapSizeForGenerator()
        {
            int side = (_playerHeight > _playerWidth) ? _playerHeight : _playerWidth;

            double power = Math.Ceiling(Math.Log(side, 2));

            return (int)Math.Pow(2, power);
        }

        /// <summary>
        /// This is an engine function for generate map
        /// </summary>
        private void Generate()
        {
            _map = new int[_size + 1, _size + 1];
                    
            _map[0, 0] = _random.Next(1, (int)_randomValue);
            _map[_size, 0] = _random.Next(1, (int)_randomValue);
            _map[0, _size] = _random.Next(1, (int)_randomValue);
            _map[_size, _size] = _random.Next(1, (int)_randomValue);

            int step = _size;
            
            while (step > 1)
            {
                for(int i = 0; i < _size; i += step)
                {
                    for(int j = 0; j < _size; j += step)
                    {
                        DiamondStep(i, j, step);

                        SquareStep(i + step / 2, j + step / 2, step/2);
                    }
                }
                step /= 2;
                _randomValue -= _random.NextDouble();
                //_randomValue += _randomValue < 1d ? 0 : _random.NextDouble() + 1;
                _randomValue = _randomValue < 1d ? 1 : _randomValue;
            }
        }

        /// <summary>
        /// Set the midpoint of square to be the average of the four corner points
        /// </summary>
        /// <param name="x">Parameter X for top-left corner</param>
        /// <param name="y">Parameter Y for top-left corner</param>
        /// <param name="step">Distance between corners</param>
        private void DiamondStep(int x, int y, int step)
        {
            int sum = 0;

            sum += _map[x, y];
            sum += _map[x + step, y];
            sum += _map[x, y + step];
            sum += _map[x + step, y + step];

            _map[x + step / 2, y + step / 2] = (sum / 4) /*+ _random.Next(1, (int)_randomValue)*/;
        }

        /// <summary>
        /// Function that count avarage value for diamond corner
        /// </summary>
        /// <param name="x">Parameter X of diamond corner</param>
        /// <param name="y">Parameter Y of diamond corner</param>
        /// <param name="step">Distance between corners</param>
        private void SquarePoint(int x, int y, int step)
        {
            int sum = 0, count = 0;

            if (x < 0 || x >= _size + 1 || y < 0 || y > _size + 1)
                return;

            if(x + step <= _size && x + step >= 0 && y <= _size && y >= 0)
            {
                sum += _map[x + step, y];
                count++;
            }
            
            if(y + step <= _size && y + step >= 0 && x <= _size && x >= 0)
            {
                sum += _map[x, y + step];
                count++;
            }
            
            if(x - step >= 0 && x - step <= _size && y >= 0 && y <= _size)
            {
                sum += _map[x - step, y];
                count++;
            }
            
            if(y - step >= 0 && y - step <= _size && x >= 0 && x <= _size)
            {
                sum += _map[x, y - step];
                count++;
            }
            
            if(count > 0)
            _map[x, y] = sum / count + _random.Next(1, (int)_randomValue);
        }

        /// <summary>
        /// Set the midpoint of diamond to be the average of the four corner points plus a random value
        /// </summary>
        /// <param name="x">Parameter X for center point of diamond</param>
        /// <param name="y">Parameter Y for center point of diamond</param>
        /// <param name="step">Distance between points in straight line</param>
        private void SquareStep(int x, int y, int step)
        {
            SquarePoint(x + step, y, step);
            SquarePoint(x, y + step, step);
            SquarePoint(x - step, y, step);
            SquarePoint(x, y - step, step);
        }
        
        /// <summary>
        /// This function resize map for user requirements
        /// </summary>
        private void Resize()
        {
            if (_map.GetLength(0) == _playerHeight && _map.GetLength(1) == _playerWidth) return;

            int[,] tempMap = new int[_playerHeight, _playerWidth];

            for (int i = 0; i < _playerHeight; i++)
            {
                for(int j = 0; j < _playerWidth; j++)
                {
                    tempMap[i, j] = _map[i, j];
                }
            }

            _map = tempMap;
        }

        /// <summary>
        /// This function is used for generate map view
        /// </summary>
        [Obsolete("This function was used in tests for display a map with 4 types of terrain.")]
        public void DisplayMap()
        {
            for(int i = 0; i < _map.GetLength(0); i++)
            {
                for(int j = 0; j < _map.GetLength(1); j++)
                {
                    if (_map[i, j] < _firstRandomValue * 0.6)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write('O');
                    }
                    else if (_map[i, j] < _firstRandomValue)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('^');

                    }
                    else if (_map[i, j] < _firstRandomValue * 1.4)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write('#');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('M');
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Init function
        /// </summary>
        /// <returns>Return copy of two-dimensional array with values</returns>
        public int[,] GetMapTemplate()
        {
            Generate();
            Resize();
            return _map.Clone() as int[,];
        }

        /// <summary>
        /// Init function
        /// </summary>
        /// <param name="firstRandomValue">This parameter set first random value</param>
        /// <returns>Return copy of two-dimensional array with values</returns>
        public int[,] GetMapTemplate(out double firstRandomValue)
        {
            firstRandomValue = this._firstRandomValue;
            return GetMapTemplate();
        }
    }
}
