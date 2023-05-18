using System;
using UI.Calendar;
using UnityEngine;

namespace UI.Windows
{
    public class ScheduleWindow : Window
    {
        [SerializeField] private CalendarView calendarView;
        
        protected override void OnShow()
        {
            calendarView.FillingOut(DateTime.Now.Month);
        }

        protected override void OnHide()
        {
            calendarView.Clear();
        }
    }
}