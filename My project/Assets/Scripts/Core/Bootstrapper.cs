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
        
        private void Awake()
        {
            foreach (var window in windows)
            {
                window.Hide();
            }

            displayedWindow.Show();
        }
    }
}
