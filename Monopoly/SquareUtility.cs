using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class SquareUtility: SquareProperty
    {
        Dice dice = new Dice();

        public SquareUtility(Board board, string title, int cost)
        {
            Board = board;
            Title = title;
            Cost = cost;
        }
        public override void PayRent(Player payer)
        {
            dice.Roll();
            if (Board.HasMonopolyUtility(this)) { Rent = 10 * (dice.Points1 + dice.Points2); }
            else { Rent = 4 * (dice.Points1 + dice.Points2); }
            try
            {
                payer.Pay(Rent);
                Owner.Recieve(Rent);
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
