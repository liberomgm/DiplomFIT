using Assets.Scripts.Core;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Calendar
{
    public class WorkoutRecordView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text date;

        [SerializeField]
        private TMP_Text userNameText;

        [SerializeField]
        private TMP_Text coachNameText;

        public void UpdateView(WorkoutRecord workoutRecord, string userName, string coachName)
        {
            date.text = workoutRecord.WorkoutDate.ToString();
            userNameText.text = userName;
            coachNameText.text = coachName;
        }
    }
}