using System;

namespace Example_06.Strategy
{
    public class Game
    {
        public void Play(IGameStrategy strategy)
        {
            strategy.InitGame();
            while (!strategy.IsGameOver())
            {
                strategy.NextTurn();
            }
            Console.Write($"Игра окончена со счетом: {strategy.GetTotalResult()}");
        }
    }

    public interface IGameStrategy
    {
        void InitGame();
        bool IsGameOver();
        void NextTurn();
        int GetTotalResult();
    }

    public class SimpleStrategy : IGameStrategy
    {
        private int _count;

        public void InitGame()
        {
            _count = 3;
            Console.WriteLine("Началась простая игра");
        }

        public bool IsGameOver()
        {
            return _count > 0;
        }

        public void NextTurn()
        {
            _count--;
            Console.WriteLine("Шаг простой игры");
        }

        public int GetTotalResult()
        {
            return 3;
        }
    }

    public class RandomStrategy : IGameStrategy
    {
        private readonly Random _rand = new Random();
        private int _count;
        private int _totalResult;

        public void InitGame()
        {
            Console.WriteLine("Началась случайная игра");
            _count = _rand.Next(5);
            _totalResult = _count;
        }

        public bool IsGameOver()
        {
            return _count > 0;
        }

        public void NextTurn()
        {
            Console.WriteLine("Шаг случайной игры");
        }

        public int GetTotalResult()
        {
            return _totalResult;
        }
    }
}
