using System;
using System.Configuration;

namespace SQL
{
    public static class Const
    {
        public static string Host { get { return "rubeen.de"; } }
        public static int Port { get { return 3333; } }
        public static string User { get { return "root"; } }
        private static string _pass = ConfigurationManager.AppSettings["SSHPassword"];
        public static string Pass { get { return "kusapit(k3seq-k9goweqow&l"; } }
        public static uint SqlPort { get { return 3306; } }
        public static string SqlConn { get { return "Server=localhost; Port=3306; Database=rubeen_testdatenbank; Uid=datentester; Pwd=0sb7PmkkCl#r8Ajj"; } } 
        public static string SqlHost { get { return "localhost"; } }
        public static string SqlIPA { get { return "127.0.0.1"; } }
        public static string SqlSelect { get { return "SELECT tbl_users.id FROM tbl_users"; } }
    }
}
