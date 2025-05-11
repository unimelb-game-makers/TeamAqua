using System.Collections.Generic;
using Kuroneko.UIDelivery;
using Kuroneko.UtilityDelivery;
using UnityEngine;
using UnityEngine.UI;

namespace Popups
{
    public class SavePopup : Popup
    {
        [SerializeField] private Button backButton;
        [SerializeField] private RectTransform saveHolder;
        [SerializeField] private SavePopupItem sampleSavePopupItem;

        private List<SavePopupItem> _saves = new();

        private MainMenuPopup _mainMenu;
        public PlayerSave Save => _mainMenu.playerSave;

        protected override void InitPopup()
        {
            backButton.onClick.AddListener(Back);
            sampleSavePopupItem.gameObject.SetActiveFast(false);
        }

        public void Init(MainMenuPopup mainMenu)
        {
            _mainMenu = mainMenu;
        }

        public override void ShowPopup()
        {
            base.ShowPopup();
            TryInstantiate();
            InitSaves();
        }

        private void TryInstantiate()
        {
            int numToSpawn = PlayerSave.SAVE_FILES.Length - _saves.Count;
            if (numToSpawn > 0)
            {
                sampleSavePopupItem.gameObject.SetActiveFast(true);
                for (int i = 0; i < PlayerSave.SAVE_FILES.Length; ++i)
                {
                    SavePopupItem save = Instantiate(sampleSavePopupItem, saveHolder);
                    _saves.Add(save);
                }

                sampleSavePopupItem.gameObject.SetActiveFast(false);
            }

            for (int i = 0; i < _saves.Count; ++i)
            {
                _saves[i].gameObject.SetActiveFast(false);
            }
        }

        private void InitSaves()
        {
            for (int i = 0; i < PlayerSave.SAVE_FILES.Length; ++i)
            {
                if (i >= _saves.Count)
                    continue;
                _saves[i].gameObject.SetActiveFast(true);
                _saves[i].Init(this, PlayerSave.SAVE_FILES[i], i + 1);
            }
        }

        public void Load(string fileName)
        {
            _mainMenu.playerSave.SetSaveSlot(fileName);

        }

        private void Back()
        {
            _mainMenu.ShowStartPopup();
        }
    }
}
