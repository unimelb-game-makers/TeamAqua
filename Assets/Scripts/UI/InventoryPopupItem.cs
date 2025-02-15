using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class InventoryPopupItem : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text countText;

        public void Init(InventoryItem data)
        {
            itemImage.sprite = data.item.sprite;
            nameText.text = data.item.name;
            countText.text = data.count.ToString();
        }
    }
}
