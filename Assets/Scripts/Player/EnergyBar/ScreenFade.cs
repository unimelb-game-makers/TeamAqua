using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage;  // Assign the UI Image in the Inspector
    public float fadeDuration = 1f;  // Duration of the fade
    private bool hasFaded = false;  // Ensure only one press works
    public TextMeshProUGUI dayText; // Assign the Text in the Inspector
    public int currentDay = 1; // Current date

    void Update()
    {
        if (DayNight.shouldChangeColor && Input.GetKeyDown(KeyCode.E) && !hasFaded)  // Press E once
        {
            hasFaded = true;
            Show();
            Invoke("Hide", 5f);
            hasFaded = false;
        }
    }

    private void ChangeColorAlpha(float currentAlpha, float targetAlpha)
    {
        LeanTween.alpha(fadeImage.rectTransform, targetAlpha, fadeDuration); // gradually turn Alpha of Image
        LeanTween.value(gameObject, currentAlpha, targetAlpha, fadeDuration)
        .setOnUpdate((float alpha) => {
            Color color = dayText.color;
            color.a = alpha;
            dayText.color = color;
        }); // gradually turn Alpha of Text
    }

    private void Show()
    {
        currentDay++; // Update date
        dayText.text = "DAY " + currentDay; 
        ChangeColorAlpha(0f, 1f);
    }

    private void Hide()
    {
        ChangeColorAlpha(1f, 0f);
    }
}
