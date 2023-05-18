using TMPro;
using UnityEngine;

namespace UI.Calendar
{
    public class DayView : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;

        public void SetDate(int day)
        {
            title.text = day.ToString();
        }
    }
}