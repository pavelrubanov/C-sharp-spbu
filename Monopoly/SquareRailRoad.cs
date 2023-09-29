using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class SquareRailRoad : SquareProperty
    {
        int MonopolyConst; //1, 2, 4, 8
        public SquareRailRoad(Board board, string title, int cost, int rent)
        {
            Board = board;
            Title = title;
            Cost = cost;
            Rent = rent;
            MonopolyConst = 1;
        }
        public override void PayRent(Player payer)
        {
            MonopolyConst = Board.HasMonopolyRailRoad(this);
            try
            {
                payer.Pay(MonopolyConst * Rent);
                Owner.Recieve(MonopolyConst * Rent);
                Console.WriteLine("Игрок {0} остановился на клетке {1} и заплатил аренду игроку {2} в размере ${3}", payer.Name, Title, Owner.Name, Rent);
            }
            catch (Exception ex)
            {
                throw ex;
                //Console.WriteLine("Игрок {0} не смог заплатить аренду игроку {1} и обанкротился", payer.Name, Owner.Name);
            }
        }
    }
}
