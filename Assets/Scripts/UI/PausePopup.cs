using Kuroneko.UIDelivery;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PausePopup : Popup
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
    
        protected override void InitPopup()
        {
            resumeButton.onClick.AddListener(Resume);
            settingsButton.onClick.AddListener(Settings);
            quitButton.onClick.AddListener(Quit);
        }

        private void Resume()
        {
            HidePopup();
        }

        private void Settings()
        {
        
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}
