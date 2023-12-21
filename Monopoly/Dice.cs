using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class Dice
    {
        public int Points1;
        public int Points2;
        Random random = new Random();

        public void Roll()
        {
            Points1 = random.Next(1, 7);
            Points2 = random.Next(1, 7);
        }
    }
}
