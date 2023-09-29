using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList<string> ListOfNames = new LinkedList<string>();
            ListOfNames.AddLast("Bennet");
            ListOfNames.AddLast("Layla");
            ListOfNames.AddLast("Sara");
            Board board = new Board();
            MonopolyGame play1 = new MonopolyGame(board, ListOfNames);
            play1.Start();
            Console.ReadLine();
        }
    }
}
