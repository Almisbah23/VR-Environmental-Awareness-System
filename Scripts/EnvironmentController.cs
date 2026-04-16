using UnityEngine;
using UnityEngine.Rendering;

public class EnvironmentController : MonoBehaviour
{
    [Header("Lighting")]
    public Light directionalLight;
    [Range(0f, 2f)] public float normalIntensity = 1.1f;
    [Range(0f, 2f)] public float darkIntensity = 0.2f;
    public Color normalLightColor = Color.white;
    public Color darkLightColor = new Color(0.6f, 0.65f, 0.7f);

    [Header("Fog")]
    public bool enableFogControl = true;
    public Color normalFogColor = new Color(0.8f, 0.9f, 1f, 1f);
    public Color darkFogColor = new Color(0.15f, 0.17f, 0.2f, 1f);
    public float normalFogDensity = 0.002f;
    public float darkFogDensity = 0.03f;

    [Header("Transition")]
    public float transitionSeconds = 2f;

    private float t = 0f;
    private bool toDark = false;

    void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = FindAnyObjectByType<Light>();
        }
        // Initialize environment
        Apply(0f);
    }

    public void SetDark(bool dark)
    {
        toDark = dark;
        StopAllCoroutines();
        StartCoroutine(LerpEnv());
    }

    System.Collections.IEnumerator LerpEnv()
    {
        float start = t;
        float target = toDark ? 1f : 0f;
        float elapsed = 0f;

        while (elapsed < transitionSeconds)
        {
            elapsed += Time.deltaTime;
            t = Mathf.Lerp(start, target, elapsed / transitionSeconds);
            Apply(t);
            yield return null;
        }
        t = target;
        Apply(t);
    }

    void Apply(float k)
    {
        if (directionalLight != null)
        {
            directionalLight.intensity = Mathf.Lerp(normalIntensity, darkIntensity, k);
            directionalLight.color = Color.Lerp(normalLightColor, darkLightColor, k);
        }
        if (enableFogControl)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = Color.Lerp(normalFogColor, darkFogColor, k);
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogDensity = Mathf.Lerp(normalFogDensity, darkFogDensity, k);
        }
        RenderSettings.ambientLight = Color.Lerp(Color.white, new Color(0.2f, 0.22f, 0.25f), k);
    }
}
