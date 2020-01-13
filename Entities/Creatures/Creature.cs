using AxeOfExile.Cartography;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile.Entities
{
    public abstract class Creature : IMapObject
    {
        protected double _health;

        protected double _strength;

        protected string _name;

        private Point _position;

        private readonly char _sign;

        readonly ConsoleColor _consoleColor;

        protected Creature(string name, Point position, double strength, double health, char sign, ConsoleColor cc)
        {
            _name = name;
            _position = position;
            _strength = strength;
            _health = health;
            _sign = sign;
            _consoleColor = cc;
        }

        public Creature() { }

        public double Health
        {
            get { return (_health <= MaxHealth) ? _health : MaxHealth; }
            set { _health = value; }
        }

        public double MaxHealth
        {
            get { return _strength * 10; }
        }

        public double Strength
        {
            get { return _strength; }
            set { _strength = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Point Position { get => _position; set => _position = value; }

        public ConsoleColor GetMapColor()
        {
            return _consoleColor;
        }

        public char GetMapSign()
        {
            return _sign;
        }
    }
}
