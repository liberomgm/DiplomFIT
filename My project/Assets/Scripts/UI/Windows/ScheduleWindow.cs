﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using Assets.Scripts.UI.Calendar;
using DefaultNamespace;
using UI.Calendar;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ScheduleWindow : Window
    {
        [SerializeField]
        private MainMenuWindow mainMenuWindow;

        [SerializeField] private CalendarView calendarView;
        [SerializeField] private Button createEntryButton;
        [SerializeField] private Button backButton;
        [SerializeField] private CreateEntryPanel createEntryPanel;
        [SerializeField] private ListWorkoutRecords listWorkoutRecords;
        private int selectDay;

        protected override void OnShow()
        {
            calendarView.FillingOut(DateTime.Now.Month, SelectDay);
            backButton.onClick.AddListener(ShowMainMenuWindow);
            createEntryButton.gameObject.SetActive(false);
            createEntryPanel.Hide();
        }

        private void SelectDay(int day)
        {
            selectDay = day;

            listWorkoutRecords.Clear();
            createEntryPanel.Hide();

            var workoutRecord = DatabaseProvider.GetWorkoutRecords(DateTime.Now.Year, DateTime.Now.Month, day);

            if(workoutRecord.Count() > 0 )
            {
                createEntryButton.gameObject.SetActive(false);

                foreach (var workout in workoutRecord)
                {
                    listWorkoutRecords.AddRecord(workout, DatabaseProvider);
                }
            }
            else
            {
                createEntryButton.gameObject.SetActive(true);
                createEntryButton.onClick.AddListener(ShowCreateEntryPanel);
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
            backButton.onClick.RemoveListener(ShowMainMenuWindow);
            listWorkoutRecords.Clear();
        }

        private void ShowMainMenuWindow()
        {
            mainMenuWindow.Show();
        }
    }
}