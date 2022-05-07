using System;
using SQLite;
using System.Collections.Generic;
using System.IO;
using Android.Media;
using Android.Content;

namespace minesweeper
{
    static class Con
    {
        //global variables
        public static DB db = new DB();
        public static string username;
        public static MediaPlayer gPlayer = new MediaPlayer();
        public static Context gCon;

        //string constants
        public const string DB_PATH = "scores.sqlite";
        public const int EASY_DIFFICULTY = 10;
        public const int NORMAL_DIFFICULTY = 20;
        public const int HARD_DIFFICULTY = 30;


        //structs
        public struct UserT
        {
            public string username { get; set; }
            public string password { get; set; }
        }

    }

    class DB
    {
        private SQLiteConnection con;

        public DB()
        {
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Con.DB_PATH);
            this.con = new SQLiteConnection(databasePath);
            this.con.CreateTable<DatabaseT>();

            this.con.Execute("CREATE TABLE IF NOT EXISTS users(id INTEGER PRIMARY KEY AUTOINCREMENT, USERNAME TINYTEXT, PASSWORD TINYTEXT, score TEXT);");
        }

        ~DB()
        {
            this.con.Close();
        }

        public bool Register(Con.UserT det)
        {
            var users = this.con.Query<DatabaseT>("Select USERNAME from users");
            if (checkExists(users, det.username))
            {
                return false;
            }
            else
            {
                this.con.Execute("INSERT INTO users (USERNAME, PASSWORD, score) VALUES ('" + det.username + "', '" + det.password + "', '0');");
                Con.username = det.username;
            }
            return true;
        }

        public int Login(Con.UserT det)
        {
            var users = this.con.Query<DatabaseT>("Select USERNAME from users");
            if (checkExists(users, det.username))
            {
                this.con.Query<DatabaseT>("Select PASSWORD from users where USERNAME='" + det.username + "';");
                if (this.con.Query<DatabaseT>("Select PASSWORD from users where USERNAME='" + det.username + "';")[0].password == det.password)
                {
                    Con.username = det.username;
                    return 0; //user connected
                }
                else
                {
                    return -1; //password incorrect
                }

            }
            else
            {
                return 1; //user doesnt exist
            }
        }

        public void AddScore()
        {
            string res = this.con.Query<DatabaseT>(@"select score from users where username='" + Con.username + "';")[0].score;
            res = (int.Parse(res) + 1).ToString();
            this.con.Execute("UPDATE users set score='" + res + "' where USERNAME='" + Con.username + "';");
        }

        public int Getscore()
        {
            string res = this.con.Query<DatabaseT>(@"select score from users where username='" + Con.username + "';")[0].score;
            return int.Parse(res);
        }

        public bool checkExists(List<DatabaseT> users, string username)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].username.CompareTo(username) == 0)
                {
                    return true;
                }
            }
            return false;
        }

    }

    class DatabaseT
    {
        public string username { get; set; }
        public string password { get; set; }
        public string score { get; set; }
    }

}