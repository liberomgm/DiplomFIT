using System;
using UI.Calendar;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ScheduleWindow : Window
    {
        [SerializeField] private CalendarView calendarView;
        [SerializeField] private Button createEntryButton;
        [SerializeField] private CreateEntryPanel createEntryPanel;
        private int selectDay;

        protected override void OnShow()
        {
            calendarView.FillingOut(DateTime.Now.Month, SelectDay);
            createEntryButton.gameObject.SetActive(false);
            createEntryPanel.Hide();
        }

        private void SelectDay(int day)
        {
            selectDay = day;
            createEntryButton.gameObject.SetActive(true);
            
            createEntryButton.onClick.AddListener(ShowCreateEntryPanel);
        }

        private void ShowCreateEntryPanel()
        {
            createEntryPanel.Show(DateTime.Now.Year, DateTime.Now.Month, selectDay);
        }

        protected override void OnHide()
        {
            calendarView.Clear();
            createEntryButton.gameObject.SetActive(false);
            createEntryButton.onClick.RemoveListener(ShowCreateEntryPanel);
        }
    }
}