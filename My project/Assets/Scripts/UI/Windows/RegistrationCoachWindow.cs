using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class RegistrationCoachWindow : Window
    {
        [SerializeField] private StartMenuWindows startMenuWindows;
        [SerializeField] private AuthorizationWindow authorizationWindow;

        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private TMP_InputField loginField;
        [SerializeField] private TMP_InputField passwordField;
        [SerializeField] private TMP_InputField sumField;
        [SerializeField] private TMP_Dropdown sportsDropdown;

        [SerializeField] private Button registrationButton;
        [SerializeField] private Button backButton;

        private string name;
        private string login;
        private string password;
        private string sum;
        private int sport;

        protected override void OnShow()
        {
            nameField.onValueChanged.AddListener(SetName);
            loginField.onValueChanged.AddListener(SetLogin);
            passwordField.onValueChanged.AddListener(SetPassword);
            sumField.onValueChanged.AddListener(SetSum);
            sportsDropdown.onValueChanged.AddListener(SetSport);

            registrationButton.onClick.AddListener(Registration);
            backButton.onClick.AddListener(ShowStartWindow);

            SportInitialize();
        }

        private void SportInitialize()
        {
            var sports = DatabaseProvider.WriteAllSports();

            var titles = new List<string>();

            foreach (var sport in sports)
            {
                titles.Add(sport.Title);
            }
            
            sportsDropdown.AddOptions(titles);
        }
        
        private void ShowStartWindow()
        {
            startMenuWindows.Show();
        }

        private void Registration()
        {
            if (DatabaseProvider.CreateCoach(name, name, name, sport, login, password, sum))
            {
                authorizationWindow.Show();
                Debug.Log($"Тренер {name} создан");
            }
            else
            {
                Debug.Log($"Тренер уже создан");
            }
        }

        protected override void OnHide()
        {
            nameField.onValueChanged.RemoveListener(SetName);
            loginField.onValueChanged.RemoveListener(SetLogin);
            passwordField.onValueChanged.RemoveListener(SetPassword);
            sumField.onValueChanged.RemoveListener(SetSum);
            sportsDropdown.onValueChanged.RemoveListener(SetSport);

            registrationButton.onClick.RemoveListener(Registration);
            backButton.onClick.RemoveListener(ShowStartWindow);
            
            sportsDropdown.ClearOptions();
        }

        private void SetPassword(string password)
        {
            this.password = password;
        }

        private void SetSport(int sport)
        {
            this.sport = sport;
        }

        private void SetSum(string sum)
        {
            this.sum = sum;
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