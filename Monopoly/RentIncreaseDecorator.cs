using System;

namespace AK_Project_35_Монополия
{
    // Декоратор для увеличения стоимости аренды
    class RentIncreaseDecorator : SquareProperty
    {
        private SquareProperty decoratedSquare;
        private int rentIncreaseAmount;

        public RentIncreaseDecorator(SquareProperty square, int increaseAmount)
        {
            decoratedSquare = square;
            rentIncreaseAmount = increaseAmount;
        }

        public override void PayRent(Player payer)
        {
            decoratedSquare.PayRent(payer);

            // Увеличение стоимости аренды
            int increasedRent = decoratedSquare.Rent + rentIncreaseAmount;

            Console.WriteLine("Игрок {0} остановился на клетке {1} и заплатил увеличенную аренду игроку {2} в размере ${3}", payer.Name, decoratedSquare.Title, decoratedSquare.Owner.Name, increasedRent);
        }

        // Перенаправление остальных методов к декорируемому объекту

        public override void ApplyTo(Player player)
        {
            decoratedSquare.ApplyTo(player);
        }

        public override void ApplyBonus(Player player)
        {
            decoratedSquare.ApplyBonus(player);
        }

        public new void Sell()
        {
            decoratedSquare.Sell();
        }

        public new void Buy(Player player)
        {
            decoratedSquare.Buy(player);
        }
    }
}
