using UnityEngine;

[RequireComponent(typeof(Collider))]
public class NextZoneTrigger : MonoBehaviour
{
    public SceneController sceneController;
    public KeyCode confirmKey = KeyCode.E;

    bool playerInside = false;

    void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            SubtitleManager.Instance?.ShowLine("Press E to continue the story.", 3f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(confirmKey))
        {
            sceneController?.GoToNextScene();
        }
    }
}
