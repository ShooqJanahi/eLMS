using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_project
{
    internal class DBManager
    {

        public class DatabaseConnection
        {
            private static DatabaseConnection _instance;
            private readonly string _connectionString;
            private DatabaseConnection()
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string databaseFileName = "Database1.mdf";

                string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={basePath}{databaseFileName};Integrated Security=True";
                //MessageBox.Show(connectionString);
                _connectionString = connectionString;
                // Your updated connection string
               

            }

            public static DatabaseConnection Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseConnection();
                    }
                    return _instance;
                }
            }

            public SqlConnection GetConnection()
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}
