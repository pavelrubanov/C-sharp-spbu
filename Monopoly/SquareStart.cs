using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class SquareStart : Square
    {
        int Bonus;

        public SquareStart(string title, int bonus)
        {
            Title = title;
            Bonus = bonus;
        }
        public override void ApplyTo(Player player)
        {
            Console.WriteLine("Игрок {0} остановился на клетке {1}", player.Name, Title);
        }
        public override void ApplyBonus(Player player)
        {
            player.Recieve(Bonus); //С этим бонусом они богатеют быстрее, чем разоряются
            Console.WriteLine("Игрок {0} не получил бонус в размере ${1}", player.Name, Bonus);
        }
    }
}
