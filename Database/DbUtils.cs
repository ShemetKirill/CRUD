using MySql.Data.MySqlClient;

namespace CRUD.Database
{
    public  static class DbUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "crud";
            string username = "root";
            string password = "97efebis";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }
    }
}
