using UnityEngine;

namespace UI.Calendar
{
    public class CreateEntryPanel : MonoBehaviour
    {
        public void Show(int year, int month, int day)
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}