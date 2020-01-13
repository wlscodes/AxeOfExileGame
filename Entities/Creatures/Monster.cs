using AxeOfExile.Cartography;
using gra;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile.Entities.Creatures
{
    public class Monster : Creature
    {
        private readonly int _minCoinDrop;
        private readonly int _maxCoinDrop;

        public char SignType;
        
        public int CoinDrop { get { return new Random().Next(_minCoinDrop, _maxCoinDrop); } }

        public Item dropItem;
        
        private int dropPercent;

        public bool Droped 
        { 
            get 
            {
                Random r = new Random();
                return r.Next(0, 100) < dropPercent;
            }
        }

        public Monster() { }

        public Monster(Monster monster, Point position)
            : this(monster._name, monster._strength, monster._health, monster._minCoinDrop, monster._maxCoinDrop, position, '@', ConsoleColor.DarkRed, monster.SignType, monster.dropItem, monster.dropPercent) { }

        
        public Monster(string name, double strength, double health, int minCoin, int maxCoin, char signType)
            : this(name, strength, health, minCoin, maxCoin, signType, null, 0) { }

        public Monster(string name, double strength, double health, int minCoin, int maxCoin, char signType, Item dropItem, int chance)
            : this(name, strength, health, minCoin, maxCoin, null, '@', ConsoleColor.DarkRed, signType, dropItem, chance) { }

        private Monster(string name, double strength, double health, int minCoin, int maxCoin, Point position, char sign, ConsoleColor cc, char signType, Item dropItem, int chance)
            :base(name, position, strength, health, sign, cc)
        {
            _minCoinDrop = minCoin;
            _maxCoinDrop = maxCoin;
            SignType = signType;
            this.dropItem = dropItem;
            dropPercent = chance;
        }

        public override string ToString()
        {
            return
@"    ,_                    _," + "\n" +
@"    ) '-._  ,_    _,  _.-' (" + "\n" +
@"    )  _.-'.|\\--//|.'-._  (" + $"            {this.Name}\n" +
@"     )'   .'\/o\/o\/'.   `(" + $"             Zycie: {this.Health}\n" +
@"      ) .' . \====/ . '. (" + $"              Sila: {this.Strength}\n" +
@"       )  / <<    >> \  (" + "\n" +
@"        '-._/``  ``\_.-'" + "\n" +
@"          __\\'--'//__" + "\n" +
@"         (((''`  `'')))" + "\n";
        }
    }
}
