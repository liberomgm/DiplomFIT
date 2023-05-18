using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;

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

        private int hour;
        private int minutes;
        private int coachId;
        
        public void Show(int year, int month, int day, List<string> coach)
        {
            this.gameObject.SetActive(true);
            dateText.text = $"{day}.{month}.{year}";
            
            coachDropDown.AddOptions(coach);
            hourTextField.onValueChanged.AddListener(SetHour);
            minutesTextField.onValueChanged.AddListener(SetMinutes);
            coachDropDown.onValueChanged.AddListener(SetCoach);
            
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
            this.coachId = coach;
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
            
            coachDropDown.ClearOptions();
            hourTextField.onValueChanged.RemoveListener(SetHour);
            minutesTextField.onValueChanged.RemoveListener(SetMinutes);
            coachDropDown.onValueChanged.RemoveListener(SetCoach);
        }
    }
}