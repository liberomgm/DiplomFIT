using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class AuthorizationWindow : Window
    {
        [SerializeField]
        private Button loginButton;
        
        [SerializeField]
        private Button backButton;
        

        [SerializeField] private TMP_InputField loginField;
        [SerializeField] private TMP_InputField passwordField;

        [SerializeField] private MainMenuWindow mainMenuWindow;
        [SerializeField] private StartMenuWindows startMenuWindows;

        private string login;
        private string password;
        
        protected override void OnShow()
        {
            loginButton.onClick.AddListener(LoginAttempt);
            backButton.onClick.AddListener(ShowStartMenu);
            
            loginField.onValueChanged.AddListener(SetLogin);
            passwordField.onValueChanged.AddListener(SetPassword);
        }

        private void SetLogin(string login)
        {
            this.login = login;
        }

        private void SetPassword(string password)
        {
            this.password = password;
        }

        protected override void OnHide()
        {
            loginButton.onClick.RemoveListener(LoginAttempt);
            backButton.onClick.RemoveListener(ShowStartMenu);
        }

        private void ShowStartMenu()
        {
            startMenuWindows.Show();
        }

        private void LoginAttempt()
        {
            if (DatabaseProvider.TryLogin(login, password, out var user))
            {
                mainMenuWindow.Show();
            }
            else
            {
                Debug.Log("Пользователь не найден");
            }
        }
    }
}