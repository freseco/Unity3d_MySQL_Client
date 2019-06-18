using MySql.Data.MySqlClient;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class MySQLscript : MonoBehaviour
{

    public Text text;

    public string Server = "";
    public string DataBase = "";
    public string UserID = "";
    public string Password = "";
    public bool Pooling = false;


    void Start()
    {
        #region Connection to MySQL
        string connectionString =
          "Server=" + Server + ";" +
          "Database=" + DataBase + ";" +
          "User ID=" + UserID + ";" +
          "Password=" + Password + ";" +
          "Pooling=" + Pooling.ToString();


        IDbConnection dbcon;
        dbcon = new MySqlConnection(connectionString);
        dbcon.Open(); 
        #endregion



        IDbCommand dbcmd = dbcon.CreateCommand();
        // requires a table to be created named employee
        // with columns firstname and lastname
        // such as,
        //        CREATE TABLE employee (
        //           firstname varchar(32),
        //           lastname varchar(32));
        string sql =
            "SELECT firstname, lastname " +
            "FROM employee";
        dbcmd.CommandText = sql;

        IDataReader reader = dbcmd.ExecuteReader();
        while (reader.Read())
        {
            string FirstName = (string)reader["firstname"];
            string LastName = (string)reader["lastname"];
            text.text="Name: " +  FirstName + " " + LastName;
        }


        #region clean up
            reader.Close();
            reader = null;

            dbcmd.Dispose();
            dbcmd = null;

            dbcon.Close();
            dbcon = null;
        #endregion

    }
}
