using Kuroneko.UIDelivery;

namespace UI
{
    public static class UIHelper
    {
        public static void SetActive(this Popup popup, bool active)
        {
            if (active) popup.ShowPopup();
            else popup.HidePopup();
        }
    }
}