using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp3
{
    
    public class Program
    {
        public delegate void SignalHandler(ConsoleSignal consoleSignal);
        public static class ConsoleHelper
        {
            [DllImport("Kernel32", EntryPoint = "SetConsoleCtrlHandler")]
            public static extern bool SetSignalHandler(SignalHandler handler, bool add);
        }
        private static SignalHandler signalHandler;
        public enum ConsoleSignal
        {
            CtrlC = 0,
            CtrlBreak = 1,
            Close = 2,
            LogOff = 5,
            Shutdown = 6
        }
        private static void HandleConsoleSignal(ConsoleSignal consoleSignal) // сохранение данных о ходах при закрытии консоли
        {
            if (consoleSignal == ConsoleSignal.Close)
            {
                using (StreamWriter writer = new StreamWriter("note3.txt", false, Encoding.UTF8))
                {
                    writer.WriteLine(move_log);
                }
            }
        }
        public static string move_log = "";
        public static int win = 30;
        static void Main(string[] args)
        {
            signalHandler += HandleConsoleSignal;
            ConsoleHelper.SetSignalHandler(signalHandler, true);
            Map field = new Map();
            int amount_players = 0;
            List<Player> players = new List<Player> { };
            while (true)
            {
                Console.WriteLine("Если вы хотите продолжить предыдщую игру нажите 1 и Enter");
                Console.WriteLine("Если вы хотите начать новую игру нажите 2 и Enter");
                string choice = Console.ReadLine();
                if (choice == "2")// новая игра
                {
                    string s = "";
                    int key = 0;
                    do
                    {
                        Console.WriteLine("Выберите количество игроков(2, 3 или 4)");
                        s = Console.ReadLine();
                        switch (s)
                        {

                            case "2":
                                amount_players = 2;
                                key = 1;
                                break;
                            case "3":
                                amount_players = 3;
                                key = 1;
                                break;
                            case "4":
                                amount_players = 4;
                                key = 1;
                                break;
                            default:
                                Console.WriteLine("Неверный выбор");
                                break;

                        }
                    } while (key != 1);

                    for (int i = 0; i < amount_players; i++)
                    {
                        Player player = new Player(i + 1);
                        players.Add(player);
                    }
                    field.GetPlayers = players;
                    using (StreamWriter writer = new StreamWriter("note2.txt", false, Encoding.UTF8))
                    {
                        writer.WriteLine(field.ToString());
                    }
                    field.Show_map();
                    break;
                }
                else if (choice == "1")
                {
                    field = new Map(1); // загрузка и файла карты
                    amount_players = field.GetPlayers.Count;
                    players = field.GetPlayers;
                    if (players.Count > 0) break;// есть предыдущая игра

                }
            }
                while (true)
                {
                    for (int i = 0; i < amount_players; i++)
                    {
                        field.Just_move(players[i]);
                        field.Action(players[i]);
                        Console.WriteLine();
                    }
                }
            }
        
    }
}
        