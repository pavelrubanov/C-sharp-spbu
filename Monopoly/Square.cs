using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    abstract  class Square
    {
        public string Title;
        public abstract void ApplyTo(Player player);
        public virtual void ApplyBonus(Player player)
        {
            ApplyBonus(player);
        }
    }
}
