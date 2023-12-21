using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class SquareTax : Square
    {
        int Tax;
        public SquareTax(string title, int tax)
        {
            Title = title;
            Tax = tax;
        }
        public override void ApplyTo(Player player)
        {
            try
            {
                player.Pay(Tax);
                Console.WriteLine("Игрок {0} заплатил налог {1} в размере ${2}", player.Name, Title, Tax);
            }
            catch
            {
                player.CanPlay = false;
                Console.WriteLine("Игрок {0} обанкротился, пытаясь заплатить налог {1}", player.Name, Title);
            }
        }
    }
}
