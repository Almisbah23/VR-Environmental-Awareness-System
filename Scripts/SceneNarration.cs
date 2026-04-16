using UnityEngine;

public class SceneNarration : MonoBehaviour
{
    public AudioSource narrationSource;

    void Start()
    {
        if (narrationSource != null)
        {
            narrationSource.Play();
        }
    }
}