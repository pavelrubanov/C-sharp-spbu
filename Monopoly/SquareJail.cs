using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class SquareJail : Square
    {
        public SquareJail()
        {
            Title = "Jail";
        }
        public override void ApplyTo(Player player)
        {
            Console.WriteLine("Игрок {0} остановился на клетке {1}", player.Name, Title);
        }
    }
}
