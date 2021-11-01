using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace HIT_Canteen_POS
{
    class DB
    {
        MySqlConnection connection = new MySqlConnection("server=localhost; database=water; username=root; password=;");

        // create a function to open the connection
        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }


        // create a function to close the connection
        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        //create funvtion to return the connection
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
