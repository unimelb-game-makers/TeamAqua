using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;
    public Image fadeImage;           // 黒パネル（自動生成される）
    public float fadeDuration = 0.5f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (fadeImage == null) fadeImage = CreateFullScreenBlackImage();

        // 最初は黒 → 透明へフェードイン
        SetAlpha(1f);
        StartCoroutine(Fade(1f, 0f, fadeDuration));

        // 新シーンロード時も1フレーム黒を保持してからフェードイン
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            Instance.StartCoroutine(Instance.DelayedFadeIn());
        };
    }

    public static void FadeToScene(string sceneName, float duration = -1f)
    {
        if (Instance == null)
        {
            var go = new GameObject("~FadeController(Auto)");
            Instance = go.AddComponent<FadeController>();
            DontDestroyOnLoad(go);
        }

        if (duration > 0f) Instance.fadeDuration = duration;
        Instance.StartCoroutine(Instance.FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        yield return Fade(0f, 1f, fadeDuration);

        var op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (op.progress < 0.9f)
            yield return null;

        op.allowSceneActivation = true;
    }

    IEnumerator DelayedFadeIn()
    {
        yield return null; // 1フレーム黒を維持
        yield return Fade(1f, 0f, fadeDuration);
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Lerp(from, to, t / duration);
            SetAlpha(a);
            yield return null;
        }
        SetAlpha(to);
    }

    void SetAlpha(float a)
    {
        if (!fadeImage) return;
        var c = fadeImage.color;
        fadeImage.color = new Color(c.r, c.g, c.b, Mathf.Clamp01(a));
    }

    Image CreateFullScreenBlackImage()
    {
        var canvasGO = new GameObject("FadeCanvas");
        canvasGO.transform.SetParent(transform, false);
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        canvas.sortingOrder = 32767;

        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        var imgGO = new GameObject("FadeImage");
        imgGO.transform.SetParent(canvasGO.transform, false);
        var img = imgGO.AddComponent<Image>();
        img.raycastTarget = false;

        var rt = img.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        img.color = Color.black;
        return img;
    }
}
