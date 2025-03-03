using Kuroneko.UIDelivery;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class FadePopup : Popup
    {
        [SerializeField] private Image fadeImage;
        [SerializeField] private TextMeshProUGUI dayText;
        

        protected override void InitPopup()
        {
            DayNight.OnDayChange += OnDayChange;
        }

        private void OnDayChange(float currentAlpha, float targetAlpha)
        {
            dayText.text = "DAY " + DayNight.currentDay;

            LeanTween.alpha(fadeImage.rectTransform, targetAlpha, DayNight.duration); // gradually turn Alpha of Image
            LeanTween.value(gameObject, currentAlpha, targetAlpha, DayNight.duration)
            .setOnUpdate((float alpha) => {
                Color color = dayText.color;
                color.a = alpha;
                dayText.color = color;
            }); // gradually turn Alpha of Text
        }
        private void OnDestroy()
        {
            DayNight.OnDayChange -= OnDayChange;
        }
    }

}