using Kuroneko.UIDelivery;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Popups
{
    public class StartPopup : Popup
    {
        private MainMenuPopup _mainMenu;

        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button loadButton;
        
        protected override void InitPopup()
        {
            newGameButton.onClick.AddListener(NewGame);
            continueButton.onClick.AddListener(Continue);
            loadButton.onClick.AddListener(Load);
        }

        public void Init(MainMenuPopup mainMenu)
        {
            _mainMenu = mainMenu;
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            bool hasLoad = PlayerPrefs.HasKey(PlayerSave.SAVE_KEY);
            bool canLoad = hasLoad && PlayerSave.HasSave(PlayerPrefs.GetString(PlayerSave.SAVE_KEY));
            bool hasEmptySave = PlayerSave.HasEmptySave();
            newGameButton.interactable = hasEmptySave;
            loadButton.interactable = canLoad;
        }
        

        private void NewGame()
        {
            _mainMenu.playerSave.StartNewGame();
            // NOTE(Alex): Hardcoded because I'm fucking lazy
            SceneManager.LoadScene("Cutscene 1");
        }

        private void Continue()
        {
            _mainMenu.ShowSavePopup();
        }

        private void Load()
        {
            string saveSlot = PlayerPrefs.GetString(PlayerSave.SAVE_KEY);
            _mainMenu.playerSave.SetSaveSlot(saveSlot);
        }
    }
}
