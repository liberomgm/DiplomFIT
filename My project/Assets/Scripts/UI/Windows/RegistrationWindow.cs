using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class RegistrationWindow : Window
    {
        [SerializeField] private StartMenuWindows startMenuWindows;
        [SerializeField] private AuthorizationWindow authorizationWindow;
        
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private TMP_InputField loginField;
        [SerializeField] private TMP_InputField passwordField;

        [SerializeField] private Button registrationButton;
        [SerializeField] private Button backButton;

        private string name;
        private string login;
        private string password;
        
        protected override void OnShow()
        {
            nameField.onValueChanged.AddListener(SetName);
            loginField.onValueChanged.AddListener(SetLogin);
            passwordField.onValueChanged.AddListener(SetPassword);
            registrationButton.onClick.AddListener(Registration);
            backButton.onClick.AddListener(ShowStartWindow);
        }

        private void ShowStartWindow()
        {
            startMenuWindows.Show();
        }

        private void Registration()
        {
            if (DatabaseProvider.CreateUser(name, name, name, DateTime.Now, login, password, "+00000000000"))
            {
                authorizationWindow.Show();
                Debug.Log($"Пользователь {name} создан");
            }
            else
            {
                Debug.Log($"Пользователь уже создан");
            }
        }

        protected override void OnHide()
        {
            nameField.onValueChanged.RemoveListener(SetName);
            loginField.onValueChanged.RemoveListener(SetLogin);
            passwordField.onValueChanged.RemoveListener(SetPassword);
            registrationButton.onClick.RemoveListener(Registration);
            backButton.onClick.RemoveListener(ShowStartWindow);
        }

        private void SetPassword(string password)
        {
            this.password = password;
        }

        private void SetLogin(string login)
        {
            this.login = login;
        }

        private void SetName(string name)
        {
            this.name = name;
        }
    }
}