using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class User
    {
        private string name;
        private int score;
        public User(string username) {
            name = username;
            score = 0;
        }

        public void ScoreIncrement(int up) {
            score += up;
        }

        public string getName
        {
            get { return name; }
            set { name = value; }
        }

        public int getScore {
            get { return score; }
            set { score = value; }
        }
    }
}
