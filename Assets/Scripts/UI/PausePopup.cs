using Kuroneko.UIDelivery;
using UnityEngine;
using UnityEngine.UI;

namespace Popups
{
    public class PausePopup : Popup
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button quitButton;
        private UIController _controller;
    
        protected override void InitPopup()
        {
            resumeButton.onClick.AddListener(Resume);
            settingsButton.onClick.AddListener(Settings);
            quitButton.onClick.AddListener(Quit);
        }

        public void Init(UIController controller)
        {
            _controller = controller;
        }
        
        private void Resume()
        {
            _controller.TogglePause();
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
