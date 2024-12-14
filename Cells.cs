using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Cell
    {
        char state;
        int indx;
        public char State
        {
            get { return state; }
            set
            {
                state = value;
            }
        }
        public int Indx
        {
            get { return indx; }
            set
            {
                indx = value;
            }
        }
        public virtual void Snake_Move(Player player) 
        {

        }
        public virtual void Rev_Snake_Move(Player player)
        {

        }
        public virtual int Get_End()
        {
            return 0;
        }
        public Cell()
        {
            State = '0';
        }
    }
    public class Snake_Cell : Cell // вперед на Н клеток
    {
        Cell begin;
        Cell end;
        public Cell Begin
        {
            get { return begin; }
            set { begin = value; }
        }
        public Cell End
        {
            get { return end; }
            set { end = value; }
        }

        public Snake_Cell(Cell beg, Cell end1)
        {
            State = '3';
            Indx = beg.Indx;
            Begin = beg;
            End = end1;
        }
        public override int Get_End()
        {
            return end.Indx;
        }
        public override void Snake_Move(Player player)
        {
            player.Pos = end.Indx - begin.Indx;
            System.Threading.Thread.Sleep(50);
            Console.WriteLine($"Хороший питон переместил игрока {player.Index} на {player.Pos} клетку!");

        }


    }
    public class Bad_Snake_Cell : Cell // назад на Н клеток
    {
        Cell begin;
        Cell end;
        public Cell Begin
        {
            get { return begin; }
            set { begin = value; }
        }
        public Cell End
        {
            get { return end; }
            set { end = value; }
        }
        public Bad_Snake_Cell(Cell beg, Cell end1)
        {
            State = '5';
            Indx = beg.Indx;
            Begin = beg;
            End = end1;
        }
        public override void Rev_Snake_Move(Player player)
        {
            player.Pos = end.Indx - begin.Indx;
            System.Threading.Thread.Sleep(50);
            Console.WriteLine($"Плохой питон переместил игрока {player.Index} на {player.Pos} клетку!");
        }
        public override int Get_End()
        {
            return end.Indx;
        }
    }

}
