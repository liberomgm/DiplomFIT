using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

namespace UI.Calendar
{
    public class CalendarView : MonoBehaviour
    {
        [SerializeField] private TMP_Text monthTitle;
        [SerializeField] private RectTransform dayContent;

        [SerializeField] private DayView dayPrefabs;

        private List<DayView> days = new List<DayView>();

        public void FillingOut(int monthNumber)
        {
            monthTitle.text = MonthName(monthNumber);
            var daysCount = DateTime.DaysInMonth(DateTime.Now.Year, monthNumber);

            for (int i = 0; i < daysCount; i++)
            {
                var day = Instantiate(dayPrefabs, dayContent);
                day.SetDate(i + 1);
                days.Add(day);
            }
        }

        private string MonthName(int monthNumber)
        {
            switch (monthNumber)
            {
                case 1:
                    return "Январь";
                case 2:
                    return "Февраль";
                case 3:
                    return "Март";
                case 4:
                    return "Апрель";
                case 5:
                    return "Май";
                case 6:
                    return "Июнь";
                case 7:
                    return "Июль";
                case 8:
                    return "Август";
                case 9:
                    return "Сентябрь";
                case 10:
                    return "Октябрь";
                case 11:
                    return "Ноябрь";
                case 12:
                    return "Декабрь";
            }

            throw new InvalidDataException();
        }

        public void Clear()
        {
            foreach (var day in days)
            {
                Destroy(day);
            }
            
            days.Clear();
        }
    }
}
