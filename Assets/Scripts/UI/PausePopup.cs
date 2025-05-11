using Kuroneko.UIDelivery;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Popups
{
    public class PausePopup : Popup
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button quitButton;
        private UIController _controller;
    
        protected override void InitPopup()
        {
            resumeButton.onClick.AddListener(Resume);
            settingsButton.onClick.AddListener(Settings);
            mainMenuButton.onClick.AddListener(MainMenu);
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

        private void MainMenu()
        {
            // Need to unpause when going back out
            _controller.TogglePause();
            SceneManager.LoadScene("Start");
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}
