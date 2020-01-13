using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gra
{
    public class Armor:Item,IDefense
    {
        public Armor() { }
        public Armor(Armor armor)
            : this(armor.Name, armor.Description, armor.IsStacked, armor.BuyPrice, armor.SellPrice, armor.GetDefense()) { }

        public Armor(string name, string desc, bool stack, int buyPrice, int sellPrice, float defense) : base(name, desc, stack, buyPrice, sellPrice)
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
                $"      .---  ----.              [{this.Name}]\n" +
                $"     /    /*\\   \\              {this.Description}\n" +
                $"    / /| ***** |\\ \\            Defense: {this.GetDefense()}\n" +
                $"   / / |   *   | \\ \\\n" +
                $"       |_______|\n";
        }

        public override string BuyInfo()
        {
            return $"{Name} | Pancerz: {GetDefense()} | {BuyPrice}";
        }
    }
}
