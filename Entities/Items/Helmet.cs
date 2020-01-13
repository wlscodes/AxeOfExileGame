using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gra
{
    public class Helmet:Item,IDefense
    {
        public Helmet() { }
        public Helmet(Helmet helmet)
            : this(helmet.Name, helmet.Description, helmet.IsStacked, helmet.BuyPrice, helmet.SellPrice, helmet.GetDefense()) { }

        public Helmet(string name, string desc, bool stack, int buyPrice, int sellPrice, float defense) : base(name, desc, stack, buyPrice, sellPrice)
        {
            _defense = defense;
        }

        private float _defense;

        public float GetDefense()
        {
            return _defense;
        }

        public override string ToString()
        {
            return
                $"           ###\n" +
                $"           [ ]               [{this.Name}]\n" +
                $"          _[ ]_              {this.Description}\n" +
                $"         ( [ ] )             Defense: {this.GetDefense()}\n" +
                $"        (___|___)\n" +
                $"       ( \\  V  / )\n" +
                $"        \\ \\   / /\n" +
                $"         \\_| |_/\n";
        }

        public override string BuyInfo()
        {
            return $"{Name} | Pancerz: {GetDefense()} | {BuyPrice}";
        }
    }
}
