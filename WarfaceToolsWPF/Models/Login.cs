using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace WarfaceToolsWPF.Models
{
    public sealed class Login
    {
        private Login() { }
        private static Login _instance;

        public static Login Instance => _instance ?? (_instance = new Login());

        private const string ConnectionString =
           @"server=localhost;database=warfacetoolsdb;uid=root;pwd=dupadupa8";

        public static MySqlConnection Conn;

        public static void DatabaseConnect()
        {
            try
            {
                Conn = new MySqlConnection(ConnectionString);
                Console.WriteLine(@"Conn1: " + Conn);
                Conn.Open();
                Console.WriteLine(@"Connection opened.");
                Console.WriteLine(@"MySQL version : {0}", Conn.ServerVersion);
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 0)
                {
                    throw new CustomException("Cannot connect to server. Contact administrator.", 1000);
                }
                Console.WriteLine(@"Error: {0}", ex);
            }
            finally
            {
                Conn.Close();
            }
        }

        private static bool UserConnect(User user)
        {
            string command = "select *  from users where login = '" + user.Username 
                + "' && password = '" + user.Password + "';";
            Console.WriteLine(@"String select: " + command);
            Console.WriteLine(@"Conn: " + Conn);
            MySqlDataAdapter da = new MySqlDataAdapter(command,Conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "users");
            DataTable dt = ds.Tables["users"];

            foreach (DataRow row in dt.Rows)
            {
                if (row["login"] == null) continue;
                Console.WriteLine(row["login"].ToString());
                user.IsLoggedIn = true;
                return true;
            }
            return false;
        }

        public static void Authenticate(User user)
        {
            if (string.IsNullOrEmpty(user.Username))
                throw new CustomException("Username is null or empty.",1001);
            if (string.IsNullOrEmpty(user.Password))
                throw new CustomException("Password is null or empty.", 1002);
            if (!UserConnect(user))
                throw new CustomException("Username or password invalid.", 1003);
        }

    }
}
