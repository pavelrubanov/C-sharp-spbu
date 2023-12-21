using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class MonopolyGame
    {
        Board board;
        Dice dice;
        int Steps;
        LinkedList<Player> ListOfPlayers;
        LinkedListNode<Player> CurrentPlayer;
        Player currentPlayer;
        public MonopolyGame(Board _board, LinkedList<string> listOfNames)
        {
            board = _board;
            dice = new Dice();
            Steps = 1;
            ListOfPlayers = new LinkedList<Player>();
            foreach (string name in listOfNames)
            {
                Player player = new Player(name, board);
                ListOfPlayers.AddLast(player);
            }
            CurrentPlayer = ListOfPlayers.Last;
        }
        public void Start()
        {
            while (ListOfPlayers.Count >= 2 && Steps <= 1000)
            {
                if (CurrentPlayer.Next != null) { CurrentPlayer = CurrentPlayer.Next; }
                else { CurrentPlayer = ListOfPlayers.First; }
                currentPlayer = CurrentPlayer.Value;

                if (currentPlayer.InJail)
                {
                    currentPlayer.GetOutOfJail();
                    Console.WriteLine(Steps + ") Игрок " + currentPlayer.Name + " пропускает ход и выходит из тюрьмы");
                    Steps++;
                    continue;
                }

                Console.WriteLine(Steps + ") Ходит " + currentPlayer.Name + ". Баланс: $" + currentPlayer.Money);
                dice.Roll();
                Console.WriteLine("На кубиках выпало: " + dice.Points1 + " и " + dice.Points2);
                while (dice.Points1 == dice.Points2 && currentPlayer.CanPlay)
                {
                    currentPlayer.IsLucky();
                    if (currentPlayer.InJail)
                    {
                        Console.WriteLine("Игрок " + currentPlayer.Name + " выбросил дубль 3 раза и попадает в тюрьму за жульничество");
                        Steps++;
                        break;
                    }
                    else
                    {
                        currentPlayer.Go(dice.Points1 + dice.Points2);
                        Steps++;
                        if (!currentPlayer.CanPlay)
                        {
                            ListOfPlayers.Remove(currentPlayer);
                            break;
                        }
                        if (!currentPlayer.InJail)
                        {
                            Console.WriteLine(Steps + ") Игрок " + currentPlayer.Name + " выбросил дубль и ходит ещё раз. Баланс: $" + currentPlayer.Money);
                            dice.Roll();
                            Console.WriteLine("На кубиках выпало: " + dice.Points1 + " и " + dice.Points2);
                        }
                    }
                }
                if (!currentPlayer.CanPlay)
                {
                    ListOfPlayers.Remove(currentPlayer);
                    continue;
                }
                if (!currentPlayer.InJail)
                {
                    currentPlayer.IsNotLacky();
                    currentPlayer.Go(dice.Points1 + dice.Points2);
                    Steps++;
                    if (!currentPlayer.CanPlay)
                    {
                        ListOfPlayers.Remove(currentPlayer);
                        continue;
                    }
                }
            }

            Player WinnerPlayer = new Player();
            int MaxMoney = 0;
            foreach (Player player in ListOfPlayers)
            {
                if (player.Money + player.MoneyOfProperty() > MaxMoney)
                {
                    MaxMoney = player.Money + player.MoneyOfProperty();
                    WinnerPlayer = player;
                }
            }
            Console.WriteLine("\nПоздравляем игрока {0} с победой! Суммарная стоимость собственности: ${1}", WinnerPlayer.Name, MaxMoney);
        }
    }
}
