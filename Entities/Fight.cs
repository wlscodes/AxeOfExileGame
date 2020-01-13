using AxeOfExile.Entities.Creatures;
using gra;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxeOfExile.Entities
{
    public class Fight
    {
        Player player { get; set; }

        Monster monster { get; set; }

        public Fight(Player player, Monster monster)
        {
            this.player = player;
            this.monster = monster;
        }

        public void Begin()
        {
            if (player is null || monster is null) return;
            if (player.Health <= 0 || monster.Health <= 0) return;

            int round = 0;
            double damage = 0;
            Random r = new Random();
            // rand * str * 0,7 + sqrt(wapon * str * 0,3)
            do
            {
                damage = (r.Next(85, 115) / 100.0) * player.Strength * 0.7 + Math.Sqrt(player.WeaponDamage * player.Strength * 0.3);
                damage = (damage < 0.0) ? 0.0f : damage;

                monster.Health -= damage;

                Console.Write("Gracz ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(player.Name);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" zadal ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(damage);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" obrazen. Zdrowie ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(monster.Name);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" to ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(monster.Health);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();


                //Console.WriteLine($"Gracz {player.Name} zadal {damage} obrazen. Zdrowie {monster.Name} to {monster.Health}");

                if (monster.Health <= 0) 
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(monster.Name);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" zginal\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    int exp = (int)monster.Strength / 2;
                    player.GainExp(exp);
                    Console.Write("Zdobyles ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(exp);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" doswiadczenia");

                    player.Strength += monster.Strength * 0.2;

                    int coin = monster.CoinDrop;
                    Console.Write("Zdobyles ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(coin);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" zlota\n");
                    player.Money += coin;

                    if(monster.Droped)
                    {
                        Item item = monster.dropItem;

                        Console.WriteLine("Zdobyles ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(item.Name);
                        Console.ForegroundColor = ConsoleColor.White;

                        if (item is Armor) player.inventory.Add(new Armor(item as Armor));
                        if (item is Boot) player.inventory.Add(new Boot(item as Boot));
                        if (item is Helmet) player.inventory.Add(new Helmet(item as Helmet));
                        if (item is Legs) player.inventory.Add(new Legs(item as Legs));
                        if (item is Shield) player.inventory.Add(new Shield(item as Shield));
                        if (item is Weapon) player.inventory.Add(new Weapon(item as Weapon));

                        monster = null;
                    }

                    Console.WriteLine();

                    break;
                }

                double monsterDamage = monster.Strength * (r.Next(85, 115) / 100.0);
                damage = monsterDamage - player.Defense * player.Strength / 10;
                damage = (damage < 0) ? 0 : damage;
                player.Health -= damage;

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(monster.Name);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" zadal ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(damage);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" obrazen. Twoje zycie: ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(player.Health);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine();

                if(player.Health <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("YOU ARE DEAD!");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }

            } while (round++ < 50);

            if(round > 50)
            {
                Console.WriteLine("Walka zakonczona remisem");
            }
        }
    }
}
