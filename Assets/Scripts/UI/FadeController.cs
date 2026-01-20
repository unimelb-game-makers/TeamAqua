using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public enum Mode { Manual, AutoHideOnStart }

    [Header("References")]
    [SerializeField] private Image blackPanel; // Assign in Inspector

    [Header("Settings")]
    [SerializeField] private Mode mode = Mode.Manual;
    [SerializeField] private float holdTime = 0.5f;
    [SerializeField] private float fadeDuration = 0.5f;

    void Awake()
    {
        if (blackPanel == null)
        {
            Debug.LogError("FadeController: 'blackPanel' not assigned.", this);
            enabled = false;
            return;
        }

        if (mode == Mode.AutoHideOnStart)
        {
            SetAlpha(1f); // Start fully black
        }
    }

    void Start()
    {
        if (mode == Mode.AutoHideOnStart)
        {
            StartCoroutine(FadeOutAfterDelay());
        }
    }

    // === Public Methods ===

    public void Show()
    {
        blackPanel.gameObject.SetActive(true);
        StartCoroutine(Fade(1f));
    }

    public void Hide()
    {
        StartCoroutine(Fade(0f));
    }

    public void ShowWhileLoading(string nextScene)
    {
        StartCoroutine(Co_ShowWhileLoading(nextScene));
    }

    private IEnumerator Co_ShowWhileLoading(string nextScene)
    {
        yield return StartCoroutine(Fade(1f));
        yield return new WaitForSeconds(holdTime);
        SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
    }

    private IEnumerator FadeOutAfterDelay()
    {
        yield return new WaitForSeconds(holdTime);
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = blackPanel.color.a;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            SetAlpha(newAlpha);
            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float a)
    {
        var color = blackPanel.color;
        color.a = a;
        blackPanel.color = color;
    }
}
