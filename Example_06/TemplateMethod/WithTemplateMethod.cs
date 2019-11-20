using System;

namespace Example_06.TemplateMethod
{
    public abstract class GameBase
    {
        public void Play()
        {
            InitGame();
            while (!IsGameOver())
            {
                NextTurn();
            }
            Console.Write($"Игра окончена со счетом: {GetTotalResult()}");
        }

        protected abstract void InitGame();
        protected abstract bool IsGameOver();
        protected abstract void NextTurn();
        protected abstract int GetTotalResult();
    }

    public class SimpleGame : GameBase
    {
        private int _count;

        protected override void InitGame()
        {
            _count = 3;
            Console.WriteLine("Началась простая игра");
        }

        protected override bool IsGameOver()
        {
            return _count > 0;
        }

        protected override void NextTurn()
        {
            _count--;
            Console.WriteLine("Шаг простой игры");
        }

        protected override int GetTotalResult()
        {
            return 3;
        }
    }

    public class RandomGame : GameBase
    {
        private readonly Random _rand = new Random();
        private int _count;
        private int _totalResult;

        protected override void InitGame()
        {
            Console.WriteLine("Началась случайная игра");
            _count = _rand.Next(5);
            _totalResult = _count;
        }

        protected override bool IsGameOver()
        {
            return _count > 0;
        }

        protected override void NextTurn()
        {
            _count--;
            Console.WriteLine("Шаг случайной игры");
        }

        protected override int GetTotalResult()
        {
            return _totalResult;
        }
    }
}
