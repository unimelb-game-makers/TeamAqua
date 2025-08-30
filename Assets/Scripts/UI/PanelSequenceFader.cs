using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class PanelSequenceFader : MonoBehaviour
{
    [Tooltip("Panels whose child TMP texts will fade (panels themselves stay fully visible).")]
    public GameObject[] panels;

    public float fadeTime = 0.5f;   // in/out duration
    public float showTime = 2f;     // fully visible duration[]
    public float duration = 1f;
    public bool loop = true;

    void Start()
    {
        // Make sure panel objects are visible, but texts start hidden (alpha 0)
        foreach (var p in panels)
        {
            if (!p) continue;
            var texts = p.GetComponentsInChildren<TMP_Text>(true);
            SetAlpha(texts, 0f);
        }

        if (panels != null && panels.Length > 0)
            StartCoroutine(Run());
        else
            Debug.LogWarning("No panels assigned.");
    }

    IEnumerator Run()
    {
        int index = 0;

        do
        {
            var panel = panels[index];
            if (panel)
            {
                var texts = panel.GetComponentsInChildren<TMP_Text>(true);

                // Fade in text
                yield return FadeTexts(texts, 0f, 1f, fadeTime);

                // Hold
                yield return new WaitForSeconds(showTime);

                // Fade out text
                yield return FadeTexts(texts, 1f, 0f, fadeTime);

                yield return new WaitForSeconds(duration);
            }

            index = index + 1;
            if (index == panels.Length)
            {
                loop = false;
            }

        } while (loop);

        SceneManager.LoadScene("Start");
    }

    IEnumerator FadeTexts(TMP_Text[] texts, float from, float to, float time)
    {
        float t = 0f;
        while (t < time)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / time);
            SetAlpha(texts, a);
            yield return null;
        }
        SetAlpha(texts, to);
    }

    void SetAlpha(TMP_Text[] texts, float alpha)
    {
        if (texts == null) return;
        for (int i = 0; i < texts.Length; i++)
        {
            if (!texts[i]) continue;
            var c = texts[i].color;
            c.a = alpha;
            texts[i].color = c;
        }
    }
}
