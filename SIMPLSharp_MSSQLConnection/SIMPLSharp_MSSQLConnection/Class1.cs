using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using System.Data.SqlClient;


namespace SIMPLSharp_MSSQLConnection
{
    public class MSSQLConnection
    {
        private SqlConnection myConnection;  // Needed???
        public string databasename;

        public void makeConnection(string userid, string password, string server, string database)
        {
            databasename = database;
            myConnection = new SqlConnection("user id=" + userid + ";" + "password=" + password + ";" + "server=" + server + ";" + "database=" + database + ";");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error while connecting to the database:" + e.ToString());
            }
        }

        public string SelectRow(int id)
        {
            string commandString = "SELECT name FROM " + databasename + ".dbo.mytable WHERE id=" + id;
            string output = "";

            try
            {
                using (SqlCommand command = new SqlCommand(commandString, myConnection))
                {
                    output = Convert.ToString(command.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error on SQL query: " + e.ToString());
            }

            return output;
        }




        public string SelectRowsAll()
        {
            string commandString = "SELECT id, name FROM " + databasename + ".dbo.mytable";
            string output = "";
            try
            {
                using (SqlCommand command = new SqlCommand(commandString, myConnection))
                {
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        output += "Id: " + reader.GetValue(0).ToString() + ",Name: " + reader.GetValue(1).ToString() + ";";
                    }
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error on SQL query: " + e.ToString());
            }
            return output;
        }


        public int InsertRow(string name) // Returns number of rows affected.
        {
            string commandString = "INSERT INTO " + databasename + ".dbo.mytable(name) VALUES('" + name + "')";
            int output = 0;
            try
            {
                using (SqlCommand command = new SqlCommand(commandString, myConnection))
                {
                    output = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error on SQL query: " + e.ToString());
            }

            return output;
        }

    }
}