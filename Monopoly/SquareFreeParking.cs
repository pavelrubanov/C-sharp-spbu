using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class SquareFreeParking : Square
    {
        public SquareFreeParking()
        {
            Title = "FreeParking";
        }
        public override void ApplyTo(Player player)
        {
            Console.WriteLine("Игрок {0} попал на бесплатную парковку", player.Name);
        }
    }
}
