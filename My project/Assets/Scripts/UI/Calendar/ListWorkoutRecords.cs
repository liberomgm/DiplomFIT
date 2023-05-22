using Assets.Scripts.Core;
using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI.Calendar
{
    public class ListWorkoutRecords : MonoBehaviour
    {
        [SerializeField]
        private WorkoutRecordView template;

        private List<WorkoutRecordView> workoutViews = new List<WorkoutRecordView>();

        public void AddRecord(WorkoutRecord workoutRecord, DatabaseProvider databaseProvider)
        {
            var workoutView = Instantiate(template, transform);

            var user = databaseProvider.GetUser(workoutRecord.User);
            var coach = databaseProvider.GetCoach(workoutRecord.Coach);

            workoutView.UpdateView(workoutRecord, $"{user.LastName} {user.FirstName} {user.FatherName}", $"{coach.LastName} {coach.FirstName} {coach.FatherName}");
            workoutViews.Add(workoutView);
        }

        public void Clear()
        {
            foreach (var item in workoutViews)
            {
                Destroy(item.gameObject);
            }

            workoutViews.Clear();
        }
    }
}