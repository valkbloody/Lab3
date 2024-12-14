using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp3.Program;

namespace ConsoleApp3
{
    public class Map
    {
        private List<Cell> map;
        private List<Player> players;
        public List<Cell> Get_Map
        {
            get { return map; }
            set { map = value; }
        }
        public List<Player> GetPlayers
        {
            get { return players; }
            set { players = value; }
        }
        public Map(List<Cell> map)
        {
            Get_Map = map;
        }
        public Map() // генерация новой карты
        {
            Random rand = new Random();
            List<Cell> map1 = new List<Cell> { };
            for (int i = 1; i < win + 7; i++)
            {
                Cell cell = new Cell();
                map1.Add(cell);
            }
            for (int i = 1; i < rand.Next(7, 15); i++)
            {
                map1[rand.Next(2, win - 1)].State = '1';
            }
            for (int i = 1; i < rand.Next(5, 12); i++)
            {
                map1[rand.Next(5, win - 1)].State = '2';
            }
            for (int i = 1; i < rand.Next(4, 7); i++)
            {

                if (rand.Next(0, 2) == 0)
                {
                    int pos3 = rand.Next(5, win - 12);
                    map1[pos3].Indx = pos3;
                    int pos4 = 0;
                    map1[pos3].State = '3';
                    pos4 = rand.Next(pos3 + 4, pos3 + 11);
                    map1[pos4].State = '4';
                    Snake_Cell snakecell = new Snake_Cell(map1[pos3], map1[pos4]);
                    map1.RemoveAt(pos3);
                    map1.Insert(pos3, snakecell);
                    map1.RemoveAt(pos4);
                    map1.Insert(pos4, snakecell.End);
                    map1[pos4].Indx = pos4;
                }
                else
                {
                    int pos3 = rand.Next(12, win - 2);
                    map1[pos3].Indx = pos3;
                    int pos4 = 0;
                    map1[pos3].State = '5';
                    pos4 = rand.Next(4, pos3 - 4);
                    map1[pos4].State = '6';
                    Bad_Snake_Cell snakecell = new Bad_Snake_Cell(map1[pos3], map1[pos4]);
                    map1.RemoveAt(pos3);
                    map1.Insert(pos3, snakecell);
                    map1.RemoveAt(pos4);
                    map1.Insert(pos4, snakecell.End);
                    map1[pos4].Indx = pos4;
                }
            }
            Get_Map = map1;
        }
        public Map(int k) // загрузка старой карты из файла note2 и note3 - ходы игроков
        {
            StreamReader fstream = null;
            fstream = new StreamReader("note2.txt", Encoding.UTF8);
            fstream.BaseStream.Position = 0;
            var str = fstream.ReadLine();
            List<Cell> cells = new List<Cell> { };
            for (int i = 0; i < str.Split(' ').Count(); i++)
            {
                if (i % 2 == 0)
                {
                    Cell cell = new Cell();
                    cells.Add(cell);
                }
                else
                {
                    cells[cells.Count - 1].State = Convert.ToChar(str.Split(' ')[i]);
                }
            }
            while (true)
            {
                var str1 = fstream.ReadLine();
                if (str1 == "" || str1 == " " || str == null) break;
                if (Convert.ToInt32(str1.Split(' ')[0]) < Convert.ToInt32(str1.Split(' ')[1]))
                {
                    cells[Convert.ToInt32(str1.Split(' ')[0])].Indx = Convert.ToInt32(str1.Split(' ')[0]);
                    cells[Convert.ToInt32(str1.Split(' ')[1])].Indx = Convert.ToInt32(str1.Split(' ')[1]);
                    Snake_Cell snake = new Snake_Cell(cells[Convert.ToInt32(str1.Split(' ')[0])], cells[Convert.ToInt32(str1.Split(' ')[1])]);
                    cells.RemoveAt(Convert.ToInt32(str1.Split(' ')[0]));
                    cells.Insert(Convert.ToInt32(str1.Split(' ')[0]), snake);
                }
                else
                {
                    cells[Convert.ToInt32(str1.Split(' ')[0])].Indx = Convert.ToInt32(str1.Split(' ')[0]);
                    cells[Convert.ToInt32(str1.Split(' ')[1])].Indx = Convert.ToInt32(str1.Split(' ')[1]);
                    Bad_Snake_Cell snake = new Bad_Snake_Cell(cells[Convert.ToInt32(str1.Split(' ')[0])], cells[Convert.ToInt32(str1.Split(' ')[1])]);
                    cells.RemoveAt(Convert.ToInt32(str1.Split(' ')[0]));
                    cells.Insert(Convert.ToInt32(str1.Split(' ')[0]), snake);
                }

            }
            Get_Map = cells;
            fstream.Close();
            StreamReader fstream1 = null;
            fstream1 = new StreamReader("note3.txt", Encoding.UTF8);
            fstream1.BaseStream.Position = 0;
            List<Player> players = new List<Player> { };
            while (true)
            {
                var str1 = fstream1.ReadLine();
                if (str1 == "" || str1 == " " || str1 == null) break;
                string[] splitstr = str1.Split(' ');
                Player player = new Player(Convert.ToInt32(splitstr[0]));
                player.Pos = Convert.ToInt32(splitstr[1]);
                int key = -1;
                int po = 0;
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].Index == player.Index) 
                    {
                        key = i;                    

                        break;
                    }
                }
                if (key == -1)
                {
                    players.Add(player);
                }
                else
                {
                    players[key].Pos = -players[key].Pos +  player.Pos-1;
                    move_log = move_log + player.Index.ToString() + " " + player.Pos.ToString() + "\n";
                }
            }
            if (players.Count == 0) Console.WriteLine("Начните новую игру!");
            GetPlayers = players;
        }
        public void Action(Player player) // при попадании на специальную клетку 
        {
            if (map[player.Pos].State == '1') // доп ход
            {
                Console.WriteLine($"Игрок {player.Index} попал на клетку дополнительного хода.");
                player.Move(0);
                move_log = move_log + player.Index.ToString() + " " + player.Pos.ToString() + "\n";
            }
            if (map[player.Pos].State == '2') // пропуск
            {
                player.Skip_Move(player.State++);
            }
            if (map[player.Pos].State == '3') // вперед на Н клеток
            {
                Console.WriteLine($"Игрок {player.Index} попал на клетку хорошего питона!");
                map[player.Pos].Snake_Move(player);
                move_log = move_log + player.Index.ToString()+" " + player.Pos.ToString() + "\n";
            }
            if (map[player.Pos].State == '5') // назад на Н клеток
            {
                Console.WriteLine($"Игрок {player.Index} попал на клетку плохого питона!");
                map[player.Pos].Rev_Snake_Move(player);
                move_log = move_log + player.Index.ToString() + " " + player.Pos.ToString() + "\n";

            }
            if (player.Pos >= win) // выход при победе одного из игроков
            {
                Console.WriteLine($"Игрок {player.Index} победил!");
                Environment.Exit(0);
            }
        }
        public void Just_move(Player player) // обычный ход
        {
            if (map[player.Pos].State != '2')
            {
                System.Threading.Thread.Sleep(50);
                Console.WriteLine("Игрок " + Convert.ToString(player.Index) + ", чтобы открыть карту нажмите 4 и Enter");
                string s = Console.ReadLine();
                player.Move(0);
                move_log = move_log + player.Index.ToString() + " " + player.Pos.ToString() + "\n";
                Console.WriteLine();
                if (s == "4")
                {
                    System.Threading.Thread.Sleep(50);
                    Show_map();
                }

                Console.WriteLine();


            }
        }
        public void Show_map() // показать поле с прорисовкой игроков
        {
            Console.WriteLine("+++ - поле с дополнительным ходом");
            Console.WriteLine("--- - поле с пропуском хода");
            Console.WriteLine("->N - поле с переходом вперед на N клетку");
            Console.WriteLine("N<- - поле с переходом назад на N клетку");
            Console.WriteLine();
            for (int k_str = 0; k_str < win / 10; k_str++)
            {
                int nach = 1;
                int kon = 1;
                if (k_str % 2 == 0)
                {
                    foreach (Player play in players)
                    {
                        if (play.Pos >= k_str * 10 + k_str && play.Pos < (k_str + 1) * 10 + 1)
                        {
                            for (int i = 0; i < (play.Pos - 1) * 11; i++) Console.Write(" ");
                            Console.WriteLine($"{play.Index} игрок");
                        }
                    }
                }
                else
                {
                    foreach (Player play in players)
                    {
                        if (play.Pos >= k_str * 10 + k_str && play.Pos <= (k_str + 1) * 10 + 1)
                        {
                            for (int i = 0; i < ((k_str + 1) * 10 - play.Pos + 1) * 10; i++) Console.Write(" ");
                            Console.WriteLine($"{play.Index} игрок");
                        }
                    }
                }
                if (k_str % 2 == 0)
                {
                    if (k_str == 0) nach = 1;
                    else nach = k_str * 10 + k_str;
                    kon = (k_str + 1) * 10 + 1;

                    for (int i = nach; i < kon; i++) Console.Write("__________ ");
                    Console.WriteLine();
                    for (int i = nach; i < kon; i++) Console.Write("|        | ");
                    Console.WriteLine();
                    for (int i = nach; i < kon; i++)
                    {

                        if (map[i].State == '0' || map[i].State == '4' || map[i].State == '6')
                        {
                            Console.Write("|        | ");

                        }
                        if (map[i].State == '1')
                        {
                            Console.Write("|  +++   | ");
                        }
                        if (map[i].State == '2')
                        {
                            Console.Write("| ---    | ");
                        }
                        if (map[i].State == '3')
                        {
                            Console.Write($"| ->{map[i].Get_End()}");
                            for (int pr1 = 0; pr1 < 5 - map[i].Get_End().ToString().Length; pr1++) Console.Write(" ");
                            Console.Write($"| ");
                        }
                        if (map[i].State == '5')
                        {
                            Console.Write($"| {map[i].Get_End()}<-");
                            for (int pr = 0; pr < 5 - map[i].Get_End().ToString().Length; pr++) Console.Write(" ");
                            Console.Write($"| ");
                        }
                    }
                    Console.WriteLine();
                    for (int i = nach; i < kon; i++)
                    {
                        Console.Write($"|   {i}");
                        for (int pr = 0; pr < 5 - i.ToString().Length; pr++) Console.Write(" ");
                        Console.Write($"| ");
                    }
                    Console.WriteLine();
                    for (int i = nach; i < kon; i++) Console.Write("__________ ");
                    Console.WriteLine();
                }
                else
                {
                    kon = k_str * 10;
                    nach = (k_str + 1) * 10 + 1;
                    for (int i = nach; i > kon; i--) Console.Write("_________ "); ;
                    Console.WriteLine();
                    for (int i = nach; i > kon; i--) Console.Write("|       | ");
                    Console.WriteLine();
                    for (int i = nach; i > kon; i--)
                    {

                        if (map[i].State == '0' || map[i].State == '4' || map[i].State == '6')
                        {
                            Console.Write("|       | ");

                        }
                        if (map[i].State == '1')
                        {
                            Console.Write("|  +++  | ");
                        }
                        if (map[i].State == '2')
                        {
                            Console.Write("| ---   | ");
                        }
                        if (map[i].State == '3')
                        {
                            Console.Write($"| ->{map[i].Get_End()}");
                            for (int pr1 = 0; pr1 < 4 - map[i].Get_End().ToString().Length; pr1++) Console.Write(" ");
                            Console.Write($"| ");
                        }
                        if (map[i].State == '5')
                        {
                            Console.Write($"| {map[i].Get_End()}<-");
                            for (int pr = 0; pr < 4 - map[i].Get_End().ToString().Length; pr++) Console.Write(" ");
                            Console.Write($"| ");
                        }
                    }
                    Console.WriteLine();
                    for (int i = nach; i > kon; i--)
                    {
                        Console.Write($"|   {i}");
                        for (int pr = 0; pr < 4 - i.ToString().Length; pr++) Console.Write(" ");
                        Console.Write($"| ");
                    }
                    Console.WriteLine();
                    for (int i = nach; i > kon; i--) Console.Write("_________ ");
                    Console.WriteLine();
                }
            }
        }
        public string ToString() // передача в файл
        {
            string res = "";
            foreach(Cell cell in Get_Map)
            {
                res = res + cell.Indx.ToString()+" "+ cell.State + " ";
            }
            res =res+"\n";
            foreach (Cell cell in Get_Map)
            {
                if (cell.State == '3' || cell.State == '5')   res = res + cell.Indx + " " + cell.Get_End()+"\n";
            }
            return res;
        }
    }
}
