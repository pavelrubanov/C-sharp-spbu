using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    internal class Street : Cell
    {
        public bool IsBought { get; set; }
        public Player Owner { get; private set; }
        private int Rent;
        public Street(string Name) : base(Name)
        {
        }
        public override void LandedOn(Player player)
        {
            base.LandedOn(player);
            if (IsBought == true &&  Owner != player) 
            {
                PayRent(player);
            }
            else if (IsBought == false)
            {
                //покупать
            }
            else
            {
                //ставить отель
            }
        }
        private void PayRent(Player player)
        {
            player.Money -= GetRent();
            Owner.Money += GetRent();
        }
        public int GetRent ()
        {
            return Rent;
        }
    }
}
