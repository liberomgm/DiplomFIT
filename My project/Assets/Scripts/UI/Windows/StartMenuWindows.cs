using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class StartMenuWindows : Window
    {
        [SerializeField]
        private Button registrationButton;
        
        [SerializeField]
        private Button authorizationButton;
        
        [SerializeField]
        private Button authorizationCoachButton;

        [SerializeField] private RegistrationUserWindow registrationUserWindow;
        [SerializeField] private RegistrationCoachWindow registrationCoachWindow;
        [SerializeField] private AuthorizationWindow authorizationWindow;
        
        protected override void OnShow()
        {
            registrationButton.onClick.AddListener(ShowRegistrationWindow);
            authorizationButton.onClick.AddListener(ShowAuthorizationWindow);
            authorizationCoachButton.onClick.AddListener(ShowAuthorizationCoachWindow);

            UserStorage.CurrentCoach = null;
            UserStorage.CurrentUser = null;
        }

        protected override void OnHide()
        {
            registrationButton.onClick.RemoveListener(ShowRegistrationWindow);
            authorizationButton.onClick.RemoveListener(ShowAuthorizationWindow);
            authorizationCoachButton.onClick.RemoveListener(ShowAuthorizationCoachWindow);
        }

        private void ShowAuthorizationCoachWindow()
        {
            registrationCoachWindow.Show();
        }

        private void ShowRegistrationWindow()
        {
            registrationUserWindow.Show();
        }

        private void ShowAuthorizationWindow()
        {
            authorizationWindow.Show();
        }
    }
}