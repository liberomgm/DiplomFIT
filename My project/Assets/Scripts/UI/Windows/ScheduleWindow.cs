using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.UI.Calendar;
using DefaultNamespace;
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
        [SerializeField] private ListWorkoutRecords listWorkoutRecords;
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

            var workoutRecord = DatabaseProvider.GetWorkoutRecords(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if(workoutRecord.Count() > 0 )
            {
                foreach (var workout in workoutRecord)
                {
                    listWorkoutRecords.AddRecord(workout, DatabaseProvider);
                }
            }
        }

        private void ShowCreateEntryPanel()
        {
            var coachName = new List<string>();
            var coaches = DatabaseProvider.GetCoaches();

            foreach (var coach in coaches)
            {
                coachName.Add( $"{coach.LastName} {coach.FirstName[0]}. {coach.FatherName}.");
            }
            
            createEntryPanel.Show(DateTime.Now.Year, DateTime.Now.Month, selectDay, coachName, UserStorage.CurrentUser.Id, DatabaseProvider);
        }

        protected override void OnHide()
        {
            calendarView.Clear();
            createEntryButton.gameObject.SetActive(false);
            createEntryButton.onClick.RemoveListener(ShowCreateEntryPanel);
        }
    }
}