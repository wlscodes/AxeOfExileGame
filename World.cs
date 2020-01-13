using AxeOfExile.Cartography;
using AxeOfExile.Entities;
using AxeOfExile.Entities.Creatures;
using gra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AxeOfExile
{
    /// <summary>
    /// Represents scenes options
    /// </summary>
    enum Scene 
    { 
        GameMenu, DisplayMap, Fight, MainMenu, Equipment, City, Shopkeeper, PlayerInfo, Help 
    };

    /// <summary>
    /// This class represents game world
    /// </summary>
    public class World
    {
        /// <summary>
        /// Game map object
        /// </summary>
        private readonly Map _map;

        /// <summary>
        /// List of monsters
        /// </summary>
        private List<Monster> _monsters;

        /// <summary>
        /// List of items
        /// </summary>
        private List<Item> _items;

        /// <summary>
        /// Random object used for generate numbers
        /// </summary>
        private Random r;

        private bool running;

        /// <summary>
        /// Represents actual scene
        /// </summary>
        private Scene sceneNumber;

        public City Town { get; set; }

        private Point TownPosition;

        public Player User { get; set; }

        public World(int mapWidth, int mapHeight, string playerName)
        {
            r = new Random();

            // Create Map
            _map = new Map(mapWidth, mapHeight);

            // Create item objects
            GenerateItems();

            // Create monsters
            GenerateMonsters();

            // Fill 20 percent of map size with monsters
            FillMapWithMonsters((int)(mapWidth * mapHeight * 0.2));

            // Set city on the map and player in city
            InitCityAndPlayer(playerName);

            // Stat game
            running = true;

            // Set start scene
            sceneNumber = Scene.GameMenu;

            //Init();
        }

        public World() { }

        private void DisplayHealthBar()
        {
            Display.Bar("Helath      ", User.Health, User.MaxHealth, ConsoleColor.Green, ConsoleColor.Red);
        }

        private void DisplayExpBar()
        {
            Display.Bar("Experience  ", User.Experience, User.ToNextLevel, ConsoleColor.Gray, ConsoleColor.DarkGray, $"Poziom: {User.Level}");
        }


        public void ReturnToGame()
        {
            sceneNumber = Scene.GameMenu;
            running = true;
            Init();
        }

        /// <summary>
        /// Initialize game world (map, monsters, weapons, player)
        /// </summary>
        private void Init()
        {
            while(running)
            {
                User.Regen();
                switch (sceneNumber)
                {
                    case Scene.Help:
                        DisplayHelp();
                        break;
                    case Scene.DisplayMap:
                        DisplayFullMap();
                        break;
                    case Scene.Fight:
                        FightScene();
                        break;
                    case Scene.City:
                        EnterCity();
                        break;
                    case Scene.Equipment:
                        EquipmentMenu();
                        break;
                    case Scene.MainMenu:
                        return;
                    case Scene.GameMenu:
                    default:
                        GameMenu();
                        break;
                }

                Console.Write("Akcja: ");
                HandleAction(Console.ReadLine());
            }
        }

        private void MovePlayer(Point point)
        {
            Point checkPoint = point + User.Position;
            if (checkPoint.X < 0 || checkPoint.Y < 0 || checkPoint.X > _map.XAxis - 1 || checkPoint.Y > _map.YAxis - 1) return;
            User.Position = checkPoint;
        }

        private void HandleAction(string action)
        {
            switch(action)
            {
                case "w":
                    MovePlayer(new Point(-1, 0));
                    break;
                case "s":
                    MovePlayer(new Point(1, 0));
                    break;
                case "a":
                    MovePlayer(new Point(0, -1));
                    break;
                case "d":
                    MovePlayer(new Point(0, 1));
                    break;
                case "help":
                    sceneNumber = Scene.Help;
                    break;
                case "map":
                    sceneNumber = Scene.DisplayMap;
                    break;
                case "fight":
                    sceneNumber = Scene.Fight;
                    break;
                case "enter":
                    sceneNumber = Scene.City;
                    break;
                case "equipment":
                    sceneNumber = Scene.Equipment;
                    break;
                case "menu":
                    sceneNumber = Scene.MainMenu;
                    break;
                case "back":
                default:
                    sceneNumber = Scene.GameMenu;
                    break;
            }
        }

        /// <summary>
        /// Manage player equipment
        /// </summary>
        private void EquipmentMenu()
        {
            User.DisplayEquipment();
        }

        /// <summary>
        /// Scene show all comands
        /// </summary>
        private void DisplayHelp()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Komendy:");
                Console.WriteLine("w - Przesuniecie gracza o jedna pozycje na polnoc");
                Console.WriteLine("s - Przesuniecie gracza o jedna pozycje na poludnie");
                Console.WriteLine("a - Przesuniecie gracza o jedna pozycje na zachod");
                Console.WriteLine("d - Przesuniecie gracza o jedna pozycje na wschod");
                Console.WriteLine("enter - Wejdz do miasta");
                Console.WriteLine("equipment - Zarzadzaj ekwipunkiem");
                Console.WriteLine("fight - Walka z przeciwnikiem");
                Console.WriteLine("help - Wyswietlenie pomocy");
                Console.WriteLine("map - Wyswietlenie wiekszej wersji mapy");
                Console.WriteLine("menu - Powrot do glownego menu");
                Console.WriteLine("stats - Wyswietlenie statystyk gracza");
                Console.WriteLine("[0] - Powrot");
                int result = DataInput.GetInt("Wpisz 0, aby wyjsc");
                if (result == 0) return;
            }
        }

        /// <summary>
        /// Display full map on the screen
        /// </summary>
        private void DisplayFullMap()
        {
            while (true)
            {
                Console.Clear();
                _map.DisplayFullMap();
                Console.WriteLine("[0] - Powrot");
                int result = DataInput.GetInt("Wpisz 0, aby wyjsc");
                if (result == 0) return;
            }
        }

        /// <summary>
        /// Game menu
        /// </summary>
        private void GameMenu()
        {
            Console.Clear();
            DisplayHealthBar();
            DisplayExpBar();
            Console.WriteLine($"Sila: {User.Strength}");
            _map.DisplayMiniMap(User.Position, 3, User.GetMapSign());
        }

        /// <summary>
        /// Fight scene
        /// </summary>
        private void FightScene()
        {
            if (_map.CompareObject(new Monster(), User.Position.X, User.Position.Y))
            {
                Monster monster = _map.GetMonster(User.Position.X, User.Position.Y);

                if (monster is null) return;

                Console.WriteLine("Chcesz walczyc z: ");
                Console.WriteLine(monster);
                Console.WriteLine("[0] TAK");
                Console.WriteLine("[1] NIE");
                int wybor = DataInput.GetInt("Wybierz: ");

                if (wybor == 0)
                {
                    Fight f = new Fight(User, monster);
                    f.Begin();

                    if (monster.Health <= 0) _map.RemoveMonster(User.Position.X, User.Position.Y);
                    if (User.Health <= 0) User.Dead(TownPosition);
                }
            }
            else
                Console.WriteLine("Brak przeciwnika");
            Console.WriteLine("Wcisnij przycisk aby kontynuowac...");  
            Console.ReadLine();
            sceneNumber = Scene.GameMenu;
        }

        /// <summary>
        /// Fight scene
        /// </summary>
        private void EnterCity()
        {
            if (_map.CompareObject(new City(), User.Position.X, User.Position.Y))
            {
                NPC npc = new NPC(User, _items.FindAll(x => x.BuyPrice > 0).ToList());
                npc.Init();
            }
            else
                Console.WriteLine("Nie mozna wykonac");
            Console.WriteLine("Wcisnij przycisk aby kontynuowac...");
            Console.ReadLine();
            sceneNumber = Scene.GameMenu;
        }

        /// <summary>
        /// Function create Monster objects
        /// </summary>
        private void GenerateMonsters()
        {
            _monsters = new List<Monster>
            {
                // Forest
                new Monster("Wolf", 5, 25, 1, 3, '^', new Armor(_items.Find(x => x.Name.Equals("Skorzana tunika")) as Armor), 40),
                new Monster("Infected Deer", 9, 47, 2, 7, '^'),
                new Monster("Grizzly", 15, 90, 5, 10, '^', new Boot(_items.Find(x => x.Name.Equals("Futrzane kamasze")) as Boot), 10),
                // Mountains
                new Monster("Dwarf Soldier", 30, 150, 30, 40, 'M', new Armor(_items.Find(x => x.Name.Equals("Kolczuga")) as Armor), 30),
                new Monster("Dwarf Minner", 20, 100, 15, 30, 'M', new Helmet(_items.Find(x => x.Name.Equals("Helm Wikinga")) as Helmet), 35),
                new Monster("Dwarf Leader", 60, 220, 35, 65, 'M', new Weapon(_items.Find(x => x.Name.Equals("Krasnoludzki topor")) as Weapon), 60),
                // Water
                new Monster("Shark", 18, 25, 3, 5, 'O', new Legs(_items.Find(x => x.Name.Equals("Spodnie z krokodylich lusek")) as Legs), 45),
                new Monster("Undead Diver", 70, 250, 40, 45, 'O', new Shield(_items.Find(x => x.Name.Equals("Miedziana tarcza")) as Shield), 33),
                // Steps
                new Monster("Blind Orc", 20, 75, 27, 31, '#', new Weapon(_items.Find(x => x.Name.Equals("Scyzoryk")) as Weapon), 70),
                new Monster("Troll", 35, 100, 17, 55, '#', new Shield(_items.Find(x => x.Name.Equals("Drewniana tarcza")) as Shield), 85)
            };
        }

        private void GenerateItems()
        {
            _items = new List<Item>()
            {
                // Zbroje
                new Armor("Skorzana tunika", "Rycerska tunika wykonana ze skory orkow", false, 10, 3, 5),
                new Armor("Kolczuga", "Kolczuga stworzona przez krasnoludzkich kowali", false, 30, 10, 14),
                new Armor("Zbroja plytowa", "Zbroja stworzona przez samych Bogow", false, 100, 30, 25),

                // Buty
                new Boot("Zelazne buty", "Niezbyt wygodne, ale za to obronia przed ukaszeniem", false, 10, 3, 4),
                new Boot("Drewniane chodaki", "Drwal dowcipnis wystrugal te okropienstwa", false, 3, 1, 1),
                new Boot("Futrzane kamasze", "Skora i futro groznego niedzwiedzia posluzyly do stworzenia tych butow", false, 20, 6, 8),
                new Boot("Zlote buty", "Buty z magicznego, niezniszczalnego zlota", false, 100, 30, 25),

                // Helmy
                new Helmet("Podarta mycka", "Czapka, ktora ledwo sie trzyma", false, 3, 1, 1),
                new Helmet("Helm Wikinga", "Starodawny helm nordyckich ludzi", false, 15, 5, 9),
                new Helmet("Helm 7 Krolow", "Helm, ktorego nosilo 7 poteznych krolow", false, 100, 30, 25),

                // Nogawice
                new Legs("Satynowe portki", "Spodnie pochodzace ze wschodnich szlakow handlowych", false, 7, 3, 3),
                new Legs("Spodnie z krokodylich lusek", "Dobre nogawice dla poczatkujacego podroznika", false, 21, 7, 12),
                new Legs("Magiczne nogawice", "Spodnie pojawily sie w magiczny sposob", false, 100, 30, 25),

                // Tarcze
                new Shield("Drewniana tarcza", "Tarcza zbita z desek starego kurnika", false, 5, 1, 4),
                new Shield("Miedziana tarcza", "Tarcza z miedzi", false, 12, 4, 9),
                new Shield("Obsydianowa tarcza", "Tarcza ktora posiadal legendarny krasnoludzki wojownik", false, 100, 30, 25),

                // Bron
                new Weapon("Scyzoryk", "Leciwy scyzoryk znaleziony na dnie szuflady", false, 6, 3, 5, 8),
                new Weapon("Krasnoludzki topor", "Wielki topor od malych krasnoludow", false, 20, 6, 12, 16),
                new Weapon("Wielki magiczny miecz", "Zaklety miecz. Tylko godny wojownik bedzie mogl go uzywac", false, 100, 30, 25, 35)
            };
        }

        /// <summary>
        /// Function generate monsters. Monster type depends on background tile type.
        /// </summary>
        /// <param name="monsterLimit">Number of monsters that be generated</param>
        private void FillMapWithMonsters(int monsterLimit)
        {
            do
            {
                int x = r.Next(0, _map.XAxis);
                int y = r.Next(0, _map.YAxis);

                var monsters = _monsters.FindAll(s => s.SignType.Equals(_map.GetTileSign(x, y)));
                Monster m = monsters.ElementAt(r.Next(0, monsters.Count));

                _map.SetMapObject(x, y, new Monster(m, new Point(x, y)));

            } while (--monsterLimit > 0);
        }
        
        /// <summary>
        /// Set player and city on the map
        /// </summary>
        /// <param name="playerName">Pplayer name</param>
        private void InitCityAndPlayer(string playerName)
        {
            int x = r.Next(0, _map.XAxis);
            int y = r.Next(0, _map.YAxis);

            this.User = new Player(playerName, new Point(5, 10), 10, '!', ConsoleColor.White);
            this.Town = new City() { player = this.User};
            _map.SetMapObject(x, y, this.Town);
            TownPosition = new Point(x, y);
        }

    }
}
