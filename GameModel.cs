using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake
{
    public enum Directions
    {
        Up,
        Right,
        Down,
        Left
    }
    class GameModel
    {
        private readonly Timer timer;
        public bool GameOver { get; set; }
        public Directions Direction { get; set; } = Directions.Up;

        private Random random = new Random();

        private char[,] gameTable = new char[10, 20];

        private List<Point> Snake = new List<Point>();

        private const char snake = 'S';

        private const char food = 'F';

        private const char empty = '\u2591';

        private int speed = 400;

        private int score = 0;

        public GameModel ()
        {
            GameOver = false;
            SetEmpty();
            Snake.Add(SetSnake());
            DrawSnake();
            SetFood();
            PrintTable();

            timer = new Timer(MoveSnake, null, 0, speed);
        }

        private void MoveSnake(object state)
        {
            Point cordinates = new Point
            {
                X = Snake.First().X,
                Y = Snake.First().Y
            };

            switch(Direction){
                case Directions.Up:
                    cordinates.X--;
                    break;
                case Directions.Right:
                    cordinates.Y++;
                    break;
                case Directions.Down:
                    cordinates.X++;
                    break;
                case Directions.Left:
                    cordinates.Y--;
                    break;
            }

            try
            {
                if (gameTable[cordinates.X, cordinates.Y].Equals(food))
                {
                    gameTable[Snake.Last().X, Snake.Last().Y] = empty;
                    SetFood();
                    score++;
                }
                else if (gameTable[cordinates.X, cordinates.Y].Equals(snake))
                {
                    GameOver = true;
                    timer.Dispose();
                }
                else
                {
                    gameTable[Snake.Last().X, Snake.Last().Y] = empty;
                    Snake.RemoveAt(Snake.Count - 1);
                    //Snake.Remove(Snake.Find());
                }

                Snake.Insert(0, cordinates);

                DrawSnake();
                PrintTable();

            }
            catch (Exception)
            {
                GameOver = true;
                timer.Dispose();
            }
        }

        private void DrawSnake()
        {
            foreach (Point cordinates in Snake)
            {
                gameTable[cordinates.X, cordinates.Y] = snake;
            }
        }

        private Point SetSnake()
        {
            Point cordinates = new Point
            {
                X = random.Next(3, gameTable.GetLength(0)-3),
                Y = random.Next(3, gameTable.GetLength(1)-3)
            };

            return cordinates;
        }

        private void SetEmpty()
        {

            for (int x = 0; x < gameTable.GetLength(0); x++)
            {
                for (int y = 0; y < gameTable.GetLength(1); y++)
                {
                    gameTable[x, y] = empty;
                }
            }
        }

        private void SetFood()
        {
            int x;
            int y;

            do
            {
                x = random.Next(gameTable.GetLength(0));
                y = random.Next(gameTable.GetLength(1));
            } while (gameTable[x, y] == snake);

            gameTable[x, y] = food;
        }

        private void PrintTable()
        {
            Console.Clear();

            for (int x = 0; x < gameTable.GetLength(0); x++)
            {
                for (int y = 0; y < gameTable.GetLength(1); y++)
                {
                    Console.Write(gameTable[x, y]);
                }
                Console.WriteLine();
                
            }

            Console.WriteLine();
            Console.WriteLine($"Score: {score}");
            Console.WriteLine();
        }
    }
}
