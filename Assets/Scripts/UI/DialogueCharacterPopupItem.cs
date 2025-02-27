using Kuroneko.UtilityDelivery;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DialogueCharacterPopupItem : MonoBehaviour
    {
        [SerializeField] private Image nameHolder;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private Image characterSprite;
        [SerializeField] private Animator animator;

        public void SetName(string characterName)
        {
             nameText.SetText(characterName);
        }
        
        public void PlayAnim(string animName)
        {
            animator.Play(animName);
        }
    }
}
