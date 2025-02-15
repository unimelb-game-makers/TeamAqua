using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogueChoicePopupItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        private Choice data;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            DialogueSystem.Instance().ChooseChoice(data.index);
        }
        
        public void Init(Choice choice)
        {
            data = choice;
            text.SetText(data.text);
        }
    }
}
