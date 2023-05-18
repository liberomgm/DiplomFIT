using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using DataBase;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Core
{
    public class DataBaseConnector
    {
        private const string DBUsers = "db_users";
        private const string DBCoach = "db_coachs";
        private const string DBSport = "db_sports";

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

        public bool CreateUserDB(
            string firstName,
            string lastName,
            string fatherName,
            int sportId,
            string login,
            string password,
            string sum)
        {
            if (sqliteConnection != null)
            {
                var selectUser = new SelectConstructor();
                var selectParameters = new QueryParametersCollection
                {
                    {
                        "@login", login, DbType.String
                    },
                    {
                        "@password", password, DbType.String
                    }
                };

                var i = (long)ExecuteScalar(
                    selectUser.Columns("count(*)").From(DBUsers).Where("login=@login AND password=@password")
                        .SelectCommand, selectParameters);

                if (i == 0)
                {
                    var insertParameters = new QueryParametersCollection
                    {
                        {
                            "login", login, DbType.String
                        },
                        {
                            "password", password, DbType.String
                        },
                        {
                            "firstName", firstName, DbType.String
                        },
                        {
                            "lastName", lastName, DbType.String
                        },
                        {
                            "fatherName", fatherName, DbType.String
                        },
                        {
                            "sum", sum, DbType.String
                        },
                        {
                            "sportId", sportId, DbType.String
                        }
                    };

                    var result = Insert(DBCoach, insertParameters);
                    return result > 0;
                }
            }

            return false;
        }

        public bool CreateUserDB(
            string firstName,
            string lastName,
            string fatherName,
            DateTime birthday,
            string login,
            string password,
            string phoneNumber)
        {
            if (sqliteConnection != null)
            {
                var selectUser = new SelectConstructor();
                var selectParameters = new QueryParametersCollection
                {
                    {
                        "@login", login, DbType.String
                    },
                    {
                        "@password", password, DbType.String
                    }
                };

                var i = (long)ExecuteScalar(
                    selectUser.Columns("count(*)").From(DBUsers).Where("login=@login AND password=@password")
                        .SelectCommand, selectParameters);

                if (i == 0)
                {
                    var insertParameters = new QueryParametersCollection
                    {
                        {
                            "login", login, DbType.String
                        },
                        {
                            "password", password, DbType.String
                        },
                        {
                            "firstName", firstName, DbType.String
                        },
                        {
                            "lastName", lastName, DbType.String
                        },
                        {
                            "fatherName", fatherName, DbType.String
                        },
                        {
                            "birthday", birthday, DbType.Date
                        },
                        {
                            "phoneNumber", phoneNumber, DbType.String
                        }
                    };

                    var result = Insert(DBUsers, insertParameters);
                    return result > 0;
                }
            }

            return false;
        }


        public bool LoginUserDB(string login, string password, out User user)
        {
            var selectUser = new SelectConstructor();
            user = null;

            var selectParameters = new QueryParametersCollection
            {
                {
                    "@login", login, DbType.String
                },
                {
                    "@password", password, DbType.String
                }
            };

            selectUser.From(DBUsers)
                .Columns("id, firstName, lastName, fatherName, login, password, phoneNumber, birthday")
                .Where("login=@login AND password=@password");

            var row = FetchOneRow(selectUser.SelectCommand, selectParameters);

            if (row is { Count: 8 })
            {
                try
                {
                    user = new User
                    {
                        FirstName = (string)row["FirstName"],
                        LastName = (string)row["LastName"],
                        FatherName = (string)row["FatherName"],
                        Id = (long)row["Id"],
                        Login = (string)row["Login"],
                        Password = (string)row["Password"],
                        PhoneNumber = (string)row["PhoneNumber"],
                        Birthday = (DateTime)row["Birthday"]
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
        
        public bool LoginCoachDB(string login, string password, out Coach coach)
        {
            var selectUser = new SelectConstructor();
            coach = null;

            var selectParameters = new QueryParametersCollection
            {
                {
                    "@login", login, DbType.String
                },
                {
                    "@password", password, DbType.String
                }
            };

            selectUser.From(DBCoach)
                .Columns("id, firstName, lastName, fatherName, login, password, sum, sportId")
                .Where("login=@login AND password=@password");

            var row = FetchOneRow(selectUser.SelectCommand, selectParameters);

            if (row is { Count: 8 })
            {
                try
                {
                    coach = new Coach()
                    {
                        FirstName = (string)row["FirstName"],
                        LastName = (string)row["LastName"],
                        FatherName = (string)row["FatherName"],
                        Id = (long)row["Id"],
                        Login = (string)row["Login"],
                        Password = (string)row["Password"],
                        Sum = (string)row["Sum"],
                        SportId = (long)row["SportId"]
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

        public IEnumerable<Sport> GetAllSport()
        {
            var sports = new List<Sport>();
            var selectName = new SelectConstructor();

            var selectParameters = new QueryParametersCollection();
            selectName.From(DBSport).Columns("id, title");
            var data = ExecuteSql(selectName, selectParameters);

            foreach (DataRow row in data.Rows)
            {
                sports.Add(new Sport()
                {
                    Id = (long)row["id"],
                    Title = (string)row["title"]
                   
                });
            }

            return sports;
        }

        public bool AddSport(string title)
        {
            if (sqliteConnection != null)
            {
                var selectUser = new SelectConstructor();
                var selectParameters = new QueryParametersCollection
                {
                    {
                        "@title", title, DbType.String
                    }
                };

                var i = (long)ExecuteScalar(
                    selectUser.Columns("count(*)").From(DBSport).Where("title=@title")
                        .SelectCommand, selectParameters);

                if (i == 0)
                {
                    var insertParameters = new QueryParametersCollection
                    {
                        {
                            "title", title, DbType.String
                        }
                    };

                    var result = Insert(DBSport, insertParameters);
                    return result > 0;
                }
            }

            return false;
        }

        private DataTable ExecuteSql(SelectConstructor select, IEnumerable parameters)
        {
            var result = new DataTable();
            if (sqliteConnection is { State: ConnectionState.Open })
            {
                try
                {
                    using var sqliteCommand = sqliteConnection.CreateCommand();

                    sqliteCommand.CommandText = @select.SelectCommand;
                    if (parameters != null)
                    {
                        foreach (QueryParameter parameter in parameters)
                        {
                            sqliteCommand.Parameters.Add(
                                parameter.ColumnName.StartsWith("@")
                                    ? parameter.ColumnName
                                    : "@" + parameter.ColumnName,
                                parameter.DbType).Value = Convert.IsDBNull(parameter.Value)
                                ? Convert.DBNull
                                : parameter.Value;
                        }
                    }

                    using var sqliteDataReader = sqliteCommand.ExecuteReader();
                    result.Load(sqliteDataReader);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }

            return result;
        }

        private long Insert(string tableName, IEnumerable parameters)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                return 0;
            }

            long lastId;

            try
            {
                using (var command = sqliteConnection.CreateCommand())
                {
                    var ifFirst = true;
                    var queryColumns = new StringBuilder("(");
                    var queryValues = new StringBuilder("(");
                    foreach (QueryParameter parameter in parameters)
                    {
                        command.Parameters.Add("@" + parameter.ColumnName, parameter.DbType).Value =
                            Convert.IsDBNull(parameter.Value) ? Convert.DBNull : parameter.Value;

                        if (ifFirst)
                        {
                            queryColumns.Append(parameter.ColumnName);
                            queryValues.Append("@" + parameter.ColumnName);
                            ifFirst = false;
                        }
                        else
                        {
                            queryColumns.Append("," + parameter.ColumnName);
                            queryValues.Append(",@" + parameter.ColumnName);
                        }
                    }

                    queryColumns.Append(")");
                    queryValues.Append(")");

                    var sql = $"INSERT INTO {tableName} {queryColumns} VALUES {queryValues}";

                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }

                lastId = long.Parse(ExecuteScalar("SELECT last_insert_rowid()", null).ToString());
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return 0;
            }

            return lastId;
        }

        private void CreateDataBase()
        {
            ExecuteNonQuery(
                "CREATE TABLE IF NOT EXISTS [db_users] ( " +
                "[id] INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                "[firstName] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[lastName] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[fatherName] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[login] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[password] VARCHAR(255)  NOT NULL, " +
                "[phoneNumber] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[birthday] DATETIME NOT NULL);");

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
                "[login] VARCHAR(255)  UNIQUE NOT NULL, " +
                "[password] VARCHAR(255)  NOT NULL, " +
                "[sum] VARCHAR(255) NOT NULL, " +
                "[sportId] INTEGER  NOT NULL REFERENCES db_sports (id) ON DELETE CASCADE ON UPDATE CASCADE);");

            ExecuteNonQuery(
                "CREATE TABLE IF NOT EXISTS [db_sports] ( " +
                "[id] INTEGER  PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                "[title] VARCHAR(255)  UNIQUE NOT NULL);");
        }

        private object ExecuteScalar(string query, IEnumerable parameters)
        {
            object result = null;
            if (sqliteConnection is { State: ConnectionState.Open })
            {
                try
                {
                    using var sqliteCommand = sqliteConnection.CreateCommand();

                    sqliteCommand.CommandText = query;

                    if (parameters != null)
                    {
                        foreach (QueryParameter parameter in parameters)
                        {
                            sqliteCommand.Parameters.Add(
                                parameter.ColumnName.StartsWith("@")
                                    ? parameter.ColumnName
                                    : "@" + parameter.ColumnName,
                                parameter.DbType).Value = Convert.IsDBNull(parameter.Value)
                                ? Convert.DBNull
                                : parameter.Value;
                        }
                    }

                    result = sqliteCommand.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }

            return result;
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