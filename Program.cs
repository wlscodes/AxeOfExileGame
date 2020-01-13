using gra;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;

namespace AxeOfExile
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            StartMenu();
        }

        private static World world;
        private static bool quit = true;
        
        private static void StartMenu()
        {
            bool quit = true;

            while (quit)
            {
                Console.Clear();
                Console.WriteLine(@"                      ____   __ ______      _ _      ");
                Console.WriteLine(@"     /\              / __ \ / _|  ____|    (_) |     ");
                Console.WriteLine(@"    /  \   __  _____| |  | | |_| |__  __  ___| | ___ ");
                Console.WriteLine(@"   / /\ \  \ \/ / _ \ |  | |  _|  __| \ \/ / | |/ _ \");
                Console.WriteLine(@"  / ____ \  >  <  __/ |__| | | | |____ >  <| | |  __/");
                Console.WriteLine(@" /_/    \_\/_/\_\___|\____/|_| |______/_/\_\_|_|\___|");
                Console.WriteLine($"    Autorzy: W&W - Walas Dariusz & Walczak Kamil     ");
                Console.WriteLine("\n\n\n");
                Console.WriteLine("--==   MENU   ==--");
                Console.WriteLine("[1] Stworz nowa gre");
                Console.WriteLine("[2] Zaladuj poprzednia rozgrywke");
                Console.WriteLine("[3] Zapisz gre");
                Console.WriteLine("[4] Wroc do gry");
                Console.WriteLine("[0] Wyjdz z gry");

                int choice = DataInput.GetInt("Wybor: ");

                switch (choice)
                {
                    case 1:
                        {
                            CreateNewGame();
                            break;
                        }
                    case 2:
                        {
                            LoadGame();
                            break;
                        }
                    case 3:
                        {
                            SaveGame();
                            break;
                        }
                    case 4:
                        {
                            ReturnToGame();
                            break;
                        }
                    case 0:
                        {
                            return;
                        }
                }
                StartMenu();
            }
        }

        private static void ReturnToGame()
        {
            if (world is null) return;
            world.ReturnToGame();
        }

        private static void CreateNewGame()
        {
            Console.WriteLine("Podaj nazwe postaci: ");
            bool nameCorrect = false;
            Regex regex = new Regex(@"^[a-zA-Z]{3,20}$");
            string name;
            do
            {
                name = Console.ReadLine();
                nameCorrect = regex.IsMatch(name);
                if (!nameCorrect)
                    Console.WriteLine("Nazwa moze zawierac tylko litery, oraz miec wiecej niz 3 i mniej niz 20 znakow");
            } while (!nameCorrect);

            world = new World(64, 20, name);
            world.ReturnToGame();
        }

        private static void LoadGame()
        {
            if(File.Exists("save.xml"))
            {
                try
                {
                    world = new World(64, 20, ""); 
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(World));
                    using (StreamReader sr = new StreamReader("save.xml"))
                    {
                        World w = xmlSerializer.Deserialize(sr) as World;
                        if (w is null) throw new ArgumentException("Zapis jest pusty!");
                        world.User = w.User;
                    }
                    ReturnToGame();
                }
                catch(ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Wystapil blad podczas odczytu!");
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Nie istnieje zaden zapis gry!");
            }
            Console.WriteLine("Wcisnij przycisk aby kontynuowac...");
            Console.ReadKey();
        }

        private static void SaveGame()
        {
            if (world is null) return;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(World));

            try
            {
                using (StreamWriter sw = new StreamWriter("save.xml"))
                {
                    xmlSerializer.Serialize(sw, world);
                }
                Console.WriteLine("Gra zapisana");
            }
            catch(Exception e)
            {
                Console.WriteLine("Wystapil blad podczas zappisu!");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Wcisnij przycisk aby kontynuowac...");
            Console.ReadKey();
        }
    }
}
