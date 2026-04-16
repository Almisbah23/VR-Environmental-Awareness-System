using UnityEngine;

public class FarmerReaction : MonoBehaviour
{
    private bool hasReacted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasReacted) return;

        if (other.CompareTag("Player"))
        {
            hasReacted = true;

            Debug.Log("FarmerNPC triggered by Player");

            if (SubtitleManager.Instance != null)
            {
                SubtitleManager.Instance.ForceShowLine(
                    "Without rain, my crops failed… my family is starving.",
                    4f
                );
            }
        }
    }
}
