using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class MainMenuWindow : Window
    {
        [SerializeField] private ScheduleWindow scheduleWindow;
        [SerializeField] private NewsWindow newsWindow;
        [SerializeField] private StartMenuWindows startMenuWindows;
        [SerializeField] private UserWindow userWindow;

        [SerializeField] private Button scheduleButton;
        [SerializeField] private Button newsButton;
        [SerializeField] private Button userButton;
        [SerializeField] private Button backButton;
        
        protected override void OnShow()
        {
            scheduleButton.onClick.AddListener(ShowScheduleWindow);
            newsButton.onClick.AddListener(ShowNewsWindow);
            userButton.onClick.AddListener(ShowUserWindow);
            backButton.onClick.AddListener(ShowStartWindow);
        }

        protected override void OnHide()
        {
            scheduleButton.onClick.RemoveListener(ShowScheduleWindow);
            newsButton.onClick.RemoveListener(ShowNewsWindow);
            userButton.onClick.RemoveListener(ShowUserWindow);
            backButton.onClick.RemoveListener(ShowStartWindow);
        }

        private void ShowStartWindow()
        {
            startMenuWindows.Show();
        }

        private void ShowUserWindow()
        {
            userWindow.Show();
        }

        private void ShowNewsWindow()
        {
            newsWindow.Show();
        }

        private void ShowScheduleWindow()
        {
            scheduleWindow.Show();
        }
    }
}