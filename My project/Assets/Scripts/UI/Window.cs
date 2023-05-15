using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace UI
{
    public abstract class Window : MonoBehaviour
    {
        private static List<Window> windows = new List<Window>();
        protected DatabaseProvider DatabaseProvider;
        
        public void Show()
        {
            ShowAllWindows();
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide();
        }

        private void ShowAllWindows()
        {
            foreach (var window in windows)
            {
                window.Hide();
            }
        }
        
        private void Awake()
        {
            windows.Add(this);
        }

        protected abstract void OnShow();
        protected abstract void OnHide();

        public void Initialize(DatabaseProvider databaseProvider)
        {
            DatabaseProvider = databaseProvider;
        }
    }
}