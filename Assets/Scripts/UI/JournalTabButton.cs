using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class JournalTabButton : MonoBehaviour
    {
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private Sprite inactiveSprite;

        private Image image;
        private Button button;

        private void Awake()
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();
        }
        
        public void SetActive(bool active)
        {
            image.sprite = active ? activeSprite : inactiveSprite;
        }

        public void AddListener(UnityAction callback)
        {
            // Ugly hack because Awake is not called when GameObject is not enabled
            if (button == null)
                button = GetComponent<Button>();
            button.onClick.AddListener(callback);
        }
    }
}
