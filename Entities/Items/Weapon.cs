using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gra
{
    public class Weapon:Item,IAttack
    {
        public Weapon() { }
        public Weapon(Weapon weapon) : this(weapon.Name, weapon.Description, weapon.IsStacked, weapon.BuyPrice, weapon.SellPrice, weapon.MinDamage, weapon.MaxDamage) { }

        public Weapon(string name, string desc, bool stack, int buyPrice, int sellPrice, int minDmg, int maxDmg) : base(name, desc, stack, buyPrice, sellPrice)
        {
            _minDamage = minDmg;
            _maxDamage = maxDmg;
        }

        private int _minDamage;
        private int _maxDamage;

        public int MinDamage { get { return _minDamage; } }
        public int MaxDamage { get { return _maxDamage; } }

        public float GetDamage()
        {
            Random random = new Random();
            return random.Next(_minDamage, _maxDamage);
        }

        public override string ToString()
        {
            return
                $"     ,  /\\  .  \n" +
                $"    //`-||-'\\\\               [{this.Name}]\n" +
                $"   (| -=||=- |)              {this.Description}\n" +
                $"    \\\\,-||-.//               Attack: {this._minDamage} - {this._maxDamage}\n" +
                $"     `  || '  \n" +
                $"        ||\n" +
                $"        ||\n" +
                $"        ||\n" +
                $"        ||\n" +
                $"        ||\n" +
                $"        ()\n";
        }

        public override string BuyInfo()
        {
            return $"{Name} | Atak: {_minDamage}-{_maxDamage} | {BuyPrice}";
        }
    }
}
