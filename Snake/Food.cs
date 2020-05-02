﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Food
    {
        public int x { get; set; }
        public int y { get; set; }
        public Food()
        {
            Random random = new Random();
            this.x = random.Next(Console.WindowWidth);
            this.y = random.Next(Console.WindowHeight);
        }

        public void Generate_random_food()
        {
            Console.SetCursorPosition(this.x, this.y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("@");
        }
    }
}
