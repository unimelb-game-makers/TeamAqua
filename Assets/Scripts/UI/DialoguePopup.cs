using Kuroneko.UIDelivery;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DialoguePopup : Popup
    {
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private DialogueCharacterPopupItem leftCharacter;
        [SerializeField] private DialogueCharacterPopupItem rightCharacter;
        [SerializeField] private DialogueChoicePopup choicePopup;
        protected override void InitPopup()
        {
        }
    }
}
