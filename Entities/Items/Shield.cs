using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gra
{
    public class Shield:Item,IDefense
    {
        public Shield() { }
        public Shield(Shield shield) :
            this(shield.Name, shield.Description, shield.IsStacked, shield.BuyPrice, shield.SellPrice, shield.GetDefense())
        { }

        public Shield(string name, string desc, bool stack, int buyPrice, int sellPrice, float defense) : base(name, desc, stack, buyPrice, sellPrice)
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
                $"   \\_____________/\n" +
                $"   |     | |     |                     [{this.Name}]\n" +
                $"   |     | |     |                     {this.Description}\n" +
                $"   |_____| |_____|                     Defense: {this.GetDefense()}\n" +
                $"   |_____   _____|\n" +
                $"   |     | |     |\n" +
                $"   |     | |     |\n" +
                $"   \\     | |     /\n" +
                $"    \\    | |    /\n" +
                $"     \\   | |   /\n" +
                $"      \\  | |  /\n" +
                $"       \\ | | /\n" +
                $"        \\|_|/";
        }

        public override string BuyInfo()
        {
            return $"{Name} | Pancerz: {GetDefense()} | {BuyPrice}";
        }
    }
}
