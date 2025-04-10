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
        private static readonly float  FADE_DURATION = 2.0f;
        private static readonly float WAIT_DURATION = 1.0f;
        

        protected override void InitPopup()
        {
            DayNight.OnDayChange += OnDayChange;
        }

 

        private void OnDayChange(float previousDay, float nextDay)
        {
            // Start Fade In
            ShowPopup();
            dayText.text = "DAY " + previousDay;
            Fade(1f, () =>
            {
                // Wait and change text after fade-in
                LeanTween.delayedCall(WAIT_DURATION, () =>
                {
                    dayText.text = "DAY " + nextDay;
                    // Start Fade Out
                    LeanTween.delayedCall(WAIT_DURATION, () =>
                    {
                        Fade(0f, () =>
                        {
                            DayNight.StartNewDay();
                            HidePopup();
                        });
                    });   
                });
            });
        }

        private void Fade(float targetAlpha, System.Action onComplete)
        {
            LeanTween.value(gameObject, fadeImage.color.a, targetAlpha, FADE_DURATION).setOnUpdate((float alpha) =>
            {
                // Update both fade panel and text alpha
                Color fadeColor = fadeImage.color;
                fadeColor.a = alpha;
                fadeImage.color = fadeColor;

                Color textColor = dayText.color;
                textColor.a = alpha;
                dayText.color = textColor;
            }).setOnComplete(onComplete);
        }


        

    }

}