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
        StartCoroutine(RunDbCode());
        creatdB();
    }
    IEnumerator RunDbCode()
    {
        //Where to copy the db to
        string dbDestination = Path.Combine(Application.persistentDataPath, "data");
        dbDestination = Path.Combine(dbDestination, fileName);

        //Check if the File do not exist then copy it
        if (!File.Exists(dbDestination))
        {
            //Where the db file is at
            string dbStreamingAsset = Path.Combine(Application.streamingAssetsPath, fileName);

            byte[] result;

            //Read the File from streamingAssets. Use WWW for Android
            if (dbStreamingAsset.Contains("://") || dbStreamingAsset.Contains(":///"))
            {
                WWW www = new WWW(dbStreamingAsset);
                yield return www;
                result = www.bytes;
            }
            else
            {
                result = File.ReadAllBytes(dbStreamingAsset);
            }
            Debug.Log("Loaded db file");

            //Create Directory if it does not exist
            if (!Directory.Exists(Path.GetDirectoryName(dbDestination)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(dbDestination));
            }

            //Copy the data to the persistentDataPath where the database API can freely access the file
            File.WriteAllBytes(dbDestination, result);
            //Debug.Log("Copied db file");
        }

        try
        {
            //Tell the db final location for debugging
            //Debug.Log("DB Path: " + dbDestination.Replace("/", "\\"));
            //Add "URI=file:" to the front of the url beore using it with the Sqlite API
            dbDestination = "URI=file:" + dbDestination;

            dbname = dbDestination;
            //Debug.Log("Success!");
        }
        catch (Exception e)
        {
            Debug.Log("Failed: " + e.Message);
        }
    }


    void creatdB()
    {
        using (var connection = new SqliteConnection(dbname))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS Data (npa INTEGER NOT NULL,TotalCoin INTEGER NOT NULL," +
                    "score INTEGER NOT NULL,gamestart INTEGER NOT NULL,SaveDataShop LONGTEXT,SaveTrailDataShop LONGTEXT," +
                    "Sound INTEGER NOT NULL,Music INTEGER NOT NULL,level INTEGER NOT NULL);";
                command.ExecuteNonQuery();
                if(read("score") == "")
                {
                    command.CommandText = "INSERT INTO Data VALUES (1,10,0,0,'','',0,0,1);";
                    command.ExecuteNonQuery();
                }

            }
            connection.Close();
        }
    }

    public static void update(string value,string write_into_table)
    {
        using (var connection = new SqliteConnection(dbname))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"UPDATE Data SET " + write_into_table + " = $value;";
                command.Parameters.AddWithValue("$value", value);
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
                command.CommandText = @"SELECT " + read_from_table + " FROM Data;";

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