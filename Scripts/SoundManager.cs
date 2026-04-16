using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource ambientBirds;   // loop = true
    public AudioSource axeSfx;         // loop = false
    public AudioSource thunderSfx;     // optional

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayAmbient(bool play)
    {
        if (ambientBirds == null) return;
        if (play && !ambientBirds.isPlaying) ambientBirds.Play();
        if (!play && ambientBirds.isPlaying) ambientBirds.Stop();
    }

    public void PlayAxe()    { if (axeSfx != null) axeSfx.Play(); }
    public void PlayThunder(){ if (thunderSfx != null) thunderSfx.Play(); }
}
