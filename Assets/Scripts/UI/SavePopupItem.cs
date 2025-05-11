using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Popups
{
    public class SavePopupItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text number;
        [SerializeField] private Button saveButton;
        private SavePopup _savePopup;
        private string _fileName = string.Empty;
        
        public void Init(SavePopup savePopup, string fileName, int index)
        {
            _savePopup = savePopup;
            _fileName = fileName;
            bool exists = PlayerSave.HasSave(fileName);
            saveButton.interactable = exists;
            saveButton.onClick.AddListener(Load);
            // Could show more in the UI. Needs a redesign
            if (exists)
            {
                SaveSlot saveData = savePopup.Save.GetSaveData(fileName);
                title.SetText($"Day: {saveData.worldSaveData.currentDay}");
            }
            else
            {
                title.SetText("Empty");
            }
            number.SetText(index.ToString());
        }

        private void Load()
        {
            _savePopup.Load(_fileName);
        }
    }
}