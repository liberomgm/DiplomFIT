using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using DataBase;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Core
{
    public class DataBaseConnector
    {
        private const string DBUsers = "db_users";
        
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
        
        public bool LoginUserDB(string name, string pass, out User user)
        {
            var selectUser = new SelectConstructor();
            user = null;

            var selectParameters = new QueryParametersCollection
            {
                {
                    "@firstName", name, DbType.String
                },
                {
                    "@password", pass, DbType.String
                }
            };

            selectUser.From(DBUsers).Columns("id, firstName, lastName, fatherName, password, phoneNumber, birthday").Where("firstName=@firstName AND password=@password");

            var row = FetchOneRow(selectUser.SelectCommand, selectParameters);

            if (row is { Count: 5 })
            {
                try
                {
                    user = new User
                    {
                        FirstName = (string)row["FirstName"],
                        LastName = (string)row["LastName"],
                        FatherName = (string)row["FatherName"],
                        Id = (long)row["Id"],
                        Password = (string)row["Password"],
                        PhoneNumber = (float)row["PhoneNumber"],
                        Birthday = (string)row["Birthday"]
                    };

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
            return false;
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

        private Dictionary<string, object> FetchOneRow(string query, IEnumerable parameters)
        {
            var rowItem = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            try
            {
                SqliteDataReader reader;
                using (var command = sqliteConnection.CreateCommand())
                {
                    command.CommandText = query;
                    foreach (QueryParameter paremeter in parameters)
                    {
                        command.Parameters.Add(
                                paremeter.ColumnName.StartsWith("@")
                                    ? paremeter.ColumnName
                                    : "@" + paremeter.ColumnName,
                                paremeter.DbType).Value =
                            Convert.IsDBNull(paremeter.Value) ? Convert.DBNull : paremeter.Value;
                    }

                    reader = command.ExecuteReader();
                }

                if (reader.HasRows)
                {
                    reader.Read();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        rowItem.Add(reader.GetName(i), reader[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return null;
            }

            return rowItem;
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