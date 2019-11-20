using System;

namespace Example_06.TemplateMethodBefore
{
    public class SimpleGame
    {
        private int _count;

        public void Play()
        {
            _count = 3;
            Console.WriteLine("Началась простая игра");
            while (_count > 0)
            {
                _count--;
                Console.WriteLine("Шаг простой игры");
            }
            Console.Write($"Игра окончена со счетом: {3}");
        }
    }

    public class RandomGame
    {
        private readonly Random _rand = new Random();
        private int _count;
        private int _totalResult;

        public void Play()
        {
            _count = _rand.Next(5);
            _totalResult = _count;
            Console.WriteLine("Началась случайная игра");

            while (_count > 0)
            {
                _count--;
                Console.WriteLine("Шаг случайной игры");
            }

            Console.Write($"Игра окончена со счетом: {_totalResult}");
        }
    }
}
