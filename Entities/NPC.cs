using AxeOfExile.Entities.Creatures;
using gra;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile.Entities
{
    class NPC
    {
        private enum NPCScene
        {
            Exit = 0,
            SellItem,
            BuyItem,
            Start
        };

        private NPCScene ActualScene { get; set; }

        private bool running;

        private Player player;

        private List<Item> items;

        public NPC(Player player, List<Item> items)
        {
            ActualScene = NPCScene.Start;
            running = true;
            this.player = player;
            this.items = items;
        }

        public void Init()
        {
            while (running)
            {
                switch (ActualScene)
                {
                    case NPCScene.SellItem:
                        SellItems();
                        break;
                    case NPCScene.BuyItem:
                        BuyItems();
                        break;
                    case NPCScene.Exit:
                        running = false;
                        break;
                    case NPCScene.Start:
                    default:
                        DisplayOptions();
                        break;
                }
            }
        }

        private void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine("Handlarz przedmiotami");
            Console.WriteLine("[1] Sprzedaj przedmioty");
            Console.WriteLine("[2] Kup przedmioty");
            Console.WriteLine("[0] Wyjdz z miasta");

            ActualScene = (NPCScene)DataInput.GetInt("Wybor: ");
        }

        private void SellItems()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[numer] | nazwa | cena sprzedazy");
                for(int i = 0; i < player.inventory.Count; i++)
                {
                    Console.WriteLine($"[{i}] | {player.inventory[i].Name} | {player.inventory[i].SellPrice}");
                }
                Console.WriteLine("[-1] Wyjscie");

                int position = DataInput.GetInt("Wybierz przedmiot do sprzedania: ");

                if (position == -1)
                {
                    ActualScene = NPCScene.Start;
                    return;
                }
                
                if (position < 0 || position >= player.inventory.Count) continue;

                player.Money += player.inventory[position].SellPrice;
                player.inventory.RemoveAt(position);
            }
        }

        private void BuyItems() 
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"Monety: {player.Money}");
                Console.WriteLine("Pozycja | Nazwa | Parametry | Cena | Szczegoly");
                for(int i = 1; i <= items.Count; i++)
                {
                    Console.WriteLine($"[{i}] | {items[i-1].BuyInfo()} | {i * 1000}");
                }
                Console.WriteLine("[-1] Wyjscie");

                int position = DataInput.GetInt("Wybierz pozycje, ktora chcesz kupic: ");

                if (position == -1)
                {
                    ActualScene = NPCScene.Start;
                    return;
                }

                int itemInfo = (position / 1000) - 1;
                if(position > items.Count && itemInfo >= 0 && itemInfo < items.Count)
                {
                    Console.WriteLine(items[itemInfo]);
                    Console.WriteLine($"[{itemInfo + 1}] Kup przedmiot");
                    Console.WriteLine($"[-1] Wyjdz");
                    position = DataInput.GetInt("Wybierz opcje: ");
                }

                if (position >= 1 && position < items.Count+1)
                {
                    position--;
                    if (player.Money < items[position].BuyPrice) continue;

                    player.Money -= items[position].BuyPrice;
                    player.inventory.Add(GetItem(items[position]));
                    Console.WriteLine($"Kupiles {items[position].Name} za {items[position].BuyPrice}");
                    Console.WriteLine("Wcisnij klawisz by kontynuowac...");
                    Console.ReadKey();
                }
            }
        }

        private Item GetItem(Item item)
        {
            if (item is Armor)
                return new Armor(item as Armor);
            if (item is Boot)
                return new Boot(item as Boot);
            if (item is Helmet)
                return new Helmet(item as Helmet);
            if (item is Legs)
                return new Legs(item as Legs);
            if (item is Shield)
                return new Shield(item as Shield);
            return new Weapon(item as Weapon);
        }

        
    }
}
