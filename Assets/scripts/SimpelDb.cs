using UnityEngine;
using Mono.Data.Sqlite;
using System.Collections;
using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;

public class SimpelDb : MonoBehaviour
{
    string fileName = "mydatabase.db";
    static string dbname;
    void Start()
    {

        dbname = Application.persistentDataPath + "/" + fileName;
        creatdB();
    }

    void creatdB()
    {
        using (var connection = new SqliteConnection(dbname))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS highscores (npa INTEGER NOT NULL,TotalCoin INTEGER NOT NULL," +
                    "score INTEGER NOT NULL,gamestart INTEGER NOT NULL,SaveDataShop LONGTEXT,SaveMapDataShop LONGTEXT,Sound INTEGER NOT NULL,Music INTEGER NOT NULL);";
                command.ExecuteNonQuery();
                if(read("score") == "")
                {
                    command.CommandText = "INSERT INTO highscores VALUES (1,0,0,0,'','',1,1);";
                    command.ExecuteNonQuery();
                }

            }
            connection.Close();
        }
    }

    public static void update(string heigscore,string write_into_table)
    {
        using (var connection = new SqliteConnection(dbname))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"UPDATE highscores SET " + write_into_table + " = $heigscore;";
                command.Parameters.AddWithValue("$heigscore", heigscore);
                command.ExecuteNonQuery();
            }
           
            connection.Close();
        }
    }

    public static string read(string read_from_table)
    {
        string rd = null;
        using (var connection = new SqliteConnection(dbname))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"SELECT " + read_from_table + " FROM highscores;";

                using(IDataReader reader = command.ExecuteReader())
                {
                    rd = reader[read_from_table].ToString();
                    reader.Close();
                }
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        return (rd);
    }
  
}