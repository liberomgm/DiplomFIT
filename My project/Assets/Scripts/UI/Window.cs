using UnityEngine;

namespace UI
{
    public abstract class Window : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide();
        }

        protected abstract void OnShow();
        protected abstract void OnHide();
    }
}