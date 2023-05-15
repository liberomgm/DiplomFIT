using UnityEngine;

namespace Core
{
    public class DatabaseProvider : MonoBehaviour
    {
        private DataBaseConnector dataBaseConnector;
        private const string DataBaseFileName = "DiplomFIT.db";
        
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
    }
}