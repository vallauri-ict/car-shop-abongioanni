using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net;
using MySql.Data.MySqlClient;

namespace DbDllProject {
    public class AccessUtils {
        public static string connStr = "Provider=Microsoft.Ace.Oledb.12.0;Data Source={DbPath};";
        public static DataTable GetRows(string DbPath, string sqlString)
        {
            DataTable t = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connStr.Replace("{DbPath}", DbPath))) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sqlString, connection);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(t);
            }
            return t;
        }

        public static DataTable GetRows(string DbPath, OleDbCommand cmd)
        {
            DataTable t = new DataTable();
            using (OleDbConnection connection = new OleDbConnection(connStr.Replace("{DbPath}", DbPath))) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                cmd.Connection = connection;
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(t);
            }
            return t;
        }

        public static int ExecQuery(string DbPath, string sqlString)
        {
            int n = 0;
            using (OleDbConnection connection = new OleDbConnection(connStr.Replace("{DbPath}", DbPath))) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                n = new OleDbCommand(sqlString, connection).ExecuteNonQuery();
            }
            return n;
        }

        public static int ExecQuery(OleDbCommand cmd)
        {
            cmd.Connection.Open();
            //cmd.Prepare();    //VA SEMPRE IN EXCEPTION
            return cmd.ExecuteNonQuery();
        }
    }
    public class SqlServerUtils {//MANCANO PARTI
        public static string connStr;
        public static DataTable GetRows(string sqlString)
        {
            DataTable t = new DataTable();
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sqlString, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(t);
            }
            return t;
        }

        public static int ExecQuery(string sqlString)
        {
            sqlString = sqlString.Replace('#', '\'');
            int n = 0;
            using (SqlConnection connection = new SqlConnection("Provider=SQLOLEDB;" + connStr)) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                n = new SqlCommand(sqlString, connection).ExecuteNonQuery();
            }
            return n;

        }
    }
    public class MySqlUtils {//MANCANO PARTI
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public static string connStr = @"server=localhost;database={DbPath};Uid=root;Pwd=;convert zero datetime=True";
        public static DataTable GetRows(string dbName, string sqlString)
        {
            DataTable t = new DataTable();
            using (MySqlConnection connection = new MySqlConnection(connStr.Replace("{DbPath}", dbName)))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sqlString, connection);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(t);
            }
            return t;
        }

        public static int ExecQuery(string dbName, string sqlString)
        {
            sqlString = sqlString.Replace("#", "'");
            int n = 0;
            using (MySqlConnection connection = new MySqlConnection(connStr.Replace("{DbPath}", dbName))) //data reader: oggetto per recuperare dati
            {
                connection.Open();
                n = new MySqlCommand(sqlString, connection).ExecuteNonQuery();
            }
            return n;

        }
    }
}
