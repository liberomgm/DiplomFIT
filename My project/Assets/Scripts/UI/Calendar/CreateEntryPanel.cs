using System;
using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Calendar
{
    public class CreateEntryPanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text dateText;
        
        [SerializeField]
        private TMP_InputField hourTextField;
        
        [SerializeField]
        private TMP_InputField minutesTextField;
        
        [SerializeField]
        private TMP_Dropdown coachDropDown;

        [SerializeField] private Button createEntryButton;

        private DatabaseProvider databaseProvider;

        private int hour;
        private int minutes;
        private long coachId;
        private long userId;
        private int year;
        private int month;
        private int day;
        
        public void Show(int year, int month, int day, List<string> coach, long userId, DatabaseProvider databaseProvider)
        {
            this.gameObject.SetActive(true);
            dateText.text = $"{day}.{month}.{year}";

            this.year = year;
            this.month = month;
            this.day = day;
            this.userId = userId;
            this.databaseProvider = databaseProvider;
            
            coachDropDown.AddOptions(coach);
            hourTextField.onValueChanged.AddListener(SetHour);
            minutesTextField.onValueChanged.AddListener(SetMinutes);
            coachDropDown.onValueChanged.AddListener(SetCoach);
            createEntryButton.onClick.AddListener(CreateEntryButtonClick);
            
        }

        private void CreateEntryButtonClick()
        {
            databaseProvider.GetCoach(coachId + 1, out var coach);

            databaseProvider.AddWorkoutRecord(userId, coachId + 1, new DateTime(year, month, day, hour, minutes, 0),
                coach.Sum);
        }

        private void SetHour(string hour)
        {
            this.hour = int.Parse(hour);
        }

        private void SetMinutes(string minutes)
        {
            this.minutes = int.Parse(minutes);
        }
        
        private void SetCoach(int coach)
        {
            coachId = coach;
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
            
            coachDropDown.ClearOptions();
            hourTextField.onValueChanged.RemoveListener(SetHour);
            minutesTextField.onValueChanged.RemoveListener(SetMinutes);
            coachDropDown.onValueChanged.RemoveListener(SetCoach);
            createEntryButton.onClick.RemoveListener(CreateEntryButtonClick);
        }
    }
}