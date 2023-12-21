using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class SquarePropertyOfStreets : SquareProperty
    {
        public string Color;

        public SquarePropertyOfStreets(Board board, string title, string color, int cost, int rent)
        {
            Board = board;
            Title = title;
            Color = color;
            Cost = cost;
            Rent = rent;
        }
        public override void PayRent(Player payer)
        {
            try
            {
                if (Board.HasMonopoly(this))
                {
                    payer.Pay(2 * Rent);
                    Owner.Recieve(2 * Rent);
                    Console.WriteLine("Игрок {0} остановился на клетке {1} и заплатил двойную аренду игроку {2} в размере ${3}", payer.Name, Title, Owner.Name, 2 * Rent);
                }
                else
                {
                    payer.Pay(Rent);
                    Owner.Recieve(Rent);
                    Console.WriteLine("Игрок {0} остановился на клетке {1} и заплатил аренду игроку {2} в размере ${3}", payer.Name, Title, Owner.Name, Rent);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
