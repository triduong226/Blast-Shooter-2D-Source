using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data; 

public class SqliteController : MonoBehaviour
{
    
    private string un, pw, em;
    private int id, bScore;
    public static List<Account> accounts = new List<Account>();
    public static SqliteController instance;
    void Awake(){
        MakeInstace();
    }

    void MakeInstace(){
        if(instance == null){
            instance = this;
        }
    }

    public void Start() // 13
    {
        // Read all values from the table.
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 14
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 15
        dbCommandReadValues.CommandText = "SELECT * FROM ACCOUNT"; // 16
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); // 17
        GetDatabase(dataReader);
        
        
        // Remember to always close the connection at the end.
        dbConnection.Close(); // 20
    }
    private void GetDatabase(IDataReader dataReader){
        accounts.Clear();
        while (dataReader.Read()) // 18
        {
            // The `id` has index 0, our `hits` have the index 1.
            id = dataReader.GetInt32(0);
            un = dataReader.GetString(1);
            pw = dataReader.GetString(2);
            em = dataReader.GetString(3);
            bScore = dataReader.GetInt32(4); // 19
            accounts.Add(new Account() { id = id, userName = un, passWord = pw, email = em, bestScore = bScore });
        }
    }

    public void Khoitao(){
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 14
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 15
        dbCommandReadValues.CommandText = "SELECT * FROM ACCOUNT"; // 16
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); // 17
        GetDatabase(dataReader);
        dbConnection.Close(); // 20
    }

    private void CreateBasicData()
    {
        // Insert hits into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 2
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand(); // 9
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO ACCOUNT (ID, USERNAME,PASSWORD, EMAIL, BESTSCORE) VALUES (0, 'triduong', '123', 'tri598110@gmail.com', 1)"; // 10
        dbCommandInsertValue.ExecuteNonQuery(); // 11
        // Remember to always close the connection at the end.
        dbConnection.Close(); // 12
    }
    public void CreateNewAccount(string un, string pw, string em){
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 2
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand(); // 9
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO ACCOUNT (ID, USERNAME, PASSWORD, EMAIL ,BESTSCORE) VALUES ("+accounts.Count.ToString()+", '"+un+"', '"+pw+"', '"+ em +"', 1)"; 
        dbCommandInsertValue.ExecuteNonQuery();
        dbConnection.Close();
        dbConnection = CreateAndOpenDatabase(); // 14
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 15
        dbCommandReadValues.CommandText = "SELECT * FROM ACCOUNT"; // 16
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); // 17
        GetDatabase(dataReader);
    }

    public void ResetPassword(int id, string un, string pw, string em, int bs){
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 2
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand(); // 9
        dbCommandInsertValue.CommandText = "REPLACE INTO ACCOUNT (ID, USERNAME, PASSWORD, EMAIL, BESTSCORE) VALUES ("+id+", '"+un+"', '"+pw+"', '"+ em +"', "+bs+")"; 
        dbCommandInsertValue.ExecuteNonQuery();
        dbConnection.Close();
        dbConnection = CreateAndOpenDatabase(); // 14
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 15
        dbCommandReadValues.CommandText = "SELECT * FROM ACCOUNT"; // 16
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); // 17
        GetDatabase(dataReader);
    }
    public void ChangePassword(string un, string pw){
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 2
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand(); // 9
        dbCommandInsertValue.CommandText = "UPDATE ACCOUNT SET PASSWORD = '"+ pw +"' WHERE USERNAME = '"+ un +"'"; 
        dbCommandInsertValue.ExecuteNonQuery();
        dbConnection.Close();
        dbConnection = CreateAndOpenDatabase(); // 14
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 15
        dbCommandReadValues.CommandText = "SELECT * FROM ACCOUNT"; // 16
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); // 17
        GetDatabase(dataReader);
    }
    public void UpdateScore(string un, int bs){
        IDbConnection dbConnection = CreateAndOpenDatabase(); // 2
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand(); // 9
        dbCommandInsertValue.CommandText = "UPDATE ACCOUNT SET BESTSCORE = "+bs+" WHERE USERNAME = '"+ un +"'"; 
        dbCommandInsertValue.ExecuteNonQuery();
        dbConnection.Close();
        dbConnection = CreateAndOpenDatabase(); // 14
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); // 15
        dbCommandReadValues.CommandText = "SELECT * FROM ACCOUNT"; // 16
        IDataReader dataReader = dbCommandReadValues.ExecuteReader(); // 17
        GetDatabase(dataReader);
    }
    private IDbConnection CreateAndOpenDatabase() // 3
    {
        // Open a connection to the database.
        string dbUri = "URI=file:MyDatabase.db"; // 4
        IDbConnection dbConnection = new SqliteConnection(dbUri); // 5
        dbConnection.Open(); // 6

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbcmd = dbConnection.CreateCommand(); // 6
        
        dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS Account (ID INTEGER PRIMARY KEY, USERNAME NVARCHAR(30), PASSWORD VARCHAR(30), EMAIL NVARCHAR(60),BESTSCORE INTEGER)"; // 7
        dbcmd.ExecuteReader(); // 8

        return dbConnection;
    }
}

