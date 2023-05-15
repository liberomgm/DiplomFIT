using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Core
{
    public class DataBaseConnector
    {
        private SqliteConnection sqliteConnection;

        public bool Connection(string dataBaseFileName)
        {
            try
            {
                var connection = Path.Combine("URI=file:", dataBaseFileName);

                sqliteConnection?.Close();
                sqliteConnection = new SqliteConnection(connection);

                sqliteConnection.Open();

                if (sqliteConnection.State != ConnectionState.Open)
                {
                    sqliteConnection = null;
                    throw new InvalidOperationException("Подключение сорвано. Поток занят");
                }

                CreateDataBase();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void CloseDataBase()
        {
            sqliteConnection?.Close();
            sqliteConnection = null;
        }
        
        private void CreateDataBase()
        {
            ExecuteNonQuery(
                "CREATE TABLE IF NOT EXISTS [db_users] ( " +
                "[id] INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                "[firstName] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[lastName] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[fatherName] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[password] VARCHAR(255)  NOT NULL, " +
                "[phoneNumber] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[birthday] VARCHAR(255)  NOT NULL);");
            
            ExecuteNonQuery(
                "CREATE TABLE IF NOT EXISTS [db_workouts] ( " +
                "[id] INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL , " +
                "[u_id] INTEGER  NOT NULL REFERENCES db_users (id) ON DELETE CASCADE ON UPDATE CASCADE, " +
                "[c_id] INTEGER  NOT NULL REFERENCES db_coachs (id) ON DELETE CASCADE ON UPDATE CASCADE, " +
                "[workoutDate] DATETIME NOT NULL, " +
                "[workoutTime] DATETIME NOT NULL, " +
                "[cost] REAL NOT NULL);");
            
            ExecuteNonQuery(
                "CREATE TABLE IF NOT EXISTS [db_coachs] ( " +
                "[id] INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                "[firstName] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[lastName] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[fatherName] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[sum] VARCHAR(255) NOT NULL, " +
                "[sportId] INTEGER  NOT NULL REFERENCES db_sports (id) ON DELETE CASCADE ON UPDATE CASCADE);");
            
            ExecuteNonQuery(
                "CREATE TABLE IF NOT EXISTS [db_sports] ( " +
                "[firstName] VARCHAR(255)  UNIQUE NOT NULL);");
        }

        private int ExecuteNonQuery(string query)
        {
            var result = 0;
            if (sqliteConnection is { State: ConnectionState.Open })
            {
                try
                {
                    using var sqliteCommand = sqliteConnection.CreateCommand();

                    sqliteCommand.CommandText = query;
                    result = sqliteCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }

            return result;
        }
    }
}