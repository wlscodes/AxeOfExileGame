using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gra
{
    public class Legs:Item,IDefense
    {
        public Legs() { }
        public Legs(Legs legs) :
            this(legs.Name, legs.Description, legs.IsStacked, legs.BuyPrice, legs.SellPrice, legs.GetDefense()) { }

        public Legs(string name, string desc, bool stack, int buyPrice, int sellPrice, float defense) : base(name, desc, stack, buyPrice, sellPrice)
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
                $"   ,== c ==.\n" +
                $"   |_ /|\\_ |         [{this.Name}]\n" +
                $"   |  ´|`  |         {this.Description}\n" +
                $"   |   |   |         Defense: {this.GetDefense()}\n" +
                $"   |   |   |\n" +
                $"   |___|___|\n";
        }

        public override string BuyInfo()
        {
            return $"{Name} | Pancerz: {GetDefense()} | {BuyPrice}";
        }
    }
}
