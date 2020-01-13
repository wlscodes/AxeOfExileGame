using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gra
{
    public abstract class Item
    {
        protected int _buyPrice;
        protected string _descriptions;
        protected bool _isStacked;
        protected string _name;
        protected int _sellPrice;

        public Item() { }

        protected Item(string name, string desc, bool stack, int buyPrice, int sellPrice)
        {
            Name = name;
            Description = desc;
            IsStacked = stack;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
        }

    public int BuyPrice
        {
            get { return _buyPrice; }
            set { _buyPrice = value; }
        }

    public string Description
        {
            get { return _descriptions; }
            set { _descriptions = value; }
        }

     public bool IsStacked
        {
            get { return _isStacked; }
            set { _isStacked = value; }
        }
    
        public string Name
        {

            get { return _name; }
            set { _name = value; }
        }

        public int SellPrice
        {
            get { return _sellPrice; }
            set { _sellPrice = value; }
        }

        public virtual string BuyInfo() 
        {
            return $"{Name} Cena kupna: {BuyPrice}";
        }
    }
}
