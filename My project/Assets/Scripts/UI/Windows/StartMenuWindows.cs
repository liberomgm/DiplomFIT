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

        [SerializeField] private RegistrationWindow registrationWindow;
        [SerializeField] private AuthorizationWindow authorizationWindow;
        
        protected override void OnShow()
        {
            registrationButton.onClick.AddListener(ShowRegistrationWindow);
            authorizationButton.onClick.AddListener(ShowAuthorizationWindow);
        }

        protected override void OnHide()
        {
            registrationButton.onClick.RemoveListener(ShowRegistrationWindow);
            authorizationButton.onClick.RemoveListener(ShowAuthorizationWindow);
        }

        private void ShowRegistrationWindow()
        {
            registrationWindow.Show();
        }

        private void ShowAuthorizationWindow()
        {
            authorizationWindow.Show();
        }
    }
}