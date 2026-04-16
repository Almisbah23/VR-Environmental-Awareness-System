using UnityEngine;

public class DustTrigger : MonoBehaviour
{
    public ParticleSystem dustFX;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger by: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER detected!");

            if (dustFX != null)
                dustFX.Play();

            if (SubtitleManager.Instance != null)
                SubtitleManager.Instance.ShowLine(
                    "The land is dry and lifeless…", 3f);
        }
    }
}
