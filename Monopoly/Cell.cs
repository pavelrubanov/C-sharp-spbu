using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    internal class Cell
    {
        public Cell(string Name)
        { 
            this.Name = Name;
        }
        public string Name { get; private set; }
        public override string ToString()
        {
            return Name;
        }
        public virtual void LandedOn(Player player)
        {
            Console.WriteLine(player.Name + " landed on " + this);
        }
    }
}
