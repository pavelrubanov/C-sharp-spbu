using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class SquareGoToJail : Square
    {
        public SquareGoToJail(string title)
        {
            Title = title;
        }
        public override void ApplyTo(Player player)
        {
            player.GoToJail();
            Console.WriteLine("Игрок {0} остановился на клетке {1} и отправляется в тюрьму", player.Name, Title);
        }
    
    }
}
