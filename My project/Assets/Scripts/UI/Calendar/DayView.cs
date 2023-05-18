using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Calendar
{
    public class DayView : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private Button button;
        
        public void SetDate(int day, Action<int> onDaySelected)
        {
            title.text = day.ToString();
            button.onClick.AddListener(() => onDaySelected.Invoke(day));
        }
    }
}