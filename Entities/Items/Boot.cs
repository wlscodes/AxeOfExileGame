using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gra
{
    public class Boot:Item,IDefense
    {
        public Boot() { }
        public Boot(Boot boot)
            : this(boot.Name, boot.Description, boot.IsStacked, boot.BuyPrice, boot.SellPrice, boot.GetDefense()) { }
        public Boot(string name, string desc, bool stack, int buyPrice, int sellPrice, float defense) : base(name, desc, stack, buyPrice, sellPrice)
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
            return  $"      ____ ___\n" +
                    $"      )  =\\  =\\                 [{this.Name}]\n" +
                    $"     /    =\\  =\\                {this.Description}\n" +
                    $"     \\      `-._`-._            Defense: {this.GetDefense()}\n" +
                    $"      )__(`\\____)___)";
        }

        public override string BuyInfo()
        {
            return $"{Name} | Pancerz: {GetDefense()} | {BuyPrice}";
        }
    }
}
