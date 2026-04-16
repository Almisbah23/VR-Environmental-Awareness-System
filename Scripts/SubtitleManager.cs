using UnityEngine;
using TMPro;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance { get; private set; }

    [SerializeField] private TMP_Text subtitleText;
    [SerializeField, Range(0.2f, 10f)] private float fadeTime = 0.35f;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        if (subtitleText == null)
            subtitleText = FindAnyObjectByType<TMP_Text>();
        ClearImmediate();
    }

    public void ForceShowLine(string message, float duration)
{
    StopAllCoroutines();        // stop previous subtitle
    subtitleText.text = message;
    subtitleText.gameObject.SetActive(true);
    StartCoroutine(HideAfterDelay(duration));
}
    IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearImmediate();
    }
    public void ShowLine(string text, float seconds = 3f)
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine(text, seconds));
    }

    IEnumerator ShowRoutine(string text, float seconds)
    {
        subtitleText.gameObject.SetActive(true);
        subtitleText.alpha = 0;
        subtitleText.text = text;

        // Fade in
        float t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            subtitleText.alpha = Mathf.Lerp(0, 1, t / fadeTime);
            yield return null;
        }
        subtitleText.alpha = 1;

        yield return new WaitForSeconds(seconds);

        // Fade out
        t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            subtitleText.alpha = Mathf.Lerp(1, 0, t / fadeTime);
            yield return null;
        }
        ClearImmediate();
    }

    void ClearImmediate()
    {
        if (subtitleText != null)
        {
            subtitleText.text = "";
            subtitleText.alpha = 0;
            subtitleText.gameObject.SetActive(true);
        }
    }
}
