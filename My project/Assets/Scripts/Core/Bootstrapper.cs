using System;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Window[] windows;
        [SerializeField] private Window displayedWindow;

        private DatabaseProvider databaseProvider;
        
        private void Awake()
        {
            databaseProvider = new DatabaseProvider();
            databaseProvider.Connecting();

            foreach (var window in windows)
            {
                window.Initialize(databaseProvider);
                window.Hide();
            }

            databaseProvider.AddSport("Волебол");
            databaseProvider.AddSport("Футбол");
            databaseProvider.AddSport("Баскетбол");
            
            displayedWindow.Show();
        }

        private void OnApplicationQuit()
        {
            databaseProvider?.Disconnect();
        }
    }
}
