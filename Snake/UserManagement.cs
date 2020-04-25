using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Snake
{    
    class UserManagement
    {
        string name;
        int score;
        private List<User> userlist;
        public UserManagement() {
            userlist = new List<User>();
        }

        public void AddUser(User user) {
            userlist.Add(user);
        }

        public void readRecord()
        {
            var path = "userRecord.txt";
            using (StreamReader file = new StreamReader(path))
            {
                string ln;
                while ((ln=file.ReadLine()) != null)
                {
                    Console.WriteLine(ln);
                    ln = file.ReadLine();
                }
            }
        }

        public void recordUser() {
            var path = "userRecord.txt";
            StreamWriter sw = File.AppendText(path);
            foreach (User user in userlist) {
                //using (StreamWriter sw = File.AppendText(path)) { sw.WriteLine(user.getName+'\t'+user.getScore.ToString()); }
                sw.WriteLine(user.getName + '\t' + user.getScore.ToString());
            }
            sw.Close();
        }

        public List<User> getUsers
        {
            get { return userlist; }
        }
    }
}
