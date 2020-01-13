using AxeOfExile.Cartography;
using gra;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile.Entities.Creatures
{
    public partial class Player : Creature
    {
        public int Level { get; set; }

        public int ToNextLevel { get; set; } 

        private int _experience;
        public int Experience 
        {
            get { return _experience; }
        }

        private long lastActivity;

        public Player() { }
        public Player(string name, Point position, double strength, char sign, ConsoleColor cc) 
            : base(name, position, strength, strength * 10, sign, cc) 
        {
            inventory = new List<Item>();
            Level = 1;
            ToNextLevel = 10;
            _experience = 0;
            lastActivity = GetTime;
            Money = 25;
        }

        private long GetTime { get { return DateTimeOffset.UtcNow.ToUnixTimeSeconds(); } }

        public void Regen()
        {
            if(Health < MaxHealth)
            {
                Health += MaxHealth * (GetTime - lastActivity) / 3600;
                Health = (Health < MaxHealth) ? Health : MaxHealth;
            }
            lastActivity = GetTime;
        }

        public float WeaponDamage { get { return (Weapon is null) ? 1.0f : Weapon.GetDamage(); } }

        public float Defense
        {
            get
            {
                return ((Armor is null) ? 0 : Armor.GetDefense())
                    + ((Boot is null) ? 0 : Boot.GetDefense())
                    + ((Helmet is null) ? 0 : Helmet.GetDefense())
                    + ((Legs is null) ? 0 : Legs.GetDefense())
                    + ((Shield is null) ? 0 : Shield.GetDefense());
            }
        }

        public void DisplayEquipment()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Monety: {this.Money}");
                Console.WriteLine("==============");
                Console.WriteLine($"Helm: {((Helmet is null) ? "-= brak =-" : Helmet.Name)}");
                Console.WriteLine($"Armor: {((Armor is null) ? "-= brak =-" : Armor.Name)}");
                Console.WriteLine($"Legs: {((Legs is null) ? "-= brak =-" : Legs.Name)}");
                Console.WriteLine($"Boots: {((Boot is null) ? "-= brak =-" : Boot.Name)}");
                Console.WriteLine($"Shield: {((Shield is null) ? "-= brak =-" : Shield.Name)}");
                Console.WriteLine($"Weapon: {((Weapon is null) ? "-= brak =-" : Weapon.Name)}");
                Console.WriteLine($"\n======= PLECAK [{inventory.Count}] =======");

                Console.WriteLine("pozycja | przedmiot");
                for (int i = 0; i < inventory.Count; i++)
                {
                    Console.WriteLine($"[{i}] {inventory[i].Name}");
                }

                Console.WriteLine("[pozycja] - Wyswietl przedmiot");
                Console.WriteLine("[-1] - Wyjdz");

                int position = DataInput.GetInt("Wybierz opcje: ");

                if (position == -1) return;
                if (position < 0 || position >= inventory.Count) continue;

                Console.Clear();
                Console.WriteLine(inventory[position]);
                Console.WriteLine("[0] - Zaloz");
                Console.WriteLine("[-1] - Cofnij");

                int option = DataInput.GetInt("Wybierz opcje: ");
                if (option == 0)
                {
                    Equip(position);
                }
            }
        }

        private void Equip(int position)
        {
            if (position < 0 || position >= inventory.Count) return;

            if (inventory[position] is Armor)
            {
                if (!(Armor is null)) inventory.Add(new Armor(Armor));
                Armor = new Armor(inventory[position] as Armor);
                inventory.RemoveAt(position);
            }
            else if (inventory[position] is Boot)
            {
                if (!(Boot is null)) inventory.Add(new Boot(Boot));
                Boot = new Boot(inventory[position] as Boot);
                inventory.RemoveAt(position);
            }
            else if (inventory[position] is Helmet)
            {
                if (!(Helmet is null)) inventory.Add(new Helmet(Helmet));
                Helmet = new Helmet(inventory[position] as Helmet);
                inventory.RemoveAt(position);
            }
            else if (inventory[position] is Legs)
            {
                if (!(Legs is null)) inventory.Add(new Legs(Legs));
                Legs = new Legs(inventory[position] as Legs);
                inventory.RemoveAt(position);
            }
            else if (inventory[position] is Shield)
            {
                if (!(Shield is null)) inventory.Add(new Shield(Shield));
                Shield = new Shield(inventory[position] as Shield);
                inventory.RemoveAt(position);
            }
            else if (inventory[position] is Weapon)
            {
                if (!(Weapon is null)) inventory.Add(new Weapon(Weapon));
                Weapon = new Weapon(inventory[position] as Weapon);
                inventory.RemoveAt(position);
            }
        }

        public void Dead(Point town)
        {
            Health = MaxHealth;
            Position.X = town.X;
            Position.Y = town.Y;
            _experience -= (int)(ToNextLevel * 0.2);
            _experience = _experience < 0 ? 0 : _experience;
        }

        public void GainExp(int exp)
        {
            _experience += exp;
            while (_experience >= ToNextLevel)
            {
                _experience = _experience - ToNextLevel;
                ToNextLevel = (int)(ToNextLevel * 1.5);
                Level++;
            }
        }
    }
}
