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
            bool hasKey = PlayerPrefs.HasKey(PlayerSave.SAVE_KEY);
            bool hasSave = hasKey && PlayerSave.HasSave(PlayerPrefs.GetString(PlayerSave.SAVE_KEY));
            bool hasEmptySave = PlayerSave.HasEmptySave();
            newGameButton.interactable = hasEmptySave;
            continueButton.interactable = hasSave;
            loadButton.interactable = hasSave;
        }
        

        private void NewGame()
        {
            SaveManager.instance.StartNewGame();
            // NOTE(Alex): Hardcoded because I'm fucking lazy
            SceneManager.LoadScene("Cutscene 1");
        }

        private void Continue()
        {
            string saveSlot = PlayerPrefs.GetString(PlayerSave.SAVE_KEY);
            SaveManager.instance.SetSaveSlot(saveSlot);
            // Make assumption that once the save file is made, they are already in NoonIsland
            SceneManager.LoadScene("NoonIsland");
        }

        private void Load()
        {
            _mainMenu.ShowSavePopup();
        }
    }
}
