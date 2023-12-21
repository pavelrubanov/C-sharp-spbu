using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class Player
    {
        public string Name;
        public int Money;
        Board Board;
        LinkedListNode<Square> Position;
        LinkedList<SquareProperty> Properties;
        public int IsVeryLucky; //Увеличивается, если игрок выбрасывает дубль
        public bool InJail;
        public bool CanPlay;

        public Player() { }
        public Player(string name, Board _board)
        {
            Name = name;
            Money = 1500;
            Board = _board;
            Position = Board.Squares.First;
            Properties = new LinkedList<SquareProperty>();
            IsVeryLucky = 0;
            InJail = false;
            CanPlay = true;
        }

        public void Go(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                if (Position.Next != null) { Position = Position.Next; }
                else { Board.Squares.First.Value.ApplyBonus(this); Position = Board.Squares.First;}
            }
            Position.Value.ApplyTo(this);
        }
        public void Pay(int money)
        {
            if (Money >= money) { Money -= money; }
            else
            {
                while (Properties.Count > 0 && Money < money)
                {
                    SquareProperty PropertyToSell = WhatToSell();
                    Sell(PropertyToSell);
                }
                if (Money >= money) { Money -= money; }
                else
                {
                    CanPlay = false;
                    throw new Exception("Игрок обанкротился");
                }
                if (Money == 0)
                {
                    CanPlay = false;
                }
            }
        }
        public void Recieve(int money)
        {
            Money += money;
        }
        public void Buy(SquareProperty property)
        {
            if (Money >= property.Cost) 
            {
                Money -= property.Cost;
                Properties.AddLast(property);
            }
            else
            {
                throw new Exception("Недостаточно денег");
            }
        }
        public void Sell(SquareProperty propertyToSell)
        {
            Money += propertyToSell.Cost;
            propertyToSell.Sell();
            Properties.Remove(propertyToSell);
        }
        public void IsLucky()
        {
            IsVeryLucky++;
            if (IsVeryLucky >= 3)
            {
                GoToJail();
            }
        }
        public void IsNotLacky()
        {
            IsVeryLucky = 0;
        }
        public void GoToJail()
        {
            Position = Board.JailSquare;
            IsVeryLucky = 0;
            InJail = true;
        }
        public void GetOutOfJail()
        {
            InJail = false;
        }
        public SquareProperty WhatToSell()
        {
            SquareProperty propertyToSell = Properties.Last.Value; //Добавить более умный алгоритм
            return propertyToSell;
        }
        public int MoneyOfProperty()
        {
            int AllCost = 0;
            foreach (SquareProperty property in Properties)
            {
                AllCost += property.Cost;
            }
            return AllCost;
        }
    }
}
