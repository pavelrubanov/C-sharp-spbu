using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    abstract class SquareProperty : Square
    {
        public bool IsOwned = false;
        public Player Owner;
        public int Cost;
        public int Rent;
        public Board Board;
        public abstract void PayRent(Player payer);
        public override void ApplyTo(Player player)
        {
            if (IsOwned)
            {
                if (Owner == player)
                {
                    Console.WriteLine("Игрок {0} остановился на своей клетке {1}", player.Name, Title);
                    try
                    {
                        player.Buy(this);
                        Console.WriteLine("Игрок {0} поставил здание на {1} за ${2}", player.Name, Title, Cost);
                    }
                    catch
                    {
                        Console.WriteLine("Игроку {0} не хватило денег, чтобы купить {1}", player.Name, Title);
                    }
                }
                else
                {
                    try
                    {
                        PayRent(player);
                    }
                    catch
                    {
                        player.CanPlay = false;
                        Console.WriteLine("Игрок {0} не смог заплатить аренду игроку {1} и обанкротился", player.Name, Owner.Name);
                    }
                }
            }
            else
            {
                try
                {
                    player.Buy(this);
                    IsOwned = true;
                    Owner = player;
                    Console.WriteLine("Игрок {0} купил {1} за ${2}", player.Name, Title, Cost);
                }
                catch
                {
                    Console.WriteLine("Игроку {0} не хватило денег, чтобы купить {1}", player.Name, Title);
                }
            }
        }
        public void Sell()
        {
            IsOwned = false;
            Owner = null;
        }
        public void Buy(Player player)
        {
            try
            {
                player.Buy(this);
                IsOwned = true;
                Owner = player;
            }
            catch
            {
                Console.WriteLine("Игроку {0} не хватило денег, чтобы купить {0}", player.Name, Cost);
            }
        }
    }
}
