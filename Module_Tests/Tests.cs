using ConsoleApp3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
namespace Module_Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Player_Created()
        {
            Player player = new Player(1);
            Assert.AreEqual(player.Index, 1);
            Assert.AreEqual(player.State, 0);
            Assert.AreEqual(player.Pos, 1);
        }
        [TestMethod]
        public void Player_Moves()
        {
            Player player = new Player(1);
            Random rand = new Random();
            int randomized = rand.Next(1, 6);
            player.Move(randomized);
            Assert.AreEqual(player.Pos, randomized + 1);
        }
        [TestMethod]
        public void Cell_State_Index()
        {
            Cell cell = new Cell();
            int index = 10;
            char state = '2';
            cell.Indx = index;
            cell.State = state;
            Assert.AreEqual(cell.Indx, index);
            Assert.AreEqual(cell.State, state);
        }
        [TestMethod]
        public void Sbake_Cell_Created()
        {
            Cell cell1 = new Cell();
            int index1 = 5;
            char state1 = '3';
            cell1.Indx = index1;
            cell1.State = state1;
            Cell cell2 = new Cell();
            int index2 = 10;
            char state2 = '4';
            cell2.Indx = index2;
            cell2.State = state2;
            Snake_Cell snake_cell = new Snake_Cell(cell1, cell2);
            Assert.AreEqual(cell1.Indx, snake_cell.Begin.Indx);
            Assert.AreEqual(cell1.State, snake_cell.Begin.State);
            Assert.AreEqual(cell2.Indx, snake_cell.End.Indx);
            Assert.AreEqual(cell2.State, snake_cell.End.State);
        }
        [TestMethod]
        public void Snake_Move()
        {
            Cell cell1 = new Cell();
            int index1 = 5;
            char state1 = '3';
            cell1.Indx = index1;
            cell1.State = state1;
            Cell cell2 = new Cell();
            int index2 = 9;
            char state2 = '4';
            cell2.Indx = index2;
            cell2.State = state2;
            Player player = new Player(1);
            player.Pos = 4;
            Snake_Cell snake_cell = new Snake_Cell(cell1, cell2);
            snake_cell.Snake_Move(player);
            Assert.AreEqual(cell2.Indx, player.Pos);
        }

        [TestMethod]
        public void Bad_Snake_Cell_Created()
        {
            Cell cell1 = new Cell();
            int index1 = 9;
            char state1 = '5';
            cell1.Indx = index1;
            cell1.State = state1;
            Cell cell2 = new Cell();
            int index2 = 2;
            char state2 = '6';
            cell2.Indx = index2;
            cell2.State = state2;
            Bad_Snake_Cell snake_cell = new Bad_Snake_Cell(cell1, cell2);
            Assert.AreEqual(cell1.Indx, snake_cell.Begin.Indx);
            Assert.AreEqual(cell1.State, snake_cell.Begin.State);
            Assert.AreEqual(cell2.Indx, snake_cell.End.Indx);
            Assert.AreEqual(cell2.State, snake_cell.End.State);
        }
        [TestMethod]
        public void Bad_Snake_Move()
        {
            Cell cell1 = new Cell();
            int index1 = 9;
            char state1 = '5';
            cell1.Indx = index1;
            cell1.State = state1;
            Cell cell2 = new Cell();
            int index2 = 2;
            char state2 = '6';
            cell2.Indx = index2;
            cell2.State = state2;
            Player player = new Player(1);
            player.Pos = 8;
            Snake_Cell snake_cell = new Snake_Cell(cell1, cell2);
            snake_cell.Snake_Move(player);
            Assert.AreEqual(cell2.Indx, player.Pos);
        }
        [TestMethod]
        public void Map_Created()
        {
            Cell cell1 = new Cell();
            int index1 = 5;
            char state1 = '3';
            cell1.Indx = index1;
            cell1.State = state1;
            Cell cell2 = new Cell();
            int index2 = 9;
            char state2 = '4';
            cell2.Indx = index2;
            cell2.State = state2;
            List<Cell> cells = new List<Cell> { cell1, cell2 };
            Player player = new Player(1);
            List<Player> players = new List<Player> { player };
            Map map = new Map();
            map.Get_Map = cells;
            map.GetPlayers = players;
            Assert.AreEqual(map.Get_Map, cells);
            Assert.AreEqual(map.GetPlayers, players);
        }
        [TestMethod]
        public void Map_Action()
        {
            Cell cell1 = new Cell();
            int index1 = 0;
            char state1 = '0';
            cell1.Indx = index1;
            cell1.State = state1;
            Cell cell2 = new Cell();
            int index2 = 1;
            char state2 = '0';
            cell2.Indx = index2;
            cell2.State = state2;
            List<Cell> cells = new List<Cell> { cell1, cell2 };
            Player player = new Player(1);
            List<Player> players = new List<Player> { player };
            Map map = new Map();
            map.Get_Map = cells;
            map.GetPlayers = players;
            map.Action(player);
            Assert.AreEqual(player.Pos, 1);
        }

    }
}
