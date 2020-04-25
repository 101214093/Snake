using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.IO;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialize objects
            UserManagement userlist = new UserManagement();
            Console.WriteLine("Please enter your username: ");
            string userName = Console.ReadLine();
            User user = new User(userName);
            Console.Clear();

            // initialize background music
            System.Media.SoundPlayer bgm = new System.Media.SoundPlayer();
            bgm.SoundLocation = "../../sound/bgm.wav";
            bgm.Play();
                        
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;

            int lastFoodTime = 0;
            int foodDissapearTime = 16000;
//            bool isGameOver = false;


            Position[] directions = new Position[]
            {
                new Position(0, 1), // right
                new Position(0, -1), // left
                new Position(1, 0), // down
                new Position(-1, 0), // up
            };

            Console.BufferHeight = Console.WindowHeight;
            double sleepTime = 100;
            lastFoodTime = Environment.TickCount;

            // Initialize obstacle and draw obstacles
            Obstacle obs = new Obstacle();
            obs.Generate_random_obstacle();

            foreach (Position obstacle in obs.GetObsPos)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(obstacle.col, obstacle.row);
                Console.Write("=");
            }

            // Iniatitlize food and draw food
            Food food = new Food();
            food.Generate_random_food();

            // Initialize snake and draw snake
            Snake snake = new Snake();
            snake.DrawSnake();
            int direct = right;

            // looping
            while (true)
            {
                // Set the score at the top right
                Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 30);
                Console.WriteLine("Score: " + user.getScore);

                // check for key pressed
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if (direct != right) direct = left;
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direct != left) direct = right;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direct != down) direct = up;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direct != up) direct = down;
                    }
                }

                // update snake position
                Position snakeHead = snake.GetPos.Last();
                Position nextDirection = directions[direct];

                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row,
                    snakeHead.col + nextDirection.col);

                // check for snake if exceed the width or height
                if (snakeNewHead.col < 0) snakeNewHead.col = Console.WindowWidth - 1;
                if (snakeNewHead.row < 0) snakeNewHead.row = Console.WindowHeight - 1;
                if (snakeNewHead.row >= Console.WindowHeight) snakeNewHead.row = 0;
                if (snakeNewHead.col >= Console.WindowWidth) snakeNewHead.col = 0;

                // check for snake collison with self or obstacles
                if (snake.GetPos.Contains(snakeNewHead) || obs.GetObsPos.Contains(snakeNewHead))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Game over!");
                    return;
                }
                
                // check for collision with the food
                if (snakeNewHead.col == food.x && snakeNewHead.row == food.y)
                {
                    System.Media.SoundPlayer eat = new System.Media.SoundPlayer();
                    eat.SoundLocation = "../../sound/coin.wav";
                    eat.Play();
                    user.ScoreIncrement(1);
                    Console.SetCursorPosition(Console.WindowWidth - 10, Console.WindowHeight - 30);
                    Console.WriteLine("Score: " + user.getScore);
                    //Console.WriteLine("Eaten");
                    bgm.Play();
                    food = new Food();
                    food.Generate_random_food();
                }
                // draw the snake
                Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("*");

                // moving
                snake.GetPos.Enqueue(snakeNewHead);
                Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                Console.ForegroundColor = ConsoleColor.Gray;

                // draw the snake head
                if (direct == right)
                {
                    Console.Write(">");
                }
                if (direct == left)
                {
                    Console.Write("<");
                }
                if (direct == up)
                {
                    Console.Write("^");
                }
                if (direct == down)
                {
                    Console.Write("v");
                }

                // moving...
                Position last = snake.GetPos.Dequeue();
                Console.SetCursorPosition(last.col, last.row);
                Console.Write(" ");

                // set winning condition score = 5
                if (user.getScore == 5)
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth/2, Console.WindowHeight/2);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Stage Clear!");
                    userlist.AddUser(user);

                    var x = userlist.getUsers;
                    userlist.recordUser();
                    //userlist.readRecord();
                    return;
                }

                // food timer
                if (Environment.TickCount - lastFoodTime >= foodDissapearTime)
                {
                    Console.SetCursorPosition(food.x, food.y);
                    Console.Write(" ");
                    food = new Food();
                    food.Generate_random_food();

                    lastFoodTime = Environment.TickCount;
                }

                sleepTime -= 0.01;
                Thread.Sleep((int)sleepTime);
            }
        }
    }
}
