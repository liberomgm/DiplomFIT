using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class DatabaseProvider : MonoBehaviour
    {
        private DataBaseConnector dataBaseConnector;
        private const string DataBaseFileName = "DiplomFIT.bytes";
        
        public void Connecting()
        {
            dataBaseConnector = new DataBaseConnector();
            if (dataBaseConnector.Connection(DataBaseFileName))
            {
                Debug.Log($"Подключение к базе данных статистики завершена успешно.");
            }
            else
            {
                Debug.LogError($"Подключение к базе данных статистики не завершено.");
            }
        }
        
        public void Disconnect()
        {
            dataBaseConnector?.CloseDataBase();
            dataBaseConnector = null;
        }
        
        public bool TryLogin(string name, string password, out User user)
        {
            return dataBaseConnector.LoginUserDB(name, password, out user);
        }
        
        public bool TryLogin(string name, string password, out Coach user)
        {
            return dataBaseConnector.LoginCoachDB(name, password, out user);
        }

        public User GetUser(long id)
        {
            return dataBaseConnector.GetUser(id);
        }

        public Coach GetCoach(long id)
        {
            return dataBaseConnector.GetCoach(id);
        }

        public bool CreateUser(string name,string lastName,string fatherName, DateTime birthday, string login, string password, string phoneNumber)
        {                          
            return dataBaseConnector.CreateUserDB(name, lastName, fatherName, birthday, login, password, phoneNumber);
        }

        public bool CreateCoach(string name,string lastName,string fatherName, int sportId, string login, string password, string sum)
        {
            return dataBaseConnector.CreateUserDB(name, lastName, fatherName, sportId, login, password, sum);
        }

        public IEnumerable<Sport> WriteAllSports()
        {
            return dataBaseConnector.GetAllSport();
        }

        public void AddSport(string title)
        {
            dataBaseConnector.AddSport(title);
        }

        public IEnumerable<Coach> GetCoaches()
        {
            return dataBaseConnector.GetCoaches();
        }

        public bool GetCoach(long coachId, out Coach coach)
        {
            return dataBaseConnector.LoginCoachDB(coachId, out coach);
        }

        public void AddWorkoutRecord(long userId, long coachId, DateTime dateTime, string coachSum)
        {
            dataBaseConnector.AddWorkoutRecord((int)userId, (int)coachId, dateTime, int.Parse(coachSum));
        }

        public IEnumerable<WorkoutRecord> GetWorkoutRecords(int year, int month, int day)
        {
            IEnumerable<WorkoutRecord> workoutRecords = dataBaseConnector.GetWorkoutRecords();


            var result = new List<WorkoutRecord>();

            foreach (WorkoutRecord record in workoutRecords)
            {
                if(record.WorkoutTime.Month == month && record.WorkoutTime.Day == day)
                    result.Add(record);
            }

            return result;
        }
    }
}
