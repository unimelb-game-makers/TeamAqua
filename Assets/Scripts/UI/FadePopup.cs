using Kuroneko.UIDelivery;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Popups
{
    public class FadePopup : Popup
    {
        [SerializeField]
        private Image fadeImage;

        [SerializeField]
        private TextMeshProUGUI dayText;
        private static readonly float FADE_DURATION = 2.0f;
        private static readonly float WAIT_DURATION = 1.0f;

        protected override void InitPopup()
        {
            DayManager.OnDayChanged += OnDayChange;
            HidePopup();
        }

        private void OnDayChange(int currentDay)
        {
            Debug.Log("Are we getting calleD??");
            // Start Fade In
            ShowPopup();
            Color fadeColour = fadeImage.color;
            fadeColour.a = 0f;
            fadeImage.color = fadeColour;
            
            dayText.text = "DAY " + currentDay;
            Fade(
                1f,
                () =>
                {
                    // Wait and change text after fade-in
                    LeanTween.delayedCall(
                        WAIT_DURATION,
                        () =>
                        {
                            dayText.text = "DAY " + (currentDay + 1);
                            // Start Fade Out
                            LeanTween.delayedCall(
                                WAIT_DURATION,
                                () =>
                                {
                                    Fade(
                                        0f,
                                        HidePopup
                                    );
                                }
                            );
                        }
                    );
                }
            );
        }

        private void Fade(float targetAlpha, System.Action onComplete)
        {
            LeanTween
                .value(gameObject, fadeImage.color.a, targetAlpha, FADE_DURATION)
                .setOnUpdate(
                    (float alpha) =>
                    {
                        // Update both fade panel and text alpha
                        Color fadeColor = fadeImage.color;
                        fadeColor.a = alpha;
                        fadeImage.color = fadeColor;

                        Color textColor = dayText.color;
                        textColor.a = alpha;
                        dayText.color = textColor;
                    }
                )
                .setOnComplete(onComplete);
        }
    }
}
