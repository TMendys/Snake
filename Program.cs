using System;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            GameModel Game = new GameModel();

            while (!Game.GameOver)
            {
                var keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (Game.Direction == Directions.Down)
                            break;
                        else
                            Game.Direction = Directions.Up;
                        break;
                    case ConsoleKey.RightArrow:
                        if (Game.Direction == Directions.Left)
                            break;
                        else
                            Game.Direction = Directions.Right;
                        break;
                    case ConsoleKey.DownArrow:
                        if (Game.Direction == Directions.Up)
                            break;
                        else
                            Game.Direction = Directions.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Game.Direction == Directions.Right)
                            break;
                        else
                            Game.Direction = Directions.Left;
                        break;
                }
            }
            Console.WriteLine("Game Over!");
            Console.ReadKey();
        }
    }
}
