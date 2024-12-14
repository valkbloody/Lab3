using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Player
    {
        int index;
        int pos;
        int state;
        public int Pos
        {
            get { return pos; }
            set
            {
                if (value == 0) pos = 1;
                pos += value;
            }
        }
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public int State
        {
            get { return state; }
            set { state = value; }
        }
        public Player(int index)
        {
            Index = index;
            Pos = 0;
            state = 0;
        }
       
        public void Move(int k)
        {
            if (k == 0) {
                System.Threading.Thread.Sleep(50);
                Console.WriteLine("Игрок " + Convert.ToString(Index) + " нажмите любую клавишу и Enter, чтобы бросить кубик.");
                string s = Console.ReadLine();
                Random rand = new Random();
                int val = 0;
                val = rand.Next(1, 7);

                Pos = val;
                System.Threading.Thread.Sleep(50);
                Console.WriteLine($"Выпало значение: {val}");
                System.Threading.Thread.Sleep(50);
                Console.WriteLine($"Игрок {Index} на {Pos} клетке");
            }
            else
            {
                Pos = k;
            }
        }
        public void Skip_Move(int state)
        {
            if (state == 0) Console.WriteLine($"Игрок {Index} попал на клетку пропуска хода.");
            if (state == 1) Console.WriteLine("Игрок " + Convert.ToString(Index) + " пропускает ход.");
            if (state >= 2)
            {
                state = 0;
                Move(0);
            }
        }
    }
}
