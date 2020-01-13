using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gra;

namespace AxeOfExile.Entities.Creatures
{
    public partial class Player : Creature
    {
        public int Money { get; set; }
        public List<Item> inventory { get; set; }
        public Boot Boot { get; set; }
        public Legs Legs { get; set; }
        public Helmet Helmet { get; set; }
        public Armor Armor { get; set; }
        public Shield Shield { get; set; }
        public Weapon Weapon { get; set; }

        
    }
}
