using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Project_35_Монополия
{
    internal class Board
    {
        public LinkedList<Square> Squares;
        public LinkedListNode<Square> JailSquare = new LinkedListNode<Square>(new SquareJail());
         public Board()
        {
            Squares = new LinkedList<Square>();
            Squares.AddLast(new SquareStart("GO", 200));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Mediterranean Avenue", "Brown", 60, 2));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Baltic Avenue", "Brown", 60, 4));
            Squares.AddLast(new SquareTax("IncomeTax", 200));
            Squares.AddLast(new SquareRailRoad(this, "Reading Railroad", 200, 25));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Oriental Avenue", "LightBlue", 100, 6));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Vermont Avenue", "LightBlue", 100, 6));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Connecticut Avenue", "LightBlue", 120, 8));
            Squares.AddLast(JailSquare);
            Squares.AddLast(new SquarePropertyOfStreets(this, "St. Charles Place", "Pink", 140, 10));
            Squares.AddLast(new SquareUtility(this, "Electric Company", 150));
            Squares.AddLast(new SquarePropertyOfStreets(this, "States Avenue", "Pink", 140, 10));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Virginia Avenue", "Pink", 160, 12));
            Squares.AddLast(new SquareRailRoad(this, "Pennsylvania Railroad", 200, 25));
            Squares.AddLast(new SquarePropertyOfStreets(this, "St. James Place", "Orange", 180, 14));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Tennessee Avenue", "Orange", 180, 14));
            Squares.AddLast(new SquarePropertyOfStreets(this, "New York Avenue", "Orange", 200, 16));
            Squares.AddLast(new SquareFreeParking());
            Squares.AddLast(new SquarePropertyOfStreets(this, "Kentucky Avenue", "Red", 220, 18));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Indiana Avenue", "Red", 220, 18));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Illinois Avenue", "Red", 240, 20));
            Squares.AddLast(new SquareRailRoad(this, "B. & O. Railroad", 200, 25));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Atlantic Avenue", "Yellow", 260, 22));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Ventnor Avenue", "Yellow", 260, 22));
            Squares.AddLast(new SquareUtility(this, "Water Works", 150));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Marvin Gardens", "Yellow", 280, 24));
            Squares.AddLast(new SquareGoToJail("Go to Jail"));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Pacific Avenue", "Green", 300, 26));
            Squares.AddLast(new SquarePropertyOfStreets(this, "North Carolina Avenue", "Green", 300, 26));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Pennsylvania Avenue", "Green", 320, 28));
            Squares.AddLast(new SquareRailRoad(this, "Short Line", 200, 25));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Park Place", "Blue", 350, 35));
            Squares.AddLast(new SquareTax("Luxury Tax", 100));
            Squares.AddLast(new SquarePropertyOfStreets(this, "Boardwalk", "Blue", 400, 50));
        }

        public bool HasMonopoly(SquarePropertyOfStreets currentProperty)
        {
            SquarePropertyOfStreets tempProperty;
            foreach (Square square in Squares)
            {
                try
                {
                    tempProperty = (SquarePropertyOfStreets)square;
                    if (tempProperty.Color == currentProperty.Color && tempProperty.Owner != currentProperty.Owner)
                    {
                        return false;
                    }
                }
                catch { }
            }
            return true;

        }
        public int HasMonopolyRailRoad(SquareRailRoad currentProperty)
        {
            SquareRailRoad tempProperty;
            int MonopolyConst = 0;
            foreach (Square square in Squares)
            {
                try
                {
                    tempProperty = (SquareRailRoad)square;
                    if (tempProperty.Owner != currentProperty.Owner)
                    {
                        if (MonopolyConst == 0) { MonopolyConst = 1; }
                        else { MonopolyConst *= 2; }
                    }
                }
                catch { }
            }
            return MonopolyConst;

        }
        public bool HasMonopolyUtility(SquareUtility currentProperty)
        {
            SquareUtility tempProperty;
            foreach (Square square in Squares)
            {
                try
                {
                    tempProperty = (SquareUtility)square;
                    if (tempProperty.Owner != currentProperty.Owner)
                    {
                        return false;
                    }
                }
                catch { }
            }
            return true;

        }
    }
}
